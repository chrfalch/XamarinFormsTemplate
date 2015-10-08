/****************************** Module Header ******************************\
Module Name:  ContainerHelper.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using Xamarin.Forms;
using Test.NewSolution;

namespace Test.NewSolution.FormsApp.IoC
{
    /// <summary>
    /// Container helper.
    /// </summary>
    public static class Container
    {
        #region Private Static Members

        /// <summary>
        /// Instance
        /// </summary>
        private static IContainerProvider _containerProvider;

        #endregion

        /// <summary>
        /// Initialize the specified provider.
        /// </summary>
        /// <param name="provider">Provider.</param>
        public static void Initialize(IContainerProvider provider)
        {
            _containerProvider = provider;
        }

        #region Members

        /// <summary>
        /// Resolves the given type into an instance
        /// </summary>
        /// <typeparam name="TTypeToResolve">The 1st type parameter.</typeparam>
        public static TTypeToResolve Resolve<TTypeToResolve>() where TTypeToResolve: class
        {
            return _containerProvider.Resolve<TTypeToResolve>();
        }

        /// <summary>
        /// Resolves the given type into an instance
        /// </summary>
        /// <typeparam name="TTypeToResolve">The 1st type parameter.</typeparam>
        public static object Resolve(Type typeToResolve)
        {
            return _containerProvider.Resolve(typeToResolve);
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void Register<RegisterType2, RegisterImplementation> () where RegisterType2 : class 
            where RegisterImplementation : class, RegisterType2
        {
            _containerProvider.Register<RegisterType2, RegisterImplementation>();
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void Register<RegisterType, RegisterImplementation> (RegisterImplementation implementation) 
            where RegisterType : class 
            where RegisterImplementation : class, RegisterType
        {
            _containerProvider.Register<RegisterType, RegisterImplementation>(implementation);
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void Register(Type registerType, Type registerImplementation)
        {
            _containerProvider.Register(registerType, registerImplementation);
        }

        /// <summary>
        /// Register a type as a singleton in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void RegisterSingleton<RegisterType, RegisterImplementation> () where RegisterType : class 
            where RegisterImplementation : class, RegisterType
        {
            _containerProvider.RegisterSingleton<RegisterType, RegisterImplementation>();
        }

        /// <summary>
        /// Register a type as a singleton in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void RegisterSingleton(Type registerType, Type registerImplementation)
        {
            _containerProvider.RegisterSingleton(registerType, registerImplementation);
        }
        #endregion
    }
}

