using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Test.NewSolution.FormsApp.Controls;
using Test.NewSolution.Droid.Renderers;

[assembly: ExportRenderer (typeof (FontAwesomeLabel), typeof (FontAwesomeLabelRenderer))]
namespace Test.NewSolution.Droid.Renderers
{
    public class FontAwesomeLabelRenderer: LabelRenderer
    {
        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            var typeface = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.Assets, "Fonts/FontAwesome.ttf");
            Control.SetTypeface(typeface, TypefaceStyle.Normal);

        }
    }
}

