using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using System.Drawing;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.UserSkins;

namespace PhoMac.Main.POS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //new DevExpress.DemoReports.ConnectionStringConfigurator(System.Configuration.ConfigurationManager.ConnectionStrings)
            //    .SelectDbEngine()
            //    .ExpandDataDirectory(fileName => DevExpress.DemoData.Helpers.DataFilesHelper.FindFile(fileName, DevExpress.DemoData.Helpers.DataFilesHelper.DataPath));
            //DataHelper.ApplicationArguments = arguments;
            System.Globalization.CultureInfo enUs = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = enUs;
            System.Threading.Thread.CurrentThread.CurrentUICulture = enUs;
            //DevExpress.Utils.LocalizationHelper.SetCurrentCulture(DataHelper.ApplicationArguments);
           // DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 8);
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
            //SkinManager.EnableFormSkins();
           // Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false); 
            //WindowsFormsSettings.SetDPIAware();



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            //WindowsFormsSettings.SetDPIAware();
            Application.Run(new FrmTest());
        }



        


    }
    interface IMainForm
    {
        void ShowHome();
        //void ShowAgent(Agent agent);
    }
}
