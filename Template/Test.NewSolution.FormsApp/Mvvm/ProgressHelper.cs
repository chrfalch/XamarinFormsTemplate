using System;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace Test.NewSolution.FormsApp.Mvvm
{
    /// <summary>
    /// Progress helper.
    /// </summary>
    public static class ProgressHelper
    {
        /// <summary>
        /// The progress dialog.
        /// </summary>
        private static IProgressDialog _progressDialog;

        /// <summary>
        /// Shows the progress.
        /// </summary>
        /// <param name="visible">If set to <c>true</c> visible.</param>
        /// <param name="title">Title.</param>
        /// <param name="subtitle">Subtitle.</param>
        public static void UpdateProgress(bool visible, string title = "", string subtitle = "")
        {
            if (_progressDialog == null && visible == false)
                return;

            if (_progressDialog == null)
            {
                _progressDialog = UserDialogs.Instance.Progress();
                _progressDialog.IsDeterministic = false;
            }

            _progressDialog.Title = title ?? string.Empty;

            if (visible)
                _progressDialog.Show();
            else
            {
                _progressDialog.Hide();
                _progressDialog = null;
            }
        }
    }
}

