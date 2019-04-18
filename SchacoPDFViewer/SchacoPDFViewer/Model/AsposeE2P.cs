﻿using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
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
                if (!File.Exists(sourceExcelFile))
                {
                    return;
                }
                Workbook wb = new Workbook(sourceExcelFile);
                if (File.Exists(targetPdfFile))
                {
                    File.Delete(targetPdfFile);
                }
                wb.Save(targetPdfFile, SaveFormat.Pdf);
            }
            catch (Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }
        }
    }
}
