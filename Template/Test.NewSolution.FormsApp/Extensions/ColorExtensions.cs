using System;
using Xamarin.Forms;

namespace Test.NewSolution.FormsApp.Extensions
{
    public static class ColorExtensions
{        
        public static Color Lighter (this Color color)
        {
            return color.WithLuminosity (color.Luminosity + 0.2);
        }

        public static Color Darker (this Color color)
        {
            return color.WithLuminosity (color.Luminosity - 0.2);
        }

        public static Color Lighten (this Color color, double amount)
        {
            return color.WithLuminosity (color.Luminosity + amount);
        }

        public static Color Darken (this Color color, double amaount)
        {
            return color.WithLuminosity (color.Luminosity - amaount);
        }

        public static Color Saturate (this Color color, double amount)
        {
            return color.WithSaturation (color.Saturation + amount);
        }

        public static Color Desaturate (this Color color, double amaount)
        {
            return color.WithSaturation (color.Saturation - amaount);
        }

        public static Color Grayscale (this Color color)
        {
            return color.WithSaturation (0);
        }

        public static Color Complement (this Color color)
        {
            var hue = (color.Hue * 359.0);
            var newHue = ((hue + 180) % 359.0);
            var complement = color.WithHue (newHue / 359.0);

            return complement;
        }

        public static Color Invert (this Color color)
        {
            var r = 255 - (int)(255 * color.R);
            var g = 255 - (int)(255 * color.G);
            var b = 255 - (int)(255 * color.B);
            return Color.FromRgb (r, g, b);
        }

        public static void PrintColor (this Color color, string label = null)
        {
            var r = (int)(255 * color.R);
            var g = (int)(255 * color.G);
            var b = (int)(255 * color.B);

            System.Diagnostics.Debug.WriteLine ("{3} := R:{0} G:{1} B:{2}", r, g, b, label);
            System.Diagnostics.Debug.WriteLine (color.ToString ());
        }
    }
}

