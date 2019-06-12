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

            #region 添加水印
            if (true)
            {
                AddWaterMark(sourceExcelFile);
            }
            #endregion
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

        /// <summary>
        /// 添加演示版水印到excel文件
        /// </summary>
        /// <param name="sourceExcelFile"></param>
        public static void AddWaterMark(string sourceExcelFile)
        {

            //string content = Cal2.Wpf.ModuleBase.ACalProductInfo.Company == ModuleBase.CompanyName.Additel ? "Demo" : "演示版";
            string content = "销售演示版";
            try
            {
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(sourceExcelFile);
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                
                    int firstRowIndex = 0;
                    int rowMaxIndex = workbook.Worksheets[i].Cells.MaxDataRow;

                    int row = firstRowIndex + 1;
                    while (row < rowMaxIndex + firstRowIndex - 10)
                    {

                        AddWaterMarkInSheet(workbook.Worksheets[i], MsoPresetTextEffect.TextEffect2,
    content, "", 50, false, true
    , row, 0, 1, 0, 100, 500);
                        row += 30;
                    }
                }

                workbook.Save(sourceExcelFile);

            }
            catch (Exception ex)
            {

            }

        }


        /// <summary>
        /// 在sheet中添加水印
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="effect"></param>
        /// <param name="text"></param>
        /// <param name="fontName"></param>
        /// <param name="size"></param>
        /// <param name="fontBold"></param>
        /// <param name="fontItalic"></param>
        /// <param name="upperLeftRow"></param>
        /// <param name="top"></param>
        /// <param name="upperLeftColumn"></param>
        /// <param name="left"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        static void AddWaterMarkInSheet(Worksheet worksheet, MsoPresetTextEffect effect, string text,
            string fontName, int size, bool fontBold, bool fontItalic, int upperLeftRow,
            int top, int upperLeftColumn, int left, int height, int width)
        {
            Aspose.Cells.Drawing.Shape wordart = worksheet.Shapes.AddTextEffect(
                effect, text, fontName, size, fontBold, fontItalic, upperLeftRow, top, upperLeftColumn, left, height, width);

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
