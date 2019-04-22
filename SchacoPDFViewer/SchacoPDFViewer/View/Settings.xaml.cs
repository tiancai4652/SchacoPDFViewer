using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();
            Register();
        }

        public void Register()
        {
            Messenger.Default.Register<SeetingView_ShowMsgEventArgs>(this, ShowMsg);
            Messenger.Default.Register<SettingView_HideEventArgs>(this, (x)=> { this.Hide(); });
            Messenger.Default.Register<SettingView_ShowFromHideEventArgs>(this, (x) => { this.Show(); });
        }

        public void Unregister()
        {
            Messenger.Default.Unregister<SeetingView_ShowMsgEventArgs>(this);
            Messenger.Default.Send(this, new SettingView_UnregisterVM());
            Messenger.Default.Unregister<SettingView_UnregisterVM>(this);
            Messenger.Default.Unregister<SettingView_HideEventArgs>(this);
            Messenger.Default.Unregister<SettingView_ShowFromHideEventArgs>(this);
        }


        public void ShowMsg(SeetingView_ShowMsgEventArgs agrs)
        {
            DialogManager.ShowMessageAsync(this, "提示", agrs.Msg);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Unregister();
        }
    }
}
