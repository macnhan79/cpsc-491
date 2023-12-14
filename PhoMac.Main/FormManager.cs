using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Main
{
    interface IFormManager
    {
        void Load<T>();
    }
    public class FormManager : IFormManager
    {
        private static FormManager instance = new FormManager();

        public static FormManager getInstance()
        {
            return instance;
        }


        ICollection<XtraFormKira> listForms = new Collection<XtraFormKira>();
        public Main _RibbonForm;

        private FormManager()
        {
        }

        XtraFormKira InitFormMdi<T>()
        {
            XtraFormKira form = new XtraFormKira();
            form.LoadUc<T>();
            form.MdiParent = _RibbonForm;
            return form;
        }

        public void InitForm<T>()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            XtraFormKira listForm = listForms.FirstOrDefault(p => p.GetType() == typeof(T));
            if (listForm == null)
            {
                listForm = Activator.CreateInstance<T>() as XtraFormKira;
                listForms.Add(listForm);
            }
            else if (listForm.IsDisposed)
            {
                listForm = Activator.CreateInstance<T>() as XtraFormKira;
            }
            listForm.ShowDialog();
            try
            {
                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            {
                
            }
            
            //XtraFormKira form = new XtraFormKira();
            //form.LoadUc<T>();
            //form.Size = form.XtraUserControl.Size;
            //form.ShowInTaskbar = true;
            //form.MaximizeBox = false;
            //form.MinimizeBox = false;
            //form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

        }

        public void Load<T>()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            XtraFormKira listForm = listForms.FirstOrDefault(p => p.XtraUserControl != null && p.XtraUserControl.GetType() == typeof(T));
            if (listForm == null)
            {
                listForm = InitFormMdi<T>();
                listForms.Add(listForm);
                listForm.Show();
            }
            else if (listForm.IsDisposed)
            {
                listForm = InitFormMdi<T>();
                listForm.Show();
            }
            else
            {
                listForm.Activate();
            }
            SplashScreenManager.CloseForm();
        }



    }
}
