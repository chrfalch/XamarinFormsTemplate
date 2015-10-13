/****************************** Module Header ******************************\
Module Name:  BaseContentsView.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.Collections.Generic;
using Test.NewSolution.Classes;
using Test.NewSolution.FormsApp.IoC;
using Test.NewSolution.Helpers;
using Test.NewSolution.FormsApp.ViewModels;
using Test.NewSolution.FormsApp.Mvvm;
using System.Threading.Tasks;

namespace Test.NewSolution.FormsApp.Views
{
	/// <summary>
	/// Base view.
	/// </summary>
	public abstract class BaseContentsView<TViewModel>: ContentPage, IView
		where TViewModel : BaseViewModel
	{

		#region Private Members

        /// <summary>
		/// Main relative layout
		/// </summary>
		private readonly RelativeLayout _layout;

        /// <summary>
        /// The property change listeners.
        /// </summary>
        private readonly List<PropertyChangeListener> _propertyChangeListeners = new List<PropertyChangeListener>();

        #endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
        public BaseContentsView ()
		{
            // Image provider
            ImageProvider = Container.Resolve<IImageProvider>();

			// Set up viewmodel and viewmodel values
            ViewModel = Container.Resolve<TViewModel> ();
			BindingContext = ViewModel;

			// Bind title
            this.SetBinding (Page.TitleProperty, NameOf (vm => vm.Title));

			// Loading/Progress overlay
			_layout = new RelativeLayout ();
			var contents = CreateContents ();
            _layout.Children.Add(contents, () => _layout.Bounds);

			// Set our content to be the relative layout with progress overlays etc.
			Content = _layout;

            // Listen for changes to busy
            ListenForPropertyChange(mn => mn.IsBusy, () =>{
                ProgressHelper.UpdateProgress(ViewModel.IsBusy, ViewModel.IsBusyText, ViewModel.IsBusySubTitle);
            });

            ListenForPropertyChange(mn => mn.IsBusyText, () =>{
                ProgressHelper.UpdateProgress(ViewModel.IsBusy, ViewModel.IsBusyText, ViewModel.IsBusySubTitle);
            });

            ListenForPropertyChange(mn => mn.IsBusySubTitle, () =>{
                ProgressHelper.UpdateProgress(ViewModel.IsBusy, ViewModel.IsBusyText, ViewModel.IsBusySubTitle);
            });
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns the ViewModel
		/// </summary>
		public TViewModel ViewModel{get; private set;}

		#endregion

		#region View LifeCycle

		/// <summary>
		/// Raised when the view has appeared on screen.
		/// </summary>
		protected override async void OnAppearing()
		{
            base.OnAppearing();

            await ViewModel.OnAppearingAsync();			
		}

        /// <summary>
        /// Raises the disappearing event.
        /// </summary>
		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			ViewModel.OnDisappearingAsync ();			
		}

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>To be added.</returns>
        /// <remarks>To be added.</remarks>
        protected override bool OnBackButtonPressed()
        {
            if (DefaultBackButtonBehaviour)
                return base.OnBackButtonPressed();
            
            if (ViewModel.BackButtonCommand != null &&
               ViewModel.BackButtonCommand.CanExecute(null))
                ViewModel.BackButtonCommand.Execute(null);
            
            return true;
        }
			
		#endregion

		#region Protected Members

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Test.NewSolution.FormsApp.Views.BaseContentsView`1"/> default back button behaviour.
        /// </summary>
        /// <value><c>true</c> if default back button behaviour; otherwise, <c>false</c>.</value>
        protected bool DefaultBackButtonBehaviour { get; set; }

        /// <summary>
        /// Gets the image provide.
        /// </summary>
        /// <value>The image provide.</value>
        protected IImageProvider ImageProvider {get;private set;}

		/// <summary>
		/// Implement to create the layout on the page
		/// </summary>
		/// <returns>The layout.</returns>
		protected abstract View CreateContents ();

		/// <summary>
		/// Sets the background image.
		/// </summary>
		/// <param name="imageType">Image type.</param>
		protected void SetBackgroundImage(string imageName)
		{
            // Background image
			var image = new Image {
                Source = ImageProvider.GetImageSource(imageName),
				Aspect = Aspect.AspectFill
			};

			_layout.Children.Add (image, Constraint.Constant(0), 
				Constraint.RelativeToParent((parent) => parent.Height - 568), 
				Constraint.RelativeToParent((parent) => parent.Width),
				Constraint.Constant(568));
		}

        /// <summary>
        /// Returns the name of the member
        /// </summary>
        /// <returns>The of.</returns>
        /// <param name="prop">Property.</param>
        public string NameOf(Expression<Func<TViewModel, object>> property)
        {
            return PropertyNameHelper.GetPropertyName<TViewModel> (property);
        }

        /// <summary>
        /// Returns the name of the member
        /// </summary>
        /// <returns>The of.</returns>
        /// <param name="prop">Property.</param>
        public string NameOf<TModel>(Expression<Func<TModel, object>> property)
        {
            return PropertyNameHelper.GetPropertyName<TModel> (property);
        }

        /// <summary>
        /// Listens for property change.
        /// </summary>
        /// <param name="property">Property.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        protected void ListenForPropertyChange(Expression<Func<TViewModel, object>> property, Action callback)
        {
            var changeListener = new PropertyChangeListener();
            changeListener.Listen<TViewModel>(property, ViewModel, callback);
            _propertyChangeListeners.Add(changeListener);
        }
		#endregion

        #region IViewWithParameter implementation

        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="parameter">Parameter.</param>
        /// <typeparam name="TParameter">The 1st type parameter.</typeparam>
        public Task InitializeAsync (object parameter)
        {
            return ViewModel.InitializeAsync(parameter);
        }

        #endregion
	}
}

