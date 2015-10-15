using System;
using Xamarin.Forms;
using System.Linq;

namespace Test.NewSolution.FormsApp.Controls
{
    /// <summary>
    /// Full width horizontal stack layout.
    /// </summary>
    public class FullWidthHorizontalStackLayout: StackLayout
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sin4U.FormsApp.Controls.FullWidthHorizontalStackLayout"/> class.
        /// </summary>
        public FullWidthHorizontalStackLayout()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 10;
            Padding = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        /// <param name="x">A value representing the x coordinate of the child region bounding box.</param>
        /// <param name="y">A value representing the y coordinate of the child region bounding box.</param>
        /// <param name="width">A value representing the width of the child region bounding box.</param>
        /// <param name="height">A value representing the height of the child region bounding box.</param>
        /// <summary>
        /// Positions and sizes the children of a Layout.
        /// </summary>
        /// <remarks>Implementors wishing to change the default behavior of a Layout should override this method. It is suggested
        /// to still call the base method and modify its calculated results.</remarks>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);

            // Update widths
            var newwidth = (width/Children.Count);
            newwidth = newwidth - (Spacing / Children.Count);

            var newx = 0.0;
            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children.ElementAt(i);
                child.Layout(new Rectangle(newx, child.Bounds.Top, newwidth, height));
                newx = newx + newwidth + Spacing;
            }
        }
    }
}

