using System;

namespace Test.NewSolution.FormsApp.Attributes
{
    /// <summary>
    /// Depends on attribute.
    /// </summary>
    public class DependsOnAttribute: Attribute
    {
        /// <summary>
        /// Gets or sets the source property.
        /// </summary>
        /// <value>The source property.</value>
        public string SourceProperty {get;set;}

        /// <summary>
        /// If the property is set but the value is the same, this flag will make sure that the 
        /// notification event is called.
        /// </summary>
        /// <value><c>true</c> if raise property change for equal values; otherwise, <c>false</c>.</value>
        public bool RaisePropertyChangeForEqualValues { get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin4U.FormsApp.Mvvm.DependsOnAttribute"/> class.
        /// </summary>
        public DependsOnAttribute(string propertyName)
        {
            SourceProperty = propertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin4U.FormsApp.Attributes.DependsOnAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="raisePropertyChangeForEqualValues">If set to <c>true</c> raise property change for equal values.</param>
        public DependsOnAttribute(string propertyName, bool raisePropertyChangeForEqualValues)
        {
            SourceProperty = propertyName;
            RaisePropertyChangeForEqualValues = raisePropertyChangeForEqualValues;
        }
    }
}

