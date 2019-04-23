using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Input;

namespace SchacoPDFViewer.ViewModel
{
    public class SettingsViewModel: ViewModelBase
    {
        ObservableCollection<ExeclToPdfType> _E2PCollection = new ObservableCollection<ExeclToPdfType>() { ExeclToPdfType.Aspose, ExeclToPdfType.Office };
        public ObservableCollection<ExeclToPdfType> E2PCollection
        {
            get
            {
                return _E2PCollection;
            }
            set
            {
                _E2PCollection = value;
                RaisePropertyChanged(() => E2PCollection);
            }
        }

        ObservableCollection<PrintPdfType> _PrintPDFCollection = new ObservableCollection<PrintPdfType>() { PrintPdfType.SumatraPDF };
        public ObservableCollection<PrintPdfType> PrintPDFCollection
        {
            get
            {
                return _PrintPDFCollection;
            }
            set
            {
                _PrintPDFCollection = value;
                RaisePropertyChanged(() => PrintPDFCollection);
            }
        }

        PrintPdfType _SelectedPrintPdfType = PrintPdfType.SumatraPDF;
        public PrintPdfType SelectedPrintPdfType
        {
            get
            {
                return _SelectedPrintPdfType;
            }
            set
            {
                _SelectedPrintPdfType = value;
                RaisePropertyChanged(() => SelectedPrintPdfType);
            }
        }

        ObservableCollection<PrintExcelType> _PrintExcelCollection = new ObservableCollection<PrintExcelType>() { PrintExcelType.Aspose };
        public ObservableCollection<PrintExcelType> PrintExcelCollection
        {
            get
            {
                return _PrintExcelCollection;
            }
            set
            {
                _PrintExcelCollection = value;
                RaisePropertyChanged(() => PrintExcelCollection);
            }
        }

        PrintExcelType _SelectedPrintExcelType = PrintExcelType.Aspose;
        public PrintExcelType SelectedPrintExcelType
        {
            get
            {
                return _SelectedPrintExcelType;
            }
            set
            {
                _SelectedPrintExcelType = value;
                RaisePropertyChanged(() => SelectedPrintExcelType);
            }
        }

        ExeclToPdfType _SelectedExeclToPdfType = ExeclToPdfType.Aspose;
        public ExeclToPdfType SelectedExeclToPdfType
        {
            get
            {
                return _SelectedExeclToPdfType;
            }
            set
            {
                _SelectedExeclToPdfType = value;
                RaisePropertyChanged(() => SelectedExeclToPdfType);
            }
        }

        string _FolderPath = Default.DefaultFolderPath;
        public string FolderPath
        {
            get
            {
                return _FolderPath;
            }
            set
            {
                _FolderPath = value;
                RaisePropertyChanged(() => FolderPath);
            }
        }

        public SettingsViewModel()
        {
            NextCommand = new RelayCommand(Next);
            OpenDialogCommand = new RelayCommand(OpenDialog);
            Register();
        }

        public ICommand NextCommand { get; set; }
        void Next()
        {

            try
            {
                if (!Directory.Exists(FolderPath))
                {
                    Messenger.Default.Send(new SeetingView_ShowMsgEventArgs() {  Msg= "不存在该路径!" });
                    return;
                }
                if (!Directory.Exists(FolderPath + "\\" + Default.TempPDFFolder))
                {
                    Directory.CreateDirectory(FolderPath + "\\" + Default.TempPDFFolder);
                }

                DirectoryInfo di = new DirectoryInfo(FolderPath + "\\" + Default.TempPDFFolder);
                System.Security.AccessControl.DirectorySecurity dirSecurity = di.GetAccessControl();
                dirSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                dirSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                di.SetAccessControl(dirSecurity);



                switch (SelectedPrintPdfType)
                {
                    case PrintPdfType.SumatraPDF:
                        SimpleIoc.Default.Register<IPrintPDF, SumatraPDFPrintPDF>(true);
                        break;
                 
                    default:
                        break;
                }
                switch (SelectedPrintExcelType)
                {
                    case  PrintExcelType.Aspose:
                        SimpleIoc.Default.Register<IPrintExcel, AsposePrintExcel>(true);
                        break;

                    default:
                        break;
                }
                switch (SelectedExeclToPdfType)
                {
                    case ExeclToPdfType.Aspose:
                        SimpleIoc.Default.Register<IExcelToPDF, AsposeE2P>(true);
                        break;
                    case ExeclToPdfType.Office:
                        break;
                    default:
                        break;
                }
                MainWindow v = new MainWindow();
                Messenger.Default.Send<SettingView_HideEventArgs>(new SettingView_HideEventArgs());
                v.ShowDialog();
                ViewModelLocator.CleanIOC();
                Messenger.Default.Send<SettingView_ShowFromHideEventArgs>(new SettingView_ShowFromHideEventArgs());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (Directory.Exists(FolderPath + "\\" + Default.TempPDFFolder))
                {
                    Directory.Delete(FolderPath + "\\" + Default.TempPDFFolder);
                }
            }
        }

        public ICommand OpenDialogCommand { get; set; }
        void OpenDialog()
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择Txt所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    Messenger.Default.Send(new SeetingView_ShowMsgEventArgs() { Msg = "选择的文件夹不能为空!" });
                    return;
                }

                FolderPath = dialog.SelectedPath;
            }
        }

        void Unregister()
        {
            Messenger.Default.Unregister<SettingView_UnregisterVM>(this);
        }

        void Register()
        {
            Messenger.Default.Register<SettingView_UnregisterVM>(this, (t)=> Unregister());
        }
    }

    

   
     
}
