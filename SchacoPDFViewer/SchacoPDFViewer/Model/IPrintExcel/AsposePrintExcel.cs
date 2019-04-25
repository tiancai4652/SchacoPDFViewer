using Aspose.Cells;
using Aspose.Cells.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class AsposePrintExcel : IPrintExcel
    {
        public AsposePrintExcel()
        { }
        public void Print(string pdfFileName, string printer, bool IsDupex = true)
        {
            if (!File.Exists(pdfFileName))
            {
                return;
            }
            Workbook workbook = new Workbook(pdfFileName);
            Worksheet worksheet = workbook.Worksheets[0];
            //Aspose.Cells.Rendering.ImageOrPrintOptions options = new Aspose.Cells.Rendering.ImageOrPrintOptions();
            //options.PrintingPage = PrintingPageType.Default;
            WorkbookRender wr = new WorkbookRender(workbook, new ImageOrPrintOptions());

            //PrinterSettings setting = new PrinterSettings();
            ////在界面设置里设置不管用 设置双面打印
            //setting.Duplex = IsDupex ? Duplex.Vertical : Duplex.Default;
            //setting.PrinterName = printer;
            Helper.SetDefaultPrinter(printer);
            //wr.ToPrinter(setting);
            wr.ToPrinter(printer);
        }

       



    }
}
