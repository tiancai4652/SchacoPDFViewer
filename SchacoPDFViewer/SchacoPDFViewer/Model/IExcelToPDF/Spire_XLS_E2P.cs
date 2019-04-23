using Spire.Pdf;
using Spire.Xls;
using Spire.Xls.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class Spire_XLS_E2P : IExcelToPDF
    {
        public void TurnToPDF(string sourceExcelFile, string targetPdfFile)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(sourceExcelFile);
            workbook.SaveToFile(targetPdfFile, Spire.Xls.FileFormat.PDF);
        }
    }
}
