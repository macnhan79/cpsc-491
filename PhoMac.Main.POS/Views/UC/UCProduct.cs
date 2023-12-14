using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using PhoHa7.Library.Froms;
using PhoMac.Model.Data;
using PhoMac.Model;
using DevExpress.XtraEditors;
using PhoMac.Main.POS.Data;
using PhoMac.Business.Presenter;
using PhoMac.Business.Presenter.Entity;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCProduct : XtraUserControlKira
    {
        #region Property

        public string BarCode
        {
            get
            {
                return lblBarcode.Text;
            }
            set
            {
                lblBarcode.Text = value;
            }
        }

        

        public string NameProduct
        {
            get
            {
                return lblName.Text;
            }
            set
            {
                lblName.Text = value;
            }
        }

        ProductPresenter _product;
        public int ProductID
        {
            get { return _product.ProductID; }
            set
            {
                _product.ProductID = value;
                _product.Products = dao.GetById<Product>(_product.ProductID);
                init();
            }
        }

        #endregion

        Dao dao = new Dao();
        public UCProduct()
        {
            InitializeComponent();
            _product = new ProductPresenter();
        }

        #region Init
        public override void init()
        {
            base.init();
            dao = new Dao();
            AllowShowImageProduct = Convert.ToBoolean(ClsPublic.ListSystemOption["ShowProductImage"].Opt_Value);

            //load info product
            _product.Products = dao.GetById<Product>(ProductID);
            NameProduct = _product.ProductName;
            BarCode = _product.BarCode;

            this.Name = ProductID + string.Empty;
            
            //load image
            loadImage();
            //load layout
            LoadLayout();
        }
        
        #endregion

        #region Method

        TableLayoutPanel tableLayoutSize;

        void initProductSize(List<PSizeDetailPresenter> sizeDetails)
        {
            tableLayoutSize = new TableLayoutPanel();
            tableLayoutSize.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            tableLayoutPanel1.SetColumnSpan(tableLayoutSize, 2);
            tableLayoutSize.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutSize.Name = "tableLayoutSize";
            tableLayoutSize.RowCount = 1;
            tableLayoutSize.ColumnCount = 1;

            this.tableLayoutPanel1.Controls.Add(tableLayoutSize, 0, 1);

            //List<PSizeDetailPresenter> sizeDetails = _product.ProductSizeDetails.ListProductSizeDetails;
            if (sizeDetails.Count > 0)
            {
                tableLayoutSize.ColumnCount = sizeDetails.Count;

                tableLayoutSize.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                float percentSplit = 100 / sizeDetails.Count;
                for (int i = 0; i < sizeDetails.Count; i++)
                {
                    tableLayoutSize.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, percentSplit));
                    tableLayoutSize.Controls.Add(createButtonSize(sizeDetails[i].ProductSize.Size_Code), i, 0);
                }
            }
        }

        LabelControl createButtonSize(string nameSize)
        {
            LabelControl btn = new DevExpress.XtraEditors.LabelControl();

            btn.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            btn.Appearance.Options.UseFont = true;
            btn.Appearance.Options.UseTextOptions = true;
            btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            btn.Dock = System.Windows.Forms.DockStyle.Fill;
            btn.Name = nameSize;
            btn.TabIndex = 0;
            btn.Text = nameSize;
            return btn;
        }

        void loadImage()
        {
            //do not allow show image
            if (!AllowShowImageProduct)
            {
                lblImage.Visible = false;
            }
                //allow show image
            else
            {
                Image img = ImageManager.GetImageByProductID(_product.ProductID);
                img = ImageTest();

                //image null ==> hide image
                if (img == null)
                {
                    lblImage.Visible = false;
                    AllowShowImageProduct = false;
                }
                else
                {
                    lblImage.Image = img;
                }
            }
            //if (lblImage.Visible == false)
            //{
            //    this.tableLayoutPanel1.SetRow(panelNameAndBarcode, 0);
            //    this.tableLayoutPanel1.SetColumn(panelNameAndBarcode, 0);
            //    this.tableLayoutPanel1.SetColumnSpan(panelNameAndBarcode, 2);

            //    //this.tableLayoutPanel1.SetRow(lblName, 0);
            //    //this.tableLayoutPanel1.SetColumn(lblName, 0);
            //    //this.tableLayoutPanel1.SetColumnSpan(lblName, 2);
                
            //}
            //else
            //{
            //    this.tableLayoutPanel1.SetRow(panelNameAndBarcode, 0);
            //    this.tableLayoutPanel1.SetColumn(panelNameAndBarcode, 1);
            //    this.tableLayoutPanel1.SetColumnSpan(panelNameAndBarcode, 1);
            //}
        }

        #endregion

        #region Layout

        void LoadLayout()
        {
            
            List<PSizeDetailPresenter> sizeDetails = _product.ProductSizeDetails.ListProductSizeDetails;
            //show/hide barcode
            if (!AllowShowBarcode)
            {
                lblBarcode.Visible = false;
            }else{
                lblBarcode.Visible = true;
            }


            if (!AllowShowImageProduct)
            {
                //this.tableLayoutPanel1.SetRow(panelNameAndBarcode, 0);
                this.tableLayoutPanel1.SetColumn(panelNameAndBarcode, 0);
                this.tableLayoutPanel1.SetColumnSpan(panelNameAndBarcode, 2);
            }
            else
            {
                //this.tableLayoutPanel1.SetRow(panelNameAndBarcode, 0);
                this.tableLayoutPanel1.SetColumn(panelNameAndBarcode, 1);
                this.tableLayoutPanel1.SetColumnSpan(panelNameAndBarcode, 1);
            }
            if (sizeDetails.Count == 0)
            {
                this.tableLayoutPanel1.SetRowSpan(panelNameAndBarcode, 2);
                this.tableLayoutPanel1.SetRowSpan(lblImage, 2);
            }
            else
            {
                this.tableLayoutPanel1.SetRowSpan(panelNameAndBarcode, 1);
                this.tableLayoutPanel1.SetRowSpan(lblImage, 1);
                initProductSize(sizeDetails);
            }
        }

        public bool AllowShowBarcode
        {
            get
            {
                return Convert.ToBoolean(ClsPublic.ListSystemOption["ShowProductBarCode"].Opt_Value);
            }
        }

        bool AllowShowImageProduct
        {
            get;
            set;
        }

#endregion

        #region Event
        #endregion


        #region Form Click

        private void lblName_Click(object sender, EventArgs e)
        {
            OnFrmClick(this, new FrmClickEventArgs(ProductID.ToString()));
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
            public string _name;
            public FrmClickEventArgs(string name)
            {
                _name = name;
            }
        }

        #endregion


        Image ImageTest()
        {
            return Image.FromFile("C:\\food.jpg");
        }




    }
}
