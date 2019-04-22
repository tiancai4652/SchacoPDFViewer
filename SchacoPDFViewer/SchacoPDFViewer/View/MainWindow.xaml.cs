using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchacoPDFViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Register();
        }

        public void Register()
        {
            Messenger.Default.Register<MainView_ShowSelectedPDFEventArgs>(this, ShowPDf);
        }

        public void Unregister()
        {
            Messenger.Default.Unregister<MainView_ShowSelectedPDFEventArgs>(this);
            Messenger.Default.Send(this, new MainView_UnregisterVM());
            Messenger.Default.Unregister<MainView_UnregisterVM>(this);

        }

        //private void TreeView_SelectedItemChanged(object sender, RoutedEventArgs e)
        //{
        //    Messenger.Default.Send<object>((e.OriginalSource as TreeViewItem).DataContext, MvvmMessage.MainView_SelectedChange);
        //}

        private void TreeView_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Messenger.Default.Send( new MainView_SelectedChangeEventArgs() {MyTreeNode= (sender as TreeView).SelectedItem as MyTreeNode});
        }

        void ShowPDf(MainView_ShowSelectedPDFEventArgs args)
        {
            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    moonPdfPanel.OpenFile(args.PDFPath);
                    _isLoaded = true;
                }));
            }
            catch (Exception ex)
            {
                _isLoaded = false;
            }
        }


        private bool _isLoaded = false;
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                string filePath = dialog.FileName;

                try
                {
                    moonPdfPanel.OpenFile(filePath);
                    _isLoaded = true;
                }
                catch (Exception ex)
                {
                    _isLoaded = false;
                }
            }
        }

        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomIn();
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.ZoomOut();
            }
        }

        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded)
            {
                moonPdfPanel.Zoom(1.0);
            }
        }

        private void FitToHeightButton_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.ZoomToHeight();
        }

        private void FacingButton_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.ViewType = MoonPdfLib.ViewType.Facing;
        }

        private void SinglePageButton_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPanel.ViewType = MoonPdfLib.ViewType.SinglePage;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unregister();
        }
    }
}
