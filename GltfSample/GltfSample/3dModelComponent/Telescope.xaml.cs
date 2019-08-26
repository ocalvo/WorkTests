using SceneLoaderComponent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Composition;
using Windows.UI.Composition.Scenes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TelescopeModelComponent
{
    public sealed partial class Telescope : UserControl
    {
        private readonly Compositor _compositor;
        private readonly ContainerVisual _hostVisual;
        private readonly SceneVisual _sceneVisual;

        public Telescope()
        {
            this.InitializeComponent();
            _compositor = Window.Current.Compositor;
            _hostVisual = _compositor.CreateContainerVisual();
            _sceneVisual = SceneVisual.Create(_compositor);
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("ms-appx:///Model/Telescope.gltf");
            var storageFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var buffer = await FileIO.ReadBufferAsync(storageFile);
            var loader = new SceneLoader();
            var sceneNode = loader.Load(buffer, _compositor);

            _hostVisual.RelativeSizeAdjustment = Vector2.One;
            ElementCompositionPreview.SetElementChildVisual(ModelHost, _hostVisual);
            _sceneVisual.Scale = new Vector3(2.5f, 2.5f, 1.0f);
            _sceneVisual.Root = SceneNode.Create(_compositor);
            _sceneVisual.Root.Children.Clear();
            _sceneVisual.Root.Children.Add(sceneNode);


            var rotationAnimation = _compositor.CreateScalarKeyFrameAnimation();
            rotationAnimation.InsertKeyFrame(1.0f, 360.0f, _compositor.CreateLinearEasingFunction());
            rotationAnimation.Duration = TimeSpan.FromSeconds(16);
            rotationAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
            _sceneVisual.Root.Transform.RotationAxis = new Vector3(0.0f, 1.0f, 0.2f); ;
            _sceneVisual.Root.Transform.StartAnimation(nameof(SceneNode.Transform.RotationAngleInDegrees), rotationAnimation);

            _hostVisual.Children.InsertAtTop(_sceneVisual);
        }


        private void ModelHost_SizeChanged(object sender, SizeChangedEventArgs e) =>
            UpdateModelPosition();

        private void UpdateModelPosition() =>
            _sceneVisual.Offset = new Vector3((float)ModelHost.ActualWidth / 2, (float)ModelHost.ActualHeight / 2.5f, 0.0f);
    }
}
