using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Cells.Rendering;
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
            AddWaterMark(sourceExcelFile);
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
                PdfSaveOptions options = new PdfSaveOptions();
                options.Compliance = PdfCompliance.None;
                wb.Save(targetPdfFile, options);
            
            }
            catch (Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }
        }

        void AddWaterMark(string sourceExcelFile)
        {
            #region 20190610演示版增加水印效果

            try
            {
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(sourceExcelFile);
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    int firstRowIndex = workbook.Worksheets[i].FirstVisibleRow;
                    int rowMaxIndex = workbook.Worksheets[i].Cells.Rows.Count;

                    //int firstColumsIndex = workbook.Worksheets[i].FirstVisibleColumn;
                    //int columnMaxIndex = workbook.Worksheets[i].Cells.Columns.Count;

                    int row = firstRowIndex+ 1;
                    while (row < rowMaxIndex+ firstRowIndex)
                    {

                        AddWaterMarkInSheet(workbook.Worksheets[i], MsoPresetTextEffect.TextEffect2,
    "演示版", "", 50, false, true
    , row, 0, 1, 0, 100, 500);

                        row += 30;
                    }

                }

                workbook.Save(sourceExcelFile);

            }
            catch (Exception ex)
            {

            }
            #endregion
        }


        void AddWaterMarkInSheet(Worksheet worksheet, MsoPresetTextEffect effect, string text, 
            string fontName, int size, bool fontBold, bool fontItalic, int upperLeftRow, 
            int top, int upperLeftColumn, int left, int height, int width)
        {
            Aspose.Cells.Drawing.Shape wordart = worksheet.Shapes.AddTextEffect(
                effect,text, fontName, size, fontBold, fontItalic, upperLeftRow, top, upperLeftColumn, left, height,width);

            MsoFillFormat wordArtFormat = wordart.FillFormat;
            wordArtFormat.ForeColor = System.Drawing.Color.Red;
            wordArtFormat.Transparency = 0.9;
            MsoLineFormat lineFormat = wordart.LineFormat;
            lineFormat.IsVisible = false;
            wordart.SetLockedProperty(ShapeLockType.Selection, true);
            wordart.SetLockedProperty(ShapeLockType.ShapeType, true);
            wordart.SetLockedProperty(ShapeLockType.Move, true);
            wordart.SetLockedProperty(ShapeLockType.Resize, true);
            wordart.SetLockedProperty(ShapeLockType.Text, true);
        }
    }
}
