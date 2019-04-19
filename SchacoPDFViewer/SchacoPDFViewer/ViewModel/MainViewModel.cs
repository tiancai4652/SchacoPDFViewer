using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Input;

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
        public IExcelToPDF ExcelToPDF { get; set; }
        

        public ICommand ShowCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IExcelToPDF excelToPDF)
        {
            //Initialize();
            IniMsgMethod();
            ShowCommand = new RelayCommand(LoadSelectedPDF);
            ExcelToPDF = excelToPDF;
        }

        bool _IsShowProgressCircle = false;
        public bool IsShowProgressCircle
        {
            get
            {
                return _IsShowProgressCircle;
            }
            set
            {
                _IsShowProgressCircle = value;
                RaisePropertyChanged(() => IsShowProgressCircle);
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

        //public void Initialize()
        //{
        //    NodesLoad();
        //}

        //void GetNodes(DirectoryInfo info,ref MyTreeNode node)
        //{
        //    var files = info.GetFiles();
        //    foreach (var file in files)
        //    {
        //        //必须进行与运算，因为默认文件是“Hidden”+归档（二进制11）。而Hidden是10.因此与运算才可以判断
        //        if ((file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
        //        {
        //            MyTreeNode dicTree = new MyTreeNode();
        //            if (Helper.CheckWhetherExcelFile(file.FullName))
        //            {
        //                dicTree.Type = TreeType.ExcelFlie;
        //            }
        //            else
        //            {
        //                dicTree.Type = TreeType.Unknown;
        //            }
        //            dicTree.FullExcelFileName = file.FullName;
        //            dicTree.ExcelFileName = file.Name;
        //            string dir = Path.GetDirectoryName(file.FullName);
        //            string filename = Path.GetFileName(dicTree.FullExcelFileName);
        //            string pdfFlename = Path.ChangeExtension(filename,".PDF");
        //            dicTree.FullPDFFileName = dir + "\\" + pdfFlename;
        //            node.ChildNodes.Add(dicTree);
        //        }
        //    }

        //    var dics = info.GetDirectories();
        //    foreach (var dic in dics)
        //    {
        //        //必须进行与运算，因为默认文件是“Hidden”+归档（二进制11）。而Hidden是10.因此与运算才可以判断
        //        if ((dic.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
        //        {
        //            MyTreeNode dicTree = new MyTreeNode();
        //            dicTree.Type = TreeType.Folder;
        //            dicTree.FullExcelFileName = dic.FullName;
        //            dicTree.ExcelFileName = dic.Name;
        //            GetNodes(dic,ref dicTree);
        //            node.ChildNodes.Add(dicTree);
        //        }
        //    }
        //}

        //void NodesLoad()
        //{
        //    MyTreeNode node = new MyTreeNode();
        //    DirectoryInfo info = new DirectoryInfo(FolderPath);
        //    GetNodes(info, ref node);
        //    Nodes = new ObservableCollection<MyTreeNode>(node.ChildNodes);
        //}

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

        void LoadSelectedPDF()
        {
            try
            {
                if (SeletedNode.Type == TreeType.ExcelFlie)
                {
                    IsShowProgressCircle = true;
                    Thread x = new Thread(() =>
                    {

                        ExcelToPDF.TurnToPDF(SeletedNode.FullExcelFileName, SeletedNode.FullPDFFileName);
                        Messenger.Default.Send<object>(SeletedNode.FullPDFFileName, MvvmMessage.MainView_ShowSelectedPDF);
                        IsShowProgressCircle = false;
                    });
                    x.Start();
                }
            }
            catch(Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }
          
        }
    }
}