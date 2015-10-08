﻿using System;
using Test.NewSolution.FormsApp.Mvvm;
using Xamarin.Forms;

namespace Test.NewSolution.iOS.Platform.Mvvm
{
    /// <summary>
    /// Image provider.
    /// </summary>
    public class TouchImageProvider: IImageProvider
    {
        #region IImageProvider implementation

        /// <summary>
        /// Returns an image asset loaded on the platform
        /// </summary>
        /// <returns>The image.</returns>
        /// <param name="imageName">Image name.</param>
        public FileImageSource GetImageSource(string imageName)
        {
            return imageName;
        }

        #endregion
    }
}

