using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Office.Interop.Excel;

namespace SchacoPDFViewer
{
    public class OfficeE2P : IExcelToPDF
    {
        private static Microsoft.Office.Interop.Excel.Application excelApplication;

        public void TurnToPDF(string sourceExcelFile, string targetPdfFile)
        {

            DateTime dt1 = DateTime.Now;
            bool isThreadNull = false;


            var pExportFormat = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
            var pExportQuality = Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard;
            var pOpenAfterPublish = false;
            var pIncludeDocProps = true;
            var pIgnorePrintAreas = true;


            excelApplication = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = null;

            try
            {
                excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelApplication.DisplayAlerts = false;
                int xlRepairFile = 1;
                excelWorkbook = excelApplication.Workbooks.Open(sourceExcelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, xlRepairFile);
                if (excelWorkbook != null)
                {
                    try
                    {
                        excelWorkbook.ExportAsFixedFormat(
                                            pExportFormat,
                                            targetPdfFile,
                                            pExportQuality,
                                            pIncludeDocProps,
                                            pIgnorePrintAreas,

                                            OpenAfterPublish: pOpenAfterPublish
                                        );
                    }
                    catch (Win32Exception ex32)
                    {
                        MyLogger.LoggerInstance.Error(ex32);
                        Messenger.Default.Send(new MainView_ShowPdfMsgEventArgs() { Msg = (string.Format("ErrorMessage:{0}\r\nErrorCode:{1}\r\nNativeErrorCode:{2}\r\n", ex32.Message, ex32.ErrorCode, ex32.NativeErrorCode)) });
                    }
                    catch (System.Threading.ThreadAbortException)
                    {
                        isThreadNull = true;
                    }
                    catch (Exception exc)
                    {
                        MyLogger.LoggerInstance.Error(exc);
                        Messenger.Default.Send(new MainView_ShowPdfMsgEventArgs() { Msg = string.Format("ErrorMessage:{0}", exc.Message) });

                    }
                }
                else
                {

                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                isThreadNull = true;
            }
            catch (Exception exc)
            {
                MyLogger.LoggerInstance.Error(exc);
                Messenger.Default.Send(new MainView_ShowPdfMsgEventArgs() { Msg = string.Format("ErrorMessage:{0}", exc.Message) });
            }
            finally
            {
                DateTime dt2 = DateTime.Now;
                //Cal2.Wpf.ModuleBase.ACalMessageBox.ShowInfo("提示", (dt2-dt1).ToString());

                if (!isThreadNull)
                {
                    try
                    {
                        if (excelWorkbook != null)
                        {
                            //excelWorkbook.Save();
                            excelWorkbook.Close();
                            excelWorkbook = null;
                        }
                        if (excelApplication != null)
                        {
                            excelApplication.Quit();
                            excelApplication.DisplayAlerts = true;
                            KillExcel(excelApplication);
                            excelApplication = null;
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    catch
                    {
                        if (excelApplication != null)
                            KillExcel(excelApplication);
                        //if (excelApplication != null)
                        //    Cal2.Wpf.ModuleBase.ACalMessageBox.ShowInfo("提示", "Excel 未关闭！");
                    }
                }
            }

        }

        /// <summary>
        /// 杀掉进程
        /// </summary>
        /// <param name="theApp"></param>
        public static void KillExcel(Microsoft.Office.Interop.Excel.Application theApp)
        {


            try
            {

                int id = 0;

                IntPtr intptr = new IntPtr(excelApplication.Hwnd);
                System.Diagnostics.Process p = null;
                GetWindowThreadProcessId(intptr, out id);
                p = System.Diagnostics.Process.GetProcessById(id);
                if (p != null)
                {
                    p.Kill();
                    p.Close();
                    p.Dispose();
                    p = null;
                }

            }
            catch
            {
                excelApplication = null;
            }
            finally
            {
                try
                {
                    excelApplication = null;

                }
                catch
                {
                }
            }
        }

        //调用底层函数获取进程标示 
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);
    }
}
