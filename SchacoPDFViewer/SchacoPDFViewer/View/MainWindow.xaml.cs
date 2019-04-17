using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
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
        }

        //private void TreeView_SelectedItemChanged(object sender, RoutedEventArgs e)
        //{
        //    Messenger.Default.Send<object>((e.OriginalSource as TreeViewItem).DataContext, MvvmMessage.MainView_SelectedChange);
        //}

        private void TreeView_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Messenger.Default.Send<object>((sender as TreeView).SelectedItem, MvvmMessage.MainView_SelectedChange);
        }
    }
}
