using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public static class Helper
    {
        public static string[] ExcelExtenseList = new string[] { ".xls", ".xlsx" };
             

        public static bool CheckWhetherExcelFile(string FileName)
        {
            var extense = Path.GetExtension(FileName);
            if (ExcelExtenseList.FirstOrDefault(t => t.ToLower().Equals(extense.ToLower())) != null)
            {
                return true;
            }
            return false;
        }
    }
}
