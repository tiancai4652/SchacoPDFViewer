using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SchacoPDFViewer.ViewModel
{
    public class SettingToPrintViewModel: ViewModelBase
    {
        ObservableCollection<string> _PrinterList = new ObservableCollection<string>();
        public ObservableCollection<string> PrinterList
        {
            get
            {
                return _PrinterList;
            }
            set
            {
                _PrinterList = value;
                RaisePropertyChanged(() => PrinterList);
            }
        }

        public IPrintPDF PDFPrinter { get; set; }
        public IPrintExcel ExcelPrinter { get; set; }

        public List<string> PDFFilesToPrint { get; set; }

        string _SelectedPrinter = "";
        public string SelectedPrinter
        {
            get
            {
                return _SelectedPrinter;
            }
            set
            {
                _SelectedPrinter = value;
                RaisePropertyChanged(() => SelectedPrinter);
            }
        }

        bool _IsSetDuplex = true;
        public bool IsSetDuplex
        {
            get
            {
                return _IsSetDuplex;
            }
            set
            {
                _IsSetDuplex = value;
                RaisePropertyChanged(() => IsSetDuplex);
            }
        }

        public SettingToPrintViewModel(IPrintPDF pDFPrinter, IPrintExcel excelPrinter)
        {
            PDFPrinter = pDFPrinter;
            ExcelPrinter = excelPrinter;
            //PDFFilesToPrint = pDFFilesToPrint;
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                PrinterList.Add(item.ToString());
            }
            PropertyCommand = new RelayCommand(PropertySet);
            OKCommand = new RelayCommand(OK);
            CancelCommand = new RelayCommand(Cancel);
        }

        public ICommand PropertyCommand { get; set; }
        void PropertySet()
        {
            if (string.IsNullOrEmpty(SelectedPrinter))
            {
                return;
            }
            string pcName = System.Environment.MachineName;
            string printerName = SelectedPrinter;
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("rundll32.exe", $@"printui.dll,PrintUIEntry /e /n\\{pcName}\\" + $"\"{printerName}\"");
            p.StartInfo.UseShellExecute = false;
            p.Start();
        }

        public ICommand OKCommand { get; set; }
        void OK()
        {
            foreach (var item in PDFFilesToPrint)
            {
                if (item.ToLower().Contains(".pdf") && PDFPrinter != null)
                {
                    PDFPrinter.Print(item, SelectedPrinter, IsSetDuplex);
                }
                else if (ExcelPrinter != null)
                {
                    ExcelPrinter.Print(item, SelectedPrinter, IsSetDuplex);
                }
            }
        }

        public ICommand CancelCommand { get; set; }
        void Cancel()
        {
            Messenger.Default.Send<SettingToPrinterView_CloseEventArgs>(new SettingToPrinterView_CloseEventArgs()); 
        }
    }
}
