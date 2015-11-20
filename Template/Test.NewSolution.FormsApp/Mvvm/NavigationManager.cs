﻿/****************************** Module Header ******************************\
Module Name:  NavigationHelper.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using Test.NewSolution.FormsApp.ViewModels;

namespace Test.NewSolution.FormsApp.Mvvm
{
    /// <summary>
    /// Navigation helper.
    /// </summary>
    public static class NavigationManager
    {
        #region Private Members

        /// <summary>
        /// The navigation page stack.
        /// </summary>
        private static Stack<NavigationElement> _navigationPageStack = new Stack<NavigationElement>();

        /// <summary>
        /// The master detail page.
        /// </summary>
        private static MasterDetailPage _masterDetailPage;

        #endregion

        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <returns>The main page.</returns>
        /// <param name="mainPage">Main page.</param>
        public static Page SetMainPage(Page mainPage)
        {
            if(_navigationPageStack.Any())
                _navigationPageStack.Pop();

            _navigationPageStack.Push(new NavigationElement{Page = mainPage});
            return _navigationPageStack.Peek().Page;
        }

        /// <summary>
        /// Sets the master detail master.
        /// </summary>
        /// <param name="page">Page.</param>
        public static void SetMasterDetailMaster(MasterDetailPage page)
        {
            _masterDetailPage = page;
        }

        /// <summary>
        /// Toggles the drawer.
        /// </summary>
        public static void ToggleDrawer()
        {
            if (_masterDetailPage == null)
                return;

            _masterDetailPage.IsPresented = !_masterDetailPage.IsPresented;
        }

        #region Regular Navigation

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync(Type viewModelType)             
        {       
            return NavigateToViewModelAsync(viewModelType, null);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync(Type viewModelType, bool animate)             
        {       
            return NavigateToViewModelAsync(viewModelType, null, animate);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync<TViewModel>() 
            where TViewModel : BaseViewModel
        {       
            return NavigateToViewModelAsync<TViewModel>(null);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync<TViewModel>(bool animate) 
            where TViewModel : BaseViewModel
        {       
            return NavigateToViewModelAsync<TViewModel>(null, animate);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync<TViewModel>(object parameter) 
            where TViewModel : BaseViewModel
        {       
            return NavigateToViewModelAsync(typeof(TViewModel), parameter);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync<TViewModel>(object parameter, bool animate) 
            where TViewModel : BaseViewModel
        {       
            return NavigateToViewModelAsync(typeof(TViewModel), parameter, animate);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task NavigateToViewModelAsync(Type viewModelType, object parameter)
        {
            return NavigateToViewModelAsync(viewModelType, parameter, true);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static async Task NavigateToViewModelAsync(Type viewModelType, object parameter, bool animate)
        {
            if (_masterDetailPage != null)
                _masterDetailPage.IsPresented = false;

            var view = ViewManager.GetViewFromViewModel(viewModelType);

            if (parameter != null)
            {
                var viewWithParameter = view as IViewWithParameter;
                if (viewWithParameter != null)
                    await viewWithParameter.InitializeAsync (parameter);
            }

            await _navigationPageStack.Peek().Page.Navigation.PushAsync (view, animate);
        }

        /// <summary>
        /// Dismisses the view model async.
        /// </summary>
        /// <returns>The view model async.</returns>
        public static async Task DismissViewModelAsync()
        {
            if(await _navigationPageStack.Peek().Page.Navigation.PopAsync() == null)
            {
                _navigationPageStack.Pop();
                await DismissViewModelAsync();
            }
        }

        #endregion

        #region Modal Navigation

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<NavigationPage> NavigateModalToViewModelAsync<TViewModel>(object parameter) 
            where TViewModel : BaseViewModel
        {       
            return NavigateModalToViewModelAsync(typeof(TViewModel), null, parameter);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<NavigationPage> NavigateModalToViewModelAsync<TViewModel>(
            Action<bool> dismissedCallback, object parameter) 
            where TViewModel : BaseViewModel
        {       
            return NavigateModalToViewModelAsync(typeof(TViewModel), dismissedCallback, parameter);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static async Task<NavigationPage> NavigateModalToViewModelAsync(
            Type viewModelType, Action<bool> dismissedCallback, object parameter)
        {       
            if (_masterDetailPage != null)
                _masterDetailPage.IsPresented = false;

            var view = ViewManager.GetViewFromViewModel(viewModelType);
            var retVal = new NavigationPage (view);

            if (parameter != null)
            {
                var viewWithParameter = view as IViewWithParameter;
                if (viewWithParameter != null)
                    await viewWithParameter.InitializeAsync(parameter);
            }

            await _navigationPageStack.Peek().Page.Navigation.PushModalAsync (retVal);

            _navigationPageStack.Push(new NavigationElement{
                Page = retVal, DismissedAction = dismissedCallback
            });

            return retVal;
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<NavigationPage> NavigateModalToViewModelAsync<TViewModel>(Action<bool> dismissedCallback) 
            where TViewModel : BaseViewModel
        {       
            return NavigateModalToViewModelAsync<TViewModel>(dismissedCallback, null);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<NavigationPage> NavigateModalToViewModelAsync<TViewModel>() 
            where TViewModel : BaseViewModel
        {       
            return NavigateModalToViewModelAsync<TViewModel>(null);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<NavigationPage> NavigateModalToViewModelAsync(Type viewModelType, Action<bool> dismissedCallback)             
        {       
            return NavigateModalToViewModelAsync(viewModelType, dismissedCallback, null);
        }

        /// <summary>
        /// Navigates to the provided view model of type
        /// </summary>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Task<NavigationPage> NavigateModalToViewModelAsync(Type viewModelType)
        {       
            return NavigateModalToViewModelAsync(viewModelType, null);
        }

        /// <summary>
        /// Pops the active modal view 
        /// </summary>
        /// <returns>The modal view model async.</returns>
        public static async Task PopModalViewModelAsync(bool success)
        {
            var poppedPage = await _navigationPageStack.Peek().Page.Navigation.PopModalAsync ();
            if(poppedPage == _navigationPageStack.Peek().Page)
            {
                var tempNavigationElement = _navigationPageStack.Peek ();
                _navigationPageStack.Pop ();
                if (tempNavigationElement.DismissedAction != null)
                    tempNavigationElement.DismissedAction (success);
            }
        }

        #endregion
    }

    /// <summary>
    /// Navigation Element Helper 
    /// </summary>
    internal class NavigationElement
    {
        public Page Page {get;set;}
        public Action<bool> DismissedAction { get; set; }
    }
}

