﻿#pragma checksum "C:\Users\Dennis\Dropbox\Projects\Entrance\Entrance\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "00EA840797F997ECC03A3575D9040AB8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Entrance {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard TranslationStoryboard;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Image PodcastLogoContainer;
        
        internal System.Windows.Media.TranslateTransform EpisodeLogoTranslate;
        
        internal System.Windows.Controls.Image DownloadedLogo;
        
        internal System.Windows.Media.TranslateTransform DownloadLogoTranslate;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Entrance;component/MainPage.xaml", System.UriKind.Relative));
            this.TranslationStoryboard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("TranslationStoryboard")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.PodcastLogoContainer = ((System.Windows.Controls.Image)(this.FindName("PodcastLogoContainer")));
            this.EpisodeLogoTranslate = ((System.Windows.Media.TranslateTransform)(this.FindName("EpisodeLogoTranslate")));
            this.DownloadedLogo = ((System.Windows.Controls.Image)(this.FindName("DownloadedLogo")));
            this.DownloadLogoTranslate = ((System.Windows.Media.TranslateTransform)(this.FindName("DownloadLogoTranslate")));
        }
    }
}

