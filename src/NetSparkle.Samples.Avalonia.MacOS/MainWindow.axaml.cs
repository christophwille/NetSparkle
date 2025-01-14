﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.SignatureVerifiers;
using System.IO;

namespace NetSparkleUpdater.Samples.Avalonia
{
    public partial class MainWindow : Window
    {
        private SparkleUpdater _sparkle;

        public MainWindow()
        {
            InitializeComponent();
            // set icon in project properties!
            string manifestModuleName = System.Reflection.Assembly.GetEntryAssembly().ManifestModule.FullyQualifiedName;
            _sparkle = new CustomSparkleUpdater("https://netsparkleupdater.github.io/NetSparkle/files/sample-app-macos/appcast.xml", new Ed25519Checker(Enums.SecurityMode.Strict, "8zPswEwycU7XQ7OcGQtI/b22pWo1qM2Ual2OhssaDyI="))
            {
                UIFactory = new NetSparkleUpdater.UI.Avalonia.UIFactory(Icon),
                // Avalonia version doesn't support separate threads: https://github.com/AvaloniaUI/Avalonia/issues/3434#issuecomment-573446972
                ShowsUIOnMainThread = true,
                LogWriter = new LogWriter(LogWriterOutputMode.Console)
                //UseNotificationToast = false // Avalonia version doesn't yet support notification toast messages
            };
            // TLS 1.2 required by GitHub (https://developer.github.com/changes/2018-02-01-weak-crypto-removal-notice/)
            _sparkle.SecurityProtocolType = System.Net.SecurityProtocolType.Tls12;
            _sparkle.StartLoop(true, true);
        }

        public async void ManualUpdateCheck_Click(object sender, RoutedEventArgs e)
        {
            await _sparkle.CheckForUpdatesAtUserRequest();
        }
    }
}
