using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public class MyTreeNode: ObservableObject
    {
        string _FullName = "";
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged(() => Name);
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
