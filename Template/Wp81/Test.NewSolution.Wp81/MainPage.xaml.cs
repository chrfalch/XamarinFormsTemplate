/************************************************************************
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2025 - Christian Falch
 * 
 * Permission is hereby granted, free of charge, to any person obtaining 
 * a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject 
 * to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NControl.Plugins.WP81;
using Test.NewSolution.Wp81.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Test.NewSolution.Wp81.Platform.IoC;
using Test.NewSolution.FormsApp.Mvvm;
using Test.NewSolution.Wp81.Platform.Mvvm;
using Test.NewSolution.Wp81.Platform.Repositories;
using Test.NewSolution.Contracts.Repositories;

namespace Test.NewSolution.Wp81
{
    public partial class MainPage : FormsApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();            
            NControlViewRenderer.Init();

            LoadApplication(new Test.NewSolution.FormsApp.App(new ContainerProvider(), (container) =>
            {
                // Register providers
                container.Register<IRepositoryProvider, RepositoryProvider>();
                container.Register<IImageProvider, ImageProvider>();
            }));
        }
    }
}