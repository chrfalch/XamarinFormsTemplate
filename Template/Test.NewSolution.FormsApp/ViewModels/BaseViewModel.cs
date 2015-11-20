/****************************** Module Header ******************************\
Module Name:  BaseViewModel.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq.Expressions;
using Test.NewSolution.Classes;
using Test.NewSolution.FormsApp.Providers;
using Test.NewSolution.Helpers;
using System.Runtime.CompilerServices;
using Test.NewSolution.FormsApp.Attributes;
using System.Reflection;

namespace Test.NewSolution.FormsApp.ViewModels
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public abstract class BaseViewModel : BaseModel
    {
        #region Private Members

        /// <summary>
        /// Command dependencies - key == property, value = list of commands
        /// </summary>
        private readonly Dictionary<string, List<Command>> _commandDependencies = 
            new Dictionary<string, List<Command>>();

        /// <summary>
        /// Command cache
        /// </summary>
        private readonly Dictionary<string, Command> _commands = new Dictionary<string, Command> ();

        /// <summary>
        /// The property change listeners.
        /// </summary>
        private readonly List<PropertyChangeListener> _propertyChangeListeners = new List<PropertyChangeListener>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CF.Xamarin.Forms.Mvvm.ViewModels.BaseViewModel"/> class.
        /// </summary>
        /// <param name="viewModelStorage">View model storage.</param>
        public BaseViewModel()
        {
            // Title
            Title = this.GetType().Name;
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Creates or returns the 
        /// </summary>
        /// <returns>The command.</returns>
        /// <param name="action">Action.</param>
        /// <param name="state">State.</param>
        protected Command GetOrCreateCommand(Action commandAction, Func<bool> canExecuteFunc = null, 
            [CallerMemberName] string commandName = null)
        {
            if (string.IsNullOrEmpty(commandName))
                throw new ArgumentException("commandname");

            if (!_commands.ContainsKey(commandName))
            {
                if(canExecuteFunc == null)
                    _commands.Add(commandName, new Command(commandAction));
                else
                    _commands.Add(commandName, new Command(commandAction, canExecuteFunc));
            }

            return _commands [commandName];
        }

        /// <summary>
        /// Creates or returns the 
        /// </summary>
        /// <returns>The command.</returns>
        /// <param name="action">Action.</param>
        /// <param name="state">State.</param>
        protected Command GetOrCreateCommand(Action<object> commandAction, Func<object, bool> canExecuteFunc = null, 
            [CallerMemberName] string commandName = null)
        {
            if (string.IsNullOrEmpty(commandName))
                throw new ArgumentException("commandname");

            if (!_commands.ContainsKey (commandName))
            {
                if(canExecuteFunc == null)
                    _commands.Add(commandName, new Command(commandAction));
                else
                    _commands.Add(commandName, new Command(commandAction, canExecuteFunc));
            }

            return _commands [commandName];
        }

        /// <summary>
        /// Creates or returns the 
        /// </summary>
        /// <returns>The command.</returns>
        /// <param name="action">Action.</param>
        /// <param name="state">State.</param>
        protected Command<T> GetOrCreateCommand<T>(Action<T> commandAction, Func<T, bool> canExecuteFunc = null, 
            [CallerMemberName] string commandName = null)
        {
            if (string.IsNullOrEmpty(commandName))
                throw new ArgumentException("commandname");

            if (!_commands.ContainsKey (commandName))
            {
                if(canExecuteFunc == null)
                    _commands.Add(commandName, new Command<T>(commandAction));
                else
                    _commands.Add(commandName, new Command<T>(commandAction, canExecuteFunc));
            }

            return _commands [commandName] as Command<T>;
        }

        /// <summary>
        /// Listens for property change.
        /// </summary>
        /// <param name="property">Property.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        protected void ListenForPropertyChange<TObject>(Expression<Func<TObject, object>> property, TObject obj, Action callback)
        {
            var changeListener = new PropertyChangeListener();
            changeListener.Listen<TObject>(property, obj, callback);
            _propertyChangeListeners.Add(changeListener);
        }

        /// <summary>
        /// Checks the dependant properties and commands.
        /// </summary>
        protected override void CheckDependantProperties (string propertyName)
        {
            base.CheckDependantProperties(propertyName);

            // Dependent commands
            if (_commandDependencies.ContainsKey (propertyName)) {
                foreach (var dependentCommand in _commandDependencies[propertyName])
                    RaiseCommandStateChangedEvent (dependentCommand);
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Adds a dependency between a command and a property. Whenever the property changes, the command's 
        /// state will be updated
        /// </summary>
        /// <param name="property">Source Property.</param>
        /// <param name="command">Target Command.</param>
        private void AddCommandDependency(Expression<Func<object>> property, Command command)
        {
            var propertyName = PropertyNameHelper.GetPropertyName <BaseViewModel>(property);
            if (!_commandDependencies.ContainsKey (propertyName))
                _commandDependencies.Add (propertyName, new List<Command> ());

            var list = _commandDependencies [propertyName];
            list.Add (command);
        }

        /// <summary>
        /// Adds a dependency between a command and a property. Whenever the property changes, the command's 
        /// state will be updated
        /// </summary>
        /// <param name="property">Source Property.</param>
        /// <param name="command">Target Command.</param>
        private void AddCommandDependency(string propertyName, Command command)
        {
            if (!_commandDependencies.ContainsKey (propertyName))
                _commandDependencies.Add (propertyName, new List<Command> ());

            var list = _commandDependencies [propertyName];
            list.Add (command);
        }

        /// <summary>
        /// Raises the command state changed event.
        /// </summary>
        /// <param name="command">Command.</param>
        private void RaiseCommandStateChangedEvent(Command command)
        {
            command.ChangeCanExecute ();
        }

        /// <summary>
        /// Handles the property dependency.
        /// </summary>
        /// <param name="dependantPropertyInfo">Dependant property info.</param>
        /// <returns><c>true</c>, if property dependency was handled, <c>false</c> otherwise.</returns>
        protected override bool HandlePropertyDependency(PropertyInfo dependantProperty, string sourcePropertyName)
        {
            // check command or property
            if (dependantProperty.PropertyType == typeof(Command))
            {   
                // Add a dependency between command and property
                AddCommandDependency(sourcePropertyName, dependantProperty.GetValue(this) as Command);

                return true;
            }

            return base.HandlePropertyDependency(dependantProperty, sourcePropertyName);
        }

        #endregion

        #region ViewModel LifeCycle

        /// <summary>
        /// Initializes the viewmodel
        /// </summary>
        public virtual Task InitializeAsync()
        {
            return Task.FromResult (true);
        }

        /// <summary>
        /// Initializes the viewmodel with a parameter. Called from the navigation manager through the view 
        /// as a IViewWithParameter instance
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameter">Parameter.</param>
        /// <typeparam name="TParameter">The 1st type parameter.</typeparam>
        public virtual Task InitializeAsync (object parameter)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Override to implement logic when the view has been set up on screen
        /// </summary>
        public virtual async Task OnAppearingAsync()
        {
            // Call initialize
            await InitializeAsync ();  

            IsOnAppearingCalled = true;
        }

        /// <summary>
        /// Called whenever the view is hidden
        /// </summary>
        public virtual Task OnDisappearingAsync()
        {       
            return Task.FromResult (true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title 
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }

        /// <summary>
        /// Flag that is set when OnAppearing was called.
        /// </summary>
        /// <value><c>true</c> if this instance is on appearing called; otherwise, <c>false</c>.</value>
        public bool IsOnAppearingCalled 
        {
            get { return GetValue<bool>(); }
            set { SetValue<bool>(value); }
        }

        /// <summary>
        /// Gets the back button command.
        /// </summary>
        /// <value>The back button command.</value>
        public virtual Command BackButtonCommand { get { return null; } }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy { 
            get{ return GetValue(() => false); }
            set{ SetValue(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public string IsBusyText 
        { 
            get{ return GetValue<string>(); }
            set{ SetValue<string> (value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public string IsBusySubTitle 
        { 
            get{ return GetValue<string>(); }
            set{ SetValue<string> (value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        [DependsOn(nameof(IsBusyText))]
        public bool IsBusyTextVisible 
        { 
            get { return !String.IsNullOrEmpty (IsBusyText); }
        }

        #endregion

    }
}

