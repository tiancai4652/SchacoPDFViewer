using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        public static bool CheckWhetherPDFFile(string FileName)
        {
            var extense = Path.GetExtension(FileName);
            if (extense.ToLower().Equals(".pdf"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 调用win api将指定名称的打印机设置为默认打印机
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name);
    }
}
