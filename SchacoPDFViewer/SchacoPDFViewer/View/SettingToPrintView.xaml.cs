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
using System.Windows.Shapes;

namespace SchacoPDFViewer
{
    /// <summary>
    /// SettingToPrintView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingToPrintView : MetroWindow
    {
        public SettingToPrintView()
        {
            InitializeComponent();
            Register();
        }

        void Register()
        {
            Messenger.Default.Register<SettingToPrinterView_CloseEventArgs>(this, (x) => { this.Close(); });
        }

        void Unregister()
        {
            Messenger.Default.Unregister<SettingToPrinterView_CloseEventArgs>(this);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unregister();
        }
    }
}
