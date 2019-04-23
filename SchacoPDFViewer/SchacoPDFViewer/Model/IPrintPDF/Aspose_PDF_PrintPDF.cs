using Aspose.Pdf.Facades;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class Aspose_PDF_PrintPDF : IPrintPDF
    {
        PdfViewer viewer = null;

        public void Print(string pdfFileName, string printer, bool IsDupex = true)
        {
            viewer = new PdfViewer();

            viewer.BindPdf(pdfFileName);
            viewer.AutoResize = true;
            viewer.AutoRotate = true;
            viewer.PrintPageDialog = false;

            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printer;
            ps.Duplex = IsDupex ? Duplex.Vertical : Duplex.Default;

            viewer.PrintDocumentWithSettings(ps);
        }
    }
}
