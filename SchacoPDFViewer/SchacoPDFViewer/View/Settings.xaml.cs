using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SchacoPDFViewer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
            Messenger.Default.Register<string>(this, MvvmMessage.SeetingView_ShowMsg, ShowMsg);
            Messenger.Default.Register<string>(this, MvvmMessage.SeetingView_ShowMain, ShowMain);
        }

        public void ShowMsg(string msg)
        {
            DialogManager.ShowMessageAsync(this, "提示", msg);
        }

        public void ShowMain(string x)
        {
            try
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    MainWindow v = new MainWindow();
                    v.ShowDialog();
                }));
            }
            catch (Exception ex)
            { }
            finally
            {
                ViewModelLocator.Cleanup();
                Messenger.Default.Send<string>("", MvvmMessage.SeetingView_LoadedDIR);
            }
        }
    }
}
