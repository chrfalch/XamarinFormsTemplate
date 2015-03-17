using System;

using Xamarin.Forms;
using Test.NewSolution.Forms.Contracts;

namespace Test.NewSolution.Forms
{
    /// <summary>
    /// App.
    /// </summary>
	public class App : Application
	{                
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.App"/> class.
        /// </summary>
        /// <param name="typeResolveProvider">Type resolve provider.</param>
		public App (ITypeResolveProvider typeResolveProvider)
		{
            Container = typeResolveProvider;

			// The root page of your application
			MainPage = new ContentPage {
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							XAlign = TextAlignment.Center,
							Text = "Welcome to Xamarin Forms!"
						}
					}
				}
			};
		}

        #region App Properties

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public ITypeResolveProvider Container { get; private set;}

        #endregion

        #region App Lifecycle Callbacks

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

        /// <summary>
        /// Application developers override this method to perform actions when the application enters the sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

        /// <summary>
        /// Application developers override this method to perform actions when the application resumes from a sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        #endregion
	}
}

