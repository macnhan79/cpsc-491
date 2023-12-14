using System;
using System.Deployment.Application;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using PhoHa7.Library.Classes.Common;

namespace PhoHa7
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        private FormManager _formManager;
        
        public Main()
        {
            InitializeComponent();
            InitSkinGallery();

            Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly()
               .GetName().Version;

            // Kiểm tra version khi build bằng ClickOnce
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                currentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            bbStaticVersion.Caption = string.Format("Phiên bản: {0}", currentVersion);

            //create event catch msg from socket server
            //ClsPublic.StartSocket()

            LoadView();
        }




        private void Main_Load(object sender, EventArgs e)
        {
            _formManager = FormManager.getInstance();
        }

        #region Sumen system event

        private void bbPermission_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Permission_User>();
        }

        private void bbPermissionGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Permission_Group>();
        }

        private void bbCauHinhServer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.InitForm<FrmCauHinhCSDL>();
        }

        private void bbThamSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _formManager.Load<frmSetting>();
        }

        private void bbError_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_Log_Error>();
        }

        private void bbTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbDoiMatKhau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.InitForm<Frm_Change_Password>();
        }

        private void bbNhatKy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_History_System>();
        }

        private void bbSaoLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.InitForm<Frm_BackUp>();
        }

        private void bbPhucHoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.InitForm<Frm_Restore>();
        }

        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClsBaoLoi.XacNhan("Bạn có chắc thoát chương trình?"))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {

            Application.Exit();
        }

        #endregion

        #region Danh muc event

        private void bbRole_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Role>();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Position_Range>();

        }

        private void bbMau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Product_Color>();
        }

        private void bbSize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Product_Size>();
        }

        private void bbNhaCungCap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Product_Provider>();
        }

        private void bbDanhMuc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Product_Category>();
        }

        private void bbChungLoai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Product_Type>();
        }

        private void bbThanhPho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Province>();
        }

        private void bbQuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_District>();
        }

        private void bbHangDoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Position_Y>();
        }

        private void bbHangNgang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Position_X>();
        }

        private void bbLoaiKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Customer_Type>();
        }

        private void bbGiamGia_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Discount>();
        }

        private void bbkho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Stock>();
        }
        #endregion

        #region Inventory event

        private void bbNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Stock_Inward>();
        }

        private void bbXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Stock_Outward>();
        }

        private void bbTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Inventory>();
        }

        private void bbTongHopTonKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_General_Inventory>();
        }

        private void bbKiemKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_Check_Inventory>();
        }

        private void bbLichSuHangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_History_Product>();
        }

        private void bbTongHopXN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_General_Inward_Outward>();
        }

        private void bbTraCuuHoSo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        #endregion

        #region Entity event click

        private void bbNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Employee>();
        }

        private void bbKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Customer>();
        }

        private void bbSanPham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Product>();
        }

        #endregion

        #region Help event

        private void bbWebsite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Website>();
        }

        #endregion

        #region Report event click

        private void bbTkLoiNhuan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<ReportInwardOutward>();
        }

        #endregion

        #region Bán hàng (sales)

        private void bbBanHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_Sales_Sale>();
        }

        private void bbTraHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_Sales_Return>();
        }

        private void bbDoiHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_Sales_Change_Product>();
        }

        private void bbDatHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Sales_Orders>();
        }

        private void bbDatHangTamHet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //_formManager.Load<Frm_Sales_OutOfStock>();
        }


        #endregion

        #region Admin

        private void bbQuanLyHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  _formManager.Load<Frm_Manager_Orders>();
        }

        private void bbUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // _formManager.Load<Frm_User>();
        }

        #endregion


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.XtraTabbedMdi.XtraMdiTabPage tabmana = xtraTabbedMdiManager1.SelectedPage;

            tabmana.MdiChild.Refresh();
        }

        void LoadView()
        {
            //for (int i = 0; i < ribbon.Pages.Count; i++)
            //{
            //    string formId = ribbon.Pages[i].Tag + string.Empty;
            //    if (!string.IsNullOrEmpty(formId))
            //    {
            //        Permission permission = new Permission(formId, ClsPublic.User.User_Username);
            //        ribbon.Pages[i].Visible = permission.PermissionView();
            //    }
                
            //    //var a = ribbon.Pages[i].Groups;
            //}
            //for (int i = 0; i < ribbon.Items.Count; i++)
            //{
            //    string formId = ribbon.Items[i].Tag + string.Empty;
            //    if (!string.IsNullOrEmpty(formId))
            //    {
            //        Permission permission = new Permission(formId, ClsPublic.User.User_Username);
            //        ribbon.Items[i].Visibility = isView(permission.PermissionView());
            //    }
            //}
        }


        BarItemVisibility isView(bool isView)
        {
            if (isView)
            {
                return BarItemVisibility.Always;
            }
            else
            {
                return BarItemVisibility.Never;
            }
        }


        #region SkinGallery

        void InitSkinGallery()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        void InitSkinGallery1()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
            //DefaultLookAndFeel defaultSkin = new DefaultLookAndFeel();
            //defaultSkin.LookAndFeel.SetSkinStyle("Seven");

            SimpleButton imageButton = new SimpleButton();
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                imageButton.LookAndFeel.SetSkinStyle(cnt.SkinName);
                GalleryItem gItem = new GalleryItem();
                int groupIndex = 0;
                //if (cnt.SkinName.IndexOf("Office") > -1) groupIndex = 1;
                rgbiSkins.Gallery.Groups[groupIndex].Items.Add(gItem);
                gItem.Caption = cnt.SkinName;

                gItem.Image = GetSkinImage(imageButton, 32, 17, 2);
                gItem.HoverImage = GetSkinImage(imageButton, 70, 36, 5);
                gItem.Caption = cnt.SkinName;
                gItem.Hint = cnt.SkinName;
                rgbiSkins.Gallery.Groups[1].Visible = false;
            }
        }
        Bitmap GetSkinImage(SimpleButton button, int width, int height, int indent)
        {
            Bitmap image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(image))
            {
                StyleObjectInfoArgs info = new StyleObjectInfoArgs(new GraphicsCache(g));
                info.Bounds = new Rectangle(0, 0, width, height);
                button.LookAndFeel.Painter.GroupPanel.DrawObject(info);
                button.LookAndFeel.Painter.Border.DrawObject(info);
                info.Bounds = new Rectangle(indent, indent, width - indent * 2, height - indent * 2);
                button.LookAndFeel.Painter.Button.DrawObject(info);
            }
            return image;
        }
        private void rgbiSkins_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            Properties.Settings.Default.SkinStyle = e.Item.Caption;
            Properties.Settings.Default.Save();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Properties.Settings.Default.SkinStyle);
        }

        private void rgbiSkins_Gallery_InitDropDownGallery(object sender, DevExpress.XtraBars.Ribbon.InplaceGalleryEventArgs e)
        {
            e.PopupGallery.CreateFrom(rgbiSkins.Gallery);
            e.PopupGallery.AllowFilter = false;
            e.PopupGallery.ShowItemText = true;
            e.PopupGallery.ShowGroupCaption = true;
            e.PopupGallery.AllowHoverImages = false;
            foreach (GalleryItemGroup galleryGroup in e.PopupGallery.Groups)
                foreach (GalleryItem item in galleryGroup.Items)
                    item.Image = item.HoverImage;
            e.PopupGallery.ColumnCount = 2;
            e.PopupGallery.ImageSize = new Size(70, 36);
        }
        #endregion


























    }
}