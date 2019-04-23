using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class SumatraPDFPrintPDF : IPrintPDF
    {
        public SumatraPDFPrintPDF()
        {
        }
        public void Print(string pdfFileName, string printer, bool IsDupex = true)
        {
            string pdfFile = "";
            if (!File.Exists(pdfFileName))
            {
                return;
            }

            //Process p = new Process();
            //StringBuilder sb = new StringBuilder();
            //sb.Append(string.Format(" -print-to \"{0}\" ", dialogPrint.PrinterSettings.PrinterName));
            //sb.Append(" -print-settings  \"");

            //if (dialogPrint.PrinterSettings.Duplex == Duplex.Simplex)
            //{
            //    sb.Append(" simplex ");
            //}
            //else if (dialogPrint.PrinterSettings.Duplex == Duplex.Vertical)
            //{
            //    sb.Append(" duplexlong ");
            //}
            //else if (dialogPrint.PrinterSettings.Duplex == Duplex.Horizontal)
            //{
            //    sb.Append(" duplexshort ");
            //}

            //if (dialogPrint.PrinterSettings.Copies > 1)
            //{
            //    sb.Append(string.Format(" {0}x ", dialogPrint.PrinterSettings.Copies));
            //}
            //sb.Append("\" ");

            //p.StartInfo = new ProcessStartInfo(@".\SumatraPDF.exe", sb.ToString() + "\"" + pdfFile + "\"");
            //p.StartInfo.UseShellExecute = false;
            //p.Start();
        }
    }
}
