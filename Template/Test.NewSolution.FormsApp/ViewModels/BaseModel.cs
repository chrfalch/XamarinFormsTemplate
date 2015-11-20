using System;
using Test.NewSolution.Classes;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Test.NewSolution.Helpers;
using System.Reflection;
using Test.NewSolution.FormsApp.Attributes;

namespace Test.NewSolution.FormsApp.ViewModels
{
    /// <summary>
    /// Base model.
    /// </summary>
    public class BaseModel: BaseNotifyPropertyChangedObject
    {
        #region Private Members

        /// <summary>
        /// The storage.
        /// </summary>
        private readonly Dictionary<string, object> _storage = 
            new Dictionary<string, object>();

        /// <summary>
        /// Command dependencies - key == property, value = list of property names
        /// </summary>
        private readonly Dictionary<string, List<string>> _propertyDependencies = 
            new Dictionary<string, List<string>>();

        /// <summary>
        /// The notify change for same values.
        /// </summary>
        private readonly List<string> _notifyChangeForSameValues = new List<string>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin4U.FormsApp.ViewModels.BaseModel"/> class.
        /// </summary>
        public BaseModel()
        {
            // Update property dependencies
            ReadPropertyDependencies();
        }

        #region Protected Members

        /// <summary>
        /// Sets a value in viewmodel storage and raises property changed if value has changed
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="TValueType">The 1st type parameter.</typeparam>
        protected bool SetValue<TValueType>(TValueType value, [CallerMemberName] string propertyName = null) 
        {
            var existingValue = GetValue<TValueType>(propertyName);

            // Check for equality
            if (!_notifyChangeForSameValues.Contains(propertyName) && 
                EqualityComparer<TValueType>.Default.Equals (existingValue, value))
                return false;

            SetObjectForKey<TValueType> (propertyName, value);

            RaisePropertyChangedEvent (propertyName);

            return true;
        }

        /// <summary>
        /// Adds a dependency between a property and another property. Whenever the property changes, the command's 
        /// state will be updated
        /// </summary>
        /// <param name="property">Source property.</param>
        /// <param name="dependantProperty">Target property.</param>
        private void AddPropertyDependency(Expression<Func<object>> property, 
            Expression<Func<object>> dependantProperty)
        {
            var propertyName = PropertyNameHelper.GetPropertyName <BaseViewModel>(property);
            if (!_propertyDependencies.ContainsKey (propertyName))
                _propertyDependencies.Add (propertyName, new List<string> ());

            var list = _propertyDependencies [propertyName];
            list.Add (PropertyNameHelper.GetPropertyName<BaseViewModel>(dependantProperty));
        }

        /// <summary>
        /// Adds a dependency between a property and another property. Whenever the property changes, the command's 
        /// state will be updated
        /// </summary>
        /// <param name="property">Source property.</param>
        /// <param name="dependantProperty">Target property.</param>
        protected void AddPropertyDependency(string sourceProperty, string dependantProperty)
        {
            if (!_propertyDependencies.ContainsKey (sourceProperty))
                _propertyDependencies.Add (sourceProperty, new List<string> ());

            var list = _propertyDependencies [sourceProperty];
            list.Add (dependantProperty);
        }

        /// <summary>
        /// Adds the raise notify changed for property when value is the same.
        /// </summary>
        /// <param name="property">Property.</param>
        /// <param name="dependantProperty">Dependant property.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        protected void AddRaiseNotifyChangedForPropertyWhenValueIsTheSame(string propertyName)
        {
            _notifyChangeForSameValues.Add(propertyName);
        }

        /// <summary>
        /// Calls the notify property changed event if it is attached. By using some
        /// Expression/Func magic we get compile time type checking on our property
        /// names by using this method instead of calling the event with a string in code.
        /// </summary>
        /// <param name="property">Property.</param>
        protected override void RaisePropertyChangedEvent (Expression<Func<object>> property)
        {
            base.RaisePropertyChangedEvent (property);
            var propertyName = PropertyNameHelper.GetPropertyName<BaseViewModel>(property);
            CheckDependantProperties (propertyName);
        }

        /// <summary>
        /// Calls the notify property changed event if it is attached. By using some
        /// Expression/Func magic we get compile time type checking on our property
        /// names by using this method instead of calling the event with a string in code.
        /// </summary>
        /// <param name="property">Property.</param>
        protected override void RaisePropertyChangedEvent (string propertyName)
        {
            base.RaisePropertyChangedEvent (propertyName);
            CheckDependantProperties (propertyName);
        }

        /// <summary>
        /// Checks the dependant properties and commands.
        /// </summary>
        protected virtual void CheckDependantProperties (string propertyName)
        {
            // Dependent properties?
            if (_propertyDependencies.ContainsKey (propertyName)) {
                foreach (var dependentProperty in _propertyDependencies[propertyName])
                    RaisePropertyChangedEvent (dependentProperty);
            }
        }

        /// <summary>
        /// Returns a value from the viewmodel storage
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="name">Name.</param>
        /// <typeparam name="TValueType">The 1st type parameter.</typeparam>
        protected TValueType GetValue<TValueType>([CallerMemberName] string property = null) 
        {
            return GetValue<TValueType> (() => default(TValueType), propertyName:property);
        }

        /// Returns a value from the viewmodel storage
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="name">Name.</param>
        /// <typeparam name="TValueType">The 1st type parameter.</typeparam>
        protected TValueType GetValue<TValueType>(Func<TValueType> defaultValueFunc, [CallerMemberName] string propertyName = null) 
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("propertyName");

            return GetObjectForKey<TValueType> (propertyName, defaultValueFunc());
        }

        /// <summary>
        /// Sets the object for key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected void SetObjectForKey<T>(string key, T value)
        {
            if (_storage.ContainsKey (key))
                _storage [key] = value;
            else
                _storage.Add (key, value);      
        }

        /// <summary>
        /// Gets the object for key.
        /// </summary>
        /// <returns>The object for key.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected T GetObjectForKey<T>(string key, T defaultValue)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("key");

            if (!_storage.ContainsKey (key)) {
                if (defaultValue == null)
                    return defaultValue;

                SetObjectForKey (key, defaultValue);
            }

            return (T)Convert.ChangeType( _storage [key], typeof(T));
        }

        /// <summary>
        /// Handles the property dependency.
        /// </summary>
        /// <param name="dependantPropertyInfo">Dependant property info.</param>
        protected virtual bool HandlePropertyDependency(PropertyInfo dependantPropertyInfo, string sourcePropertyName)
        {
            return false;   
        }
        #endregion

        #region Private Members

        /// <summary>
        /// Reads the property dependencies.
        /// </summary>
        private void ReadPropertyDependencies()
        {
            foreach (var prop in this.GetType().GetRuntimeProperties())
            {
                foreach (var dependantPropertyInfo in this.GetType().GetRuntimeProperties())
                {                
                    var attribute = dependantPropertyInfo.GetCustomAttribute<DependsOnAttribute>();
                    if (attribute == null)
                        continue;

                    var handled = HandlePropertyDependency(dependantPropertyInfo, attribute.SourceProperty);

                    if (!handled)
                    {
                        // Add a dependency between two properties
                        AddPropertyDependency(attribute.SourceProperty, dependantPropertyInfo.Name);

                        if (attribute.RaisePropertyChangeForEqualValues)
                            AddRaiseNotifyChangedForPropertyWhenValueIsTheSame(attribute.SourceProperty);
                    }
                }
            }
        } 
        #endregion
    }
}

