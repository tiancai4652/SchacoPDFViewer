using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer.Model
{
    public class AsposeE2P : IExcelToPDF
    {
        public AsposeE2P()
        {
        }

        public void TurnToPDF(string sourceExcelFile, string targetPdfFile)
        {
            try
            {
                Workbook wb = new Workbook(sourceExcelFile);
                wb.Save(targetPdfFile, SaveFormat.Pdf);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
