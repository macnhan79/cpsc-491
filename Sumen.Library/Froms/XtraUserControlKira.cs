using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Enum;
using PhoHa7.Library.Interface;

namespace PhoHa7.Library.Froms
{
    public class XtraUserControlKira : XtraUserControl
    {
        private EnumFormCode _formCode;
        public Library.Enum.EnumFormCode FormCode
        {
            get { return _formCode; }
            set { _formCode = value; }
        }
        public bool IsChange
        {
            get;
            set;
        }
        public bool IsSuccess
        {
            get;
            set;
        }

        public bool IsSelected { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        public XtraUserControlKira PreviousInstance { get; set; }

        public string SetText { get { return Text; } set { Text = value; } }

        public XtraUserControlKira()
        {
            //InitializeComponent();
            IsChange = false;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            if (!DesignMode) 
            this.Load += XtraUserControlKira_Load;
            
        }

        public override void Refresh()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            base.Refresh();
            LoadForm();
            SplashScreenManager.CloseForm();
        }



        private void XtraUserControlKira_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadForm();
                init();
            }
            //
        }




        public virtual void ResetForm()
        {

        }

        public virtual void LoadForm()
        {

        }

        public virtual void init()
        {

        }

        public virtual bool Validation()
        {
            return true;
        }

        public virtual bool add()
        {
            return true;
        }

        public virtual bool update()
        {
            return true;
        }

        public virtual bool delete()
        {
            return true;
        }

        public virtual bool print()
        {
            return true;
        }

        public virtual bool exportWord()
        {
            return true;
        }

        public virtual bool exportExcel()
        {
            return true;
        }

        public virtual bool approved()
        {
            return true;
        }

        public virtual bool report()
        {
            return true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XtraUserControlKira
            // 
            this.Name = "XtraUserControlKira";
            this.Load += new System.EventHandler(this.XtraUserControlKira_Load);
            this.ResumeLayout(false);

        }

        public delegate void FrmClickHandler(object sender, FrmClickEventArgs frmClickInfo);

        public event FrmClickHandler FrmClick;

        protected void OnFrmClick(object sender, FrmClickEventArgs frmClickInfo)
        {
            if (FrmClick != null)
            {
                FrmClick(this, frmClickInfo);
            }
        }

        public class FrmClickEventArgs : EventArgs
        {
            public object Parameters;
            public FrmClickEventArgs(object parameters)
            {
                Parameters = parameters;
            }
        }


    }
}
