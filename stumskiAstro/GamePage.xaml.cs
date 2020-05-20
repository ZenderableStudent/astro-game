using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace stumskiAstro
{
    public sealed partial class GamePage : Page
    {
		readonly Game1 _game;

		public GamePage()
        {
            this.InitializeComponent();
            ApplicationView.GetForCurrentView().TryResizeView(new Size(480, 800));
            // Create the game.
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }
    }
}
