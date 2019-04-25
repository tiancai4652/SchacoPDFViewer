using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class Spire_XLS_PrintExcel : IPrintExcel
    {
        public void Print(string pdfFileName, string printer, bool IsDupex = true)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(pdfFileName);
            Helper.SetDefaultPrinter(printer);
            PrintDocument pd = workbook.PrintDocument;


            //PrinterSettings ps = new PrinterSettings();
            //ps.Duplex = IsDupex? Duplex.Vertical: Duplex.Default;
            //ps.PrinterName = printer;
            //pd.PrinterSettings = ps;

        

            pd.Print();
        }
    }
}
