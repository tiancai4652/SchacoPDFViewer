using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public static class Default
    {

        static Configuration  config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public static string DefaultFolderPath
        {
            get
            {
                string temp= @"G:\WorkCode\ACal_Tempture\Cal2.Wpf.App\bin\Debug\Templates";
                if (config.AppSettings.Settings["DefaultFolderPath"] != null)
                {
                    temp = config.AppSettings.Settings["DefaultFolderPath"].Value;
                }
                return temp;
            } 
        }

        public static string TempPDFFolder = "ShacoPDFTempFolder";

        public static void Save(string dic)
        {
            if (config.AppSettings.Settings["DefaultFolderPath"] == null)
            {
                config.AppSettings.Settings.Add("DefaultFolderPath", dic);
            }
            else
            {
                config.AppSettings.Settings["DefaultFolderPath"].Value = dic;
            }
            config.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
