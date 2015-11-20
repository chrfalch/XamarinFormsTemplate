using System;
using Xamarin.Forms;

namespace Test.NewSolution.FormsApp.Extensions
{
    /// <summary>
    /// Binding extensions.
    /// </summary>
    public static class BindingExtensions
    {
        /// <summary>
        /// Sets up a binding
        /// </summary>
        /// <param name="bindableObject">Bindable object.</param>
        /// <param name="bindableProperty">Bindable property.</param>
        /// <param name="viewModelProperty">View model property.</param>
        public static View BindTo(this View view, BindableProperty bindableProperty, string propertyName, 
            IValueConverter converter = null){

            view.SetBinding(bindableProperty, propertyName, converter: converter); //, mode:bindingMode);
            return view;
        }
    }
}

