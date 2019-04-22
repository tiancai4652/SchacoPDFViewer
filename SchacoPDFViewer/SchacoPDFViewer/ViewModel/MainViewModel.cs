using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Input;
using System.Linq;

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

        bool _IsOpearting = false;
        public bool IsOpearting
        {
            get
            {
                return _IsOpearting;
            }
            set
            {
                _IsOpearting = value;
                RaisePropertyChanged(() => IsOpearting);

                
            }
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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IExcelToPDF excelToPDF)
        {
            //Initialize();
            Register();
            ShowCommand = new RelayCommand(ShowSelectedPDF,()=>!IsOpearting,true);
            DeleteAllPDFCommand = new RelayCommand(DeleteAllPDF, () => !IsOpearting, true);
            CreatAllPDFWithMultiThreadCommand = new RelayCommand(CreatAllPDFWithMultiThread, () => !IsOpearting, true);
            RefreshCommand = new RelayCommand(Refresh,() => !IsOpearting, true);
            ExcelToPDF = excelToPDF;
        }

        public ICommand ShowCommand { get; set; }
        void ShowSelectedPDF()
        {
            try
            {
                IsOpearting = true;
                if (SeletedNode.Type == TreeType.ExcelFlie)
                {
                    IsShowProgressCircle = true;
                    Thread x = new Thread(() =>
                    {
                        ExcelToPDF.TurnToPDF(SeletedNode.FullExcelFileName, SeletedNode.FullPDFFileName);
                        Messenger.Default.Send(new MainView_ShowSelectedPDFEventArgs() { PDFPath = SeletedNode.FullPDFFileName });
                        IsShowProgressCircle = false;
                        Initialize();
                        IsOpearting = false;
                    });
                    x.Start();
                }
                else if (SeletedNode.Type == TreeType.Pdf)
                {
                    IsShowProgressCircle = true;
                    Thread x = new Thread(() =>
                    {
                        Messenger.Default.Send(new MainView_ShowSelectedPDFEventArgs() { PDFPath = SeletedNode.FullPDFFileName });
                        IsShowProgressCircle = false;
                        Initialize();
                        IsOpearting = false;
                    });
                    x.Start();
                }
               
            }
            catch (Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }
        }

        public ICommand DeleteAllPDFCommand { get; set; }
        void DeleteAllPDF()
        {
            IsOpearting = true;
            IsShowProgressCircle = true;
            Thread x = new Thread(() =>
             {
                DirectoryInfo info = new DirectoryInfo(FolderPath);
                var list = info.GetFiles("*.PDF", SearchOption.AllDirectories);
                foreach (var item in list)
                {
                    File.Delete(item.FullName);
                }
                 Initialize();
                 IsShowProgressCircle = false;
                 IsOpearting = false;
            });
            x.Start();
            
        }

        public ICommand RefreshCommand { get; set; }
        void Refresh()
        {
            IsOpearting = true;
            Initialize();
            IsOpearting = false;
        }

        public ICommand CreatAllPDFWithMultiThreadCommand { get; set; }
        void CreatAllPDFWithMultiThread()
        {
            IsOpearting = true;
            IsShowProgressCircle = true;
            Thread x = new Thread(()=> {
                List<MyTreeNode> list = GetAllExcelFiles(Nodes);
                list.ForEach(t => ExcelToPDF.TurnToPDF(t.FullExcelFileName, t.FullPDFFileName));
                Initialize();
                IsShowProgressCircle = false;
                IsOpearting = false;
            });
            x.Start();
        }

        List<MyTreeNode> GetAllExcelFiles(ObservableCollection<MyTreeNode> Nodes)
        {
            List<MyTreeNode> list = new List<MyTreeNode>();
            if (Nodes != null && Nodes.Count > 0)
            {
                foreach (var Node in Nodes)
                {
                    if (Node.Type == TreeType.ExcelFlie)
                    {
                        list.Add(Node);
                    }
                    list.AddRange(GetAllExcelFiles(Node.ChildNodes));
                }
            }
            return list;
        }

        public void Initialize()
        {
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
                    else if (Helper.CheckWhetherPDFFile(file.FullName))
                    {
                        dicTree.Type = TreeType.Pdf;
                    }
                    else
                    {
                        dicTree.Type = TreeType.Unknown;
                    }
                    dicTree.FullExcelFileName = file.FullName;
                    dicTree.ExcelFileName = file.Name;
                    string dir = Path.GetDirectoryName(file.FullName);
                    string filename = Path.GetFileName(dicTree.FullExcelFileName);
                    string pdfFlename = Path.ChangeExtension(filename,".PDF");
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

        void SelectedChange(MainView_SelectedChangeEventArgs o)
        {
            SeletedNode = o.MyTreeNode;
        }


        public void Register()
        {
            Messenger.Default.Register<MainView_SelectedChangeEventArgs>(this, SelectedChange);
            Messenger.Default.Register<MainView_UnregisterVM>(this, (t)=> UnRegister());
        }

        public void UnRegister()
        {
            Messenger.Default.Unregister<MainView_SelectedChangeEventArgs>(this);
            Messenger.Default.Unregister<MainView_UnregisterVM>(this);
        }

      
    }
}