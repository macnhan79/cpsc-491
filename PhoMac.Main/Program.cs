using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Classes.Common;
using PhoMac.Main.GUI;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System.Threading;
using PhoMac.Main.Win32;
using PhoMac.Main.Test;
using System.Drawing;
using DevExpress.Skins.Info;
using DevExpress.Skins;
using System.Globalization;
using PhoHa7;
using DevExpress.XtraEditors;
using PhoMac.Model.Data;
using PhoMac.Model;
using PhoMac.Main.Controller;
using PhoMac.Main.PhoMac_System;
using PhoMac.Business.Presenter;
namespace PhoMac.Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static Mutex _mutex = new Mutex(true, "{d5146e84-6f95-4027-bfa9-a83d47b282a1}");
        [STAThread]
        static void Main()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            
            bool allowMultiInsatnce = false;
            try
            {
                allowMultiInsatnce = ClsPublic.AllowMultiInstance;
            }
            catch (System.Exception ex)
            {
            	
            }
            SplashScreenManager.CloseForm();
            if (allowMultiInsatnce)
            {

                runApplication();
            }
            else
            {
                if (_mutex.WaitOne(TimeSpan.Zero, true))
                {
                    runApplication();
                    _mutex.ReleaseMutex();
                }
                else
                {
                    if (!allowMultiInsatnce)
                    {
                        ClsMsgBox.Loi("Chương trình hệ thống đang được mở.");

                        // send our Win32 message to make the currently running instance
                        // jump on top of all the other windows
                        NativeConstants.PostMessage(
                            (IntPtr)NativeConstants.HWND_BROADCAST,
                            NativeConstants.WM_SHOWME,
                            IntPtr.Zero,
                            IntPtr.Zero);
                    }
                }
            }

            
        }

        private static void runApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //
            //DevExpress.XtraEditors.
            //DevExpress.XtraEditors.WindowsFormsSettings.SetDPIAware();
            //DevExpress.XtraEditors.WindowsFormsSettings.EnableFormSkins();
            //DevExpress.XtraEditors.WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            //DevExpress.XtraEditors.WindowsFormsSettings.ScrollUIMode = DevExpress.XtraEditors.ScrollUIMode.Touch;

            //((DevExpress.LookAndFeel.Design.UserLookAndFeelDefault)DevExpress.LookAndFeel.Design.UserLookAndFeelDefault.Default).LoadSettings(() =>
            //{
            //    var skinCreator = new SkinBlobXmlCreator("HybridApp", "DevExpress.DevAV.SkinData.", typeof(Program).Assembly, null);
            //    SkinManager.Default.RegisterSkin(skinCreator);
            //});
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("HybridApp");
            //float fontSize = 9f;
            //DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("Segoe UI", fontSize);
            //WindowsFormsSettings.DefaultFont = new Font("Segoe UI", fontSize);
            //WindowsFormsSettings.DefaultMenuFont = new Font("Segoe UI", fontSize);
            Application.CurrentCulture = CultureInfo.GetCultureInfo("en-us");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                SqlConnection conn = ClsConnection.MySqlConn;
                Application.Run(new Frm_Kitchen());
            }
            catch (System.Exception ex)
            {
                try
                {
                    SplashScreenManager.CloseForm();
                }
                catch (Exception)
                {

                }
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception as Exception);
        }


        private static void HandleException(Exception ex)
        {
            if (ClsMsgBox.LoiChung(ex, false) == 1)
            {
                ClsPublic.WriteException(ex);
            }
        }



    }
}
