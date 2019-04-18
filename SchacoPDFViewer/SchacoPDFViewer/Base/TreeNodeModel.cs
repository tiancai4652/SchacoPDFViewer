using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class MyTreeNode: ObservableObject
    {
        string _FullExcelFileName = "";
        public string FullExcelFileName
        {
            get
            {
                return _FullExcelFileName;
            }
            set
            {
                _FullExcelFileName = value;
                RaisePropertyChanged(() => FullExcelFileName);
            }
        }

        string _ExcelFileName;
        public string ExcelFileName
        {
            get
            {
                return _ExcelFileName;
            }
            set
            {
                _ExcelFileName = value;
                RaisePropertyChanged(() => ExcelFileName);
            }
        }

        string _FullPDFFileName;
        public string FullPDFFileName
        {
            get
            {
                return _FullPDFFileName;
            }
            set
            {
                _FullPDFFileName = value;
                RaisePropertyChanged(() => FullPDFFileName);
            }
        }

        TreeType _Type;
        public TreeType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        ObservableCollection<MyTreeNode> _ChildNodes = new ObservableCollection<MyTreeNode>();
        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<MyTreeNode> ChildNodes
        {
            get
            {
                return _ChildNodes;
            }
            set
            {
                _ChildNodes = value;
                RaisePropertyChanged(() => ChildNodes);
            }
        }

    }
}
