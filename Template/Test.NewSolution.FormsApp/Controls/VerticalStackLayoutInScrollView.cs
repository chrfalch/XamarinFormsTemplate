using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Test.NewSolution.FormsApp.Controls
{
    /// <summary>
    /// Implements a scrollview with a children property that adds to an inner stack layout
    /// </summary>
    public class VerticalStackLayoutInScrollView: ScrollView
    {
        #region Private Members

        /// <summary>
        /// The stack.
        /// </summary>
        private StackLayout _stack;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Sin4U.FormsApp.Controls.VerticalStackLayoutInScrollView"/> class.
        /// </summary>
        public VerticalStackLayoutInScrollView()
        {
            _stack = new StackLayout{ 
                Padding = 14,
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
            };

            Content = _stack;
        }

        #region Properties

        /// <summary>
        /// Gets a read-only list of the immediate children of the renderer that implements the interface.
        /// </summary>
        /// <value>To be added.</value>
        /// <remarks>To be added.</remarks>
        public IList<View> Children {
            get { return _stack.Children; }
        }

        #endregion
    }
}

