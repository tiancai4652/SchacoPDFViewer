using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

       public static ObservableCollection<MyTreeNode> NodesLoad(string FolderPath)
        {
            MyTreeNode node = new MyTreeNode();
            DirectoryInfo info = new DirectoryInfo(FolderPath);
            GetNodes(info, ref node);
            return (node.ChildNodes);
        }

        public static void GetNodes(DirectoryInfo info, ref MyTreeNode node)
        {
            var files = info.GetFiles();
            foreach (var file in files)
            {
                //必须进行与运算，因为默认文件是“Hidden”+归档（二进制11）。而Hidden是10.因此与运算才可以判断
                if ((file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    MyTreeNode dicTree = new MyTreeNode();
                    if (Helper.CheckWhetherExcelFile(file.FullName))
                    {
                        dicTree.Type = TreeType.ExcelFlie;
                    }
                    else
                    {
                        dicTree.Type = TreeType.Unknown;
                    }
                    dicTree.FullExcelFileName = file.FullName;
                    dicTree.ExcelFileName = file.Name;
                    string dir = Path.GetDirectoryName(file.FullName);
                    string filename = Path.GetFileName(dicTree.FullExcelFileName);
                    string pdfFlename = Path.ChangeExtension(filename, ".PDF");
                    dicTree.FullPDFFileName = dir + "\\" + pdfFlename;
                    node.ChildNodes.Add(dicTree);
                }
            }

            var dics = info.GetDirectories();
            foreach (var dic in dics)
            {
                //必须进行与运算，因为默认文件是“Hidden”+归档（二进制11）。而Hidden是10.因此与运算才可以判断
                if ((dic.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    MyTreeNode dicTree = new MyTreeNode();
                    dicTree.Type = TreeType.Folder;
                    dicTree.FullExcelFileName = dic.FullName;
                    dicTree.ExcelFileName = dic.Name;
                    GetNodes(dic, ref dicTree);
                    node.ChildNodes.Add(dicTree);
                }
            }
        }
    }
}
