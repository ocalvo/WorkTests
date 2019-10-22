﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using AppUIBasics.Common;
using AppUIBasics.Data;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AppUIBasics
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private const string SelectedAppThemeKey = "SelectedAppTheme";

        /// <summary>
        /// Gets the current actual theme of the app based on the requested theme of the
        /// root element, or if that value is Default, the requested theme of the Application.
        /// </summary>
        public static ElementTheme ActualTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    if (rootElement.RequestedTheme != ElementTheme.Default)
                    {
                        return rootElement.RequestedTheme;
                    }
                }

                return GetEnum<ElementTheme>(Current.RequestedTheme.ToString());
            }
        }

        /// <summary>
        /// Gets or sets (with LocalSettings persistence) the RequestedTheme of the root element.
        /// </summary>
        public static ElementTheme RootTheme
        {
            get
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    return rootElement.RequestedTheme;
                }

                return ElementTheme.Default;
            }
            set
            {
                if (Window.Current.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }

                ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
            }
        }

        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += App_Resuming;
            this.RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested;

            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6))
            {
                this.FocusVisualKind = AnalyticsInfo.VersionInfo.DeviceFamily == "Xbox" ? FocusVisualKind.Reveal : FocusVisualKind.HighVisibility;
            }
        }

        public void EnableSound(bool withSpatial = false)
        {
            ElementSoundPlayer.State = ElementSoundPlayerState.On;

            if(!withSpatial)
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
            else
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        private void App_Resuming(object sender, object e)
        {
            //switch (NavigationRootPage.RootFrame?.Content)
            //{
            //    case ItemPage itemPage:
            //        itemPage.SetInitialVisuals();
            //        break;
            //    case NewControlsPage newControlsPage:
            //    case AllControlsPage allControlsPage:
            //        NavigationRootPage.Current.NavigationView.AlwaysShowHeader = false;
            //        break;
            //}
        }
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            await ControlInfoDataSource.Instance.GetGroupsAsync();

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(AllControlsPage), e.Arguments);
                    //rootFrame.Navigate(typeof(MiniMainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }


        //        /// <summary>
        //        /// Invoked when the application is launched normally by the end user.  Other entry points
        //        /// will be used such as when the application is launched to open a specific file.
        //        /// </summary>
        //        /// <param name="e">Details about the launch request and process.</param>
        //        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        //        {
        //#if DEBUG
        //            //if (System.Diagnostics.Debugger.IsAttached)
        //            //{
        //            //    this.DebugSettings.EnableFrameRateCounter = true;
        //            //}

        //            if (System.Diagnostics.Debugger.IsAttached)
        //            {
        //                this.DebugSettings.BindingFailed += DebugSettings_BindingFailed;
        //            }
        //#endif
        //            //draw into the title bar
        //            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

        //            await EnsureWindow(args);
        //        }

        //        private void DebugSettings_BindingFailed(object sender, BindingFailedEventArgs e)
        //        {

        //        }

        //protected async override void OnActivated(IActivatedEventArgs args)
        //{
        //    await EnsureWindow(args);

        //    base.OnActivated(args);
        //}

        //private async Task EnsureWindow(IActivatedEventArgs args)
        //{
        //    // No matter what our destination is, we're going to need control data loaded - let's knock that out now.
        //    // We'll never need to do this again.
        //    await ControlInfoDataSource.Instance.GetGroupsAsync();

        //    Frame rootFrame = GetRootFrame();

        //    string savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();

        //    if (savedTheme != null)
        //    {
        //        RootTheme = GetEnum<ElementTheme>(savedTheme);
        //    }

        //    Type targetPageType = typeof(NewControlsPage);
        //    string targetPageArguments = string.Empty;

        //    if (args.Kind == ActivationKind.Launch)
        //    {
        //        if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
        //        {
        //            try
        //            {
        //                await SuspensionManager.RestoreAsync();
        //            }
        //            catch (SuspensionManagerException)
        //            {
        //                //Something went wrong restoring state.
        //                //Assume there is no state and continue
        //            }
        //        }

        //        targetPageArguments = ((LaunchActivatedEventArgs)args).Arguments;
        //    }
        //    else if (args.Kind == ActivationKind.Protocol)
        //    {
        //        Match match;

        //        string targetId = string.Empty;

        //        switch (((ProtocolActivatedEventArgs)args).Uri?.AbsolutePath)
        //        {
        //            case string s when IsMatching(s, "/category/(.*)"):
        //                targetId = match.Groups[1]?.ToString();
        //                if (ControlInfoDataSource.Instance.Groups.Any(g => g.UniqueId == targetId))
        //                {
        //                    targetPageType = typeof(SectionPage);
        //                }
        //                break;

        //            case string s when IsMatching(s, "/item/(.*)"):
        //                targetId = match.Groups[1]?.ToString();
        //                if (ControlInfoDataSource.Instance.Groups.Any(g => g.Items.Any(i => i.UniqueId == targetId)))
        //                {
        //                    targetPageType = typeof(ItemPage);
        //                }
        //                break;
        //        }

        //        targetPageArguments = targetId;

        //        bool IsMatching(string parent, string expression)
        //        {
        //            match = Regex.Match(parent, expression);
        //            return match.Success;
        //        }
        //    }

        //    rootFrame.Navigate(targetPageType, targetPageArguments);
        //    ((Microsoft.UI.Xaml.Controls.NavigationViewItem)(((NavigationRootPage)(Window.Current.Content)).NavigationView.MenuItems[0])).IsSelected = true;

        //    // Ensure the current window is active
        //    Window.Current.Activate();
        //}

        //private Frame GetRootFrame()
        //{
        //    Frame rootFrame;
        //    NavigationRootPage rootPage = Window.Current.Content as NavigationRootPage;
        //    if (rootPage == null)
        //    {
        //        rootPage = new NavigationRootPage();
        //        rootFrame = (Frame)rootPage.FindName("rootFrame");
        //        if (rootFrame == null)
        //        {
        //            throw new Exception("Root frame not found");
        //        }
        //        SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
        //        rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
        //        rootFrame.NavigationFailed += OnNavigationFailed;

        //        Window.Current.Content = rootPage;
        //    }
        //    else
        //    {
        //        rootFrame = (Frame)rootPage.FindName("rootFrame");
        //    }

        //    return rootFrame;
        //}

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }
    }
}
