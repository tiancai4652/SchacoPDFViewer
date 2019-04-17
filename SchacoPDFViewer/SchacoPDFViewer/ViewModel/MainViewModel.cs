using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.IO;

namespace SchacoPDFViewer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Initialize();
            IniMsgMethod();
           
        }

        string _FolderPath;
        public string FolderPath
        {
            get
            {
                return _FolderPath;
            }
            set
            {
                _FolderPath = value;
                RaisePropertyChanged(() => FolderPath);
            }
        }

        ObservableCollection<MyTreeNode> _Nodes = new ObservableCollection<MyTreeNode>();
        public ObservableCollection<MyTreeNode> Nodes
        {
            get
            {
                return _Nodes;
            }
            set
            {
                _Nodes = value;
                RaisePropertyChanged(() => Nodes);
            }
        }

        MyTreeNode _SeletedNode;
        public MyTreeNode SeletedNode
        {
            get
            {
                return _SeletedNode;
            }
            set
            {
                _SeletedNode = value;
                RaisePropertyChanged(() => SeletedNode);
            }
        }

        void Initialize()
        {
            FolderPath = Default.DefaultFolderPath;
            NodesLoad();

        }

        void GetNodes(DirectoryInfo info,ref MyTreeNode node)
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
                    dicTree.FullName = file.FullName;
                    dicTree.Name = file.Name;
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
                    dicTree.FullName = dic.FullName;
                    dicTree.Name = dic.Name;
                    GetNodes(dic,ref dicTree);
                    node.ChildNodes.Add(dicTree);
                }
            }
        }

        void NodesLoad()
        {
            MyTreeNode node = new MyTreeNode();
            DirectoryInfo info = new DirectoryInfo(FolderPath);
            GetNodes(info, ref node);
            Nodes = new ObservableCollection<MyTreeNode>(node.ChildNodes);
        }

        public void IniMsgMethod()
        {
            Messenger.Default.Register<object>(this, MvvmMessage.MainView_SelectedChange, SelectedChange);
        }

        void SelectedChange(object o)
        {
            if (o is MyTreeNode)
            {
                SeletedNode = o as MyTreeNode;
            }
        }
    }
}