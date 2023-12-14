using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using PhoHa7.Library.Classes.Connection;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            try
            {




                SqlConnection conn = ClsConnection.MySqlConn;
                //Application.Run(new PhoHa7.Kitchen());

                FormManager formManager = FormManager.getInstance();

                if (formManager._RibbonForm == null || formManager._RibbonForm.IsDisposed)
                    //objFrmMain = new FrmMain_CTU();
                    formManager._RibbonForm = new global::PhoHa7.Main();

                formManager._RibbonForm.Show();
                formManager._RibbonForm.Activate();
            }
            catch (System.Exception ex)
            {

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
