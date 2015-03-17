﻿using System;
using Test.NewSolution.Forms.ViewModels;
using Xamarin.Forms;

namespace Test.NewSolution.Forms.Views
{
    /// <summary>
    /// Menu view.
    /// </summary>
    public class MenuView: BaseContentsView<MenuViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Test.NewSolution.Forms.Views.MenuView"/> class.
        /// </summary>
        public MenuView()
        {
            Icon = ImageProvider.GetImageSource("MenuButton");
        }

        /// <summary>
        /// Implement to create the layout on the page
        /// </summary>
        /// <returns>The layout.</returns>
        protected override View CreateContents()
        {
            return new StackLayout();
        }
    }
}

