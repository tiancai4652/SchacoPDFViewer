/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:SchacoPDFViewer"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;

namespace SchacoPDFViewer.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<SettingToPrintViewModel>();
            //SimpleIoc.Default.Register<IExcelToPDF,AsposeE2P>();
        }

        public MainViewModel Main
        {
            get
            {
                if(SimpleIoc.Default.IsRegistered<MainViewModel>())
                {
                    SimpleIoc.Default.Unregister<MainViewModel>();
                }
                SimpleIoc.Default.Register<MainViewModel>();
                MainViewModel mvm= ServiceLocator.Current.GetInstance<MainViewModel>();
                mvm.FolderPath = Settings.FolderPath;
                mvm.Initialize();
                return mvm;
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public SettingToPrintViewModel SettingToPrint
        {
            get
            {
                var s = ServiceLocator.Current.GetInstance<MainViewModel>();
                var vm = ServiceLocator.Current.GetInstance<SettingToPrintViewModel>();
                vm.PDFFilesToPrint = new List<string>()
            {
                s.SeletedNode.Type == TreeType.ExcelFlie ? s.SeletedNode.FullExcelFileName :
                (s.SeletedNode.Type == TreeType.Folder ? s.SeletedNode.FullPDFFileName : "") };
                return vm;
            }
        }



        public static void Cleanup()
        {
            // TODO Clear the ViewModels
          
        }

        public static void CleanIOC()
        {
            try
            {
                SimpleIoc.Default.Unregister<IExcelToPDF>();
            }
            catch (Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }

            try
            {
                SimpleIoc.Default.Unregister<IPrintPDF>();
            }
            catch (Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }
            try
            {
                SimpleIoc.Default.Unregister<IPrintExcel>();
            }
            catch (Exception ex)
            {
                MyLogger.LoggerInstance.Error(ex);
            }
        }
    }
}