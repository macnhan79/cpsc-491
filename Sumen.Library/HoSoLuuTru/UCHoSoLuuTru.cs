using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using PhoHa7.Library.Classes.Common;
using PhoHa7.Library.Classes.Connection;

namespace PhoHa7.Library.HoSoLuuTru
{
    public partial class UCHoSoLuuTru : DevExpress.XtraEditors.XtraUserControl
    {
        string hSLT_ID = "-1";
        int hSLT_LHS_ID = -1;


        //lưu đường dẫn hình khi khi chọn hình trên listbox 
        string urlCurrentPicture = "";
        //mãng lưu tên hình load từ database
        List<string> urlImageFromDB;
        //mảng lưu đường dẫn image đc thêm khi Thêm mới-->Chỉ lưu đường dẫn
        List<string> urlTempAddImage;
        //đường dẫn lưu hình
        string urlDesImage = ClsConnection.LocationSave;
        bool isAdd = false;
        bool isEdit = false;
        HSLTDataAccess hsltDataAccess;

        DataTable dtbHSLT;


        public UCHoSoLuuTru()
        {
            InitializeComponent();
            urlImageFromDB = new List<string>();
            urlTempAddImage = new List<string>();
            hsltDataAccess = new HSLTDataAccess();
            urlDesImage = Path.Combine(urlDesImage, "SanPham");
        }

        private void zoomTrackBarControl1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Properties.ZoomPercent = zoomTrackBarControl1.Value;
        }

        private void btAddPic_Click(object sender, EventArgs e)
        {
            if (isAdd || isEdit)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //lấy tên file hình hiện tại
                    urlCurrentPicture = openFileDialog1.FileName;
                    if (Path.GetExtension(urlCurrentPicture) == ".doc" || Path.GetExtension(urlCurrentPicture) == ".docx")
                    {
                        //richEditControl1.LoadDocument(urlCurrentPicture, DocumentFormat.Doc);
                        ShowHidePictureBox(false);
                    }
                    else
                    {
                        FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                        Image img = Image.FromStream(fs);
                        pictureBox1.Image = img;
                        fs.Close();
                        ShowHidePictureBox(true);
                    }
                    //
                    if (isAdd)
                    {
                        urlTempAddImage.Add(urlCurrentPicture);
                        listBoxImg.Items.Add(Path.GetFileName(urlCurrentPicture));
                        urlCurrentPicture = "";
                    }
                    if (isEdit)
                    {
                        if (!Directory.Exists(GetDirectoryByID(hSLT_ID)))
                            Directory.CreateDirectory(GetDirectoryByID(hSLT_ID));

                        urlImageFromDB.Add(Path.GetFileName(urlCurrentPicture));
                        urlTempAddImage.Add(urlCurrentPicture);
                        reloadListBox();
                        urlCurrentPicture = "";
                    }
                }
            }
        }

        private void btDelPic_Click(object sender, EventArgs e)
        {
            if (listBoxImg.SelectedIndex >= 0)
            {
                //MessageBox.Show("Dữ liệu đã xóa không thể phục lại. Bạn chắc chắn muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes
                if (ClsMsgBox.XacNhanXoaThongTin())
                {
                    //xóa hình
                    if (isAdd)
                    {
                        urlTempAddImage.RemoveAt(listBoxImg.SelectedIndex);
                        listBoxImg.Items.RemoveAt(listBoxImg.SelectedIndex);
                        if (listBoxImg.Items.Count > 0)
                        {
                            listBoxImg.SelectedIndex = 0;
                            btnOpen.Enabled = true;
                        }
                        else
                        {
                            //pictureBox1.Image = pictureBox1.ErrorImage;
                            btnOpen.Enabled = false;
                        }
                    }
                    if (isEdit)
                    {
                        if (File.Exists(Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString())))
                            File.Delete(Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString()));
                        
                        foreach (string item in urlTempAddImage)
                        {
                            if (item.EndsWith(listBoxImg.SelectedItem.ToString()))
                            {
                                urlTempAddImage.Remove(item);
                                break;
                            }
                        }
                        urlImageFromDB.RemoveAt(listBoxImg.SelectedIndex);
                        reloadListBox();

                    }
                }
            }
            else
            {
                ClsMsgBox.ThongTin("Vui lòng chọn hình");
                //MessageBox.Show("Vui lòng chọn hình");
            }
        }

        private void listBoxImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fullUrl = "";
            string fullUrlTemp = "";

            if (listBoxImg.SelectedIndex >= 0 && Path.GetExtension(listBoxImg.SelectedItem.ToString()) != ".doc" && Path.GetExtension(listBoxImg.SelectedItem.ToString()) != ".docx")
            {
                fullUrl = Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString());
                foreach (string item in urlTempAddImage)
                {
                    if (Path.GetFileName(item) == listBoxImg.SelectedItem.ToString())
                    {
                        fullUrlTemp = item;
                        break;
                    }
                }
                if (File.Exists(fullUrl))
                {
                    FileStream fs = new FileStream(fullUrl, FileMode.Open);
                    Image img = Image.FromStream(fs);
                    fs.Close();
                    ShowHidePictureBox(true);
                    pictureBox1.Image = img;
                    btnOpen.Enabled = true;
                }
                else if (File.Exists(fullUrlTemp))
                {
                    FileStream fs = new FileStream(fullUrlTemp, FileMode.Open);
                    Image img = Image.FromStream(fs);
                    ShowHidePictureBox(true);
                    pictureBox1.Image = img;
                    fs.Close();
                    btnOpen.Enabled = true;
                }
                else
                {
                    btnOpen.Enabled = false;
                    //pictureBox1.Image = pictureBox1.ErrorImage;
                }
            }
            else if (listBoxImg.SelectedIndex >= 0 && (Path.GetExtension(listBoxImg.SelectedItem.ToString()) == ".doc" || Path.GetExtension(listBoxImg.SelectedItem.ToString()) == ".docx"))
            {
                fullUrl = Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString());
                foreach (string item in urlTempAddImage)
                {
                    if (Path.GetFileName(item) == listBoxImg.SelectedItem.ToString())
                    {
                        fullUrlTemp = item;
                        break;
                    }
                }
                if (File.Exists(fullUrl))
                {
                    //if (Path.GetExtension(fullUrl) == ".doc")
                        //richEditControl1.LoadDocument(fullUrl, DocumentFormat.Doc);
                    //else if (Path.GetExtension(fullUrl) == ".docx")
                        //richEditControl1.LoadDocument(fullUrl, DocumentFormat.OpenXml);
                    ShowHidePictureBox(false);
                    //btnZoom.Enabled = false;
                    btnOpen.Enabled = true;
                }
                else if (File.Exists(fullUrlTemp))
                {
                    //if (Path.GetExtension(fullUrlTemp) == ".doc")
                        //richEditControl1.LoadDocument(fullUrlTemp, DocumentFormat.Doc);
                   // else if (Path.GetExtension(fullUrlTemp) == ".docx")
                        //richEditControl1.LoadDocument(fullUrlTemp, DocumentFormat.OpenXml);

                    ShowHidePictureBox(false);
                    btnOpen.Enabled = true;
                }
                else
                {
                    btnOpen.Enabled = false;
                }
            }
            if (listBoxImg.ItemCount > 0)
            {
                btNext.Enabled = true;
                btPrev.Enabled = true;

            }
            else
            {
                btNext.Enabled = false;
                btPrev.Enabled = false;
            }
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            if (listBoxImg.ItemCount > 0)
            {
                int count = listBoxImg.ItemCount - 1;
                if (listBoxImg.SelectedIndex == 0)
                {
                    listBoxImg.SelectedIndex = count;
                }
                else
                {
                    if (listBoxImg.SelectedIndex == -1)
                        listBoxImg.SelectedIndex = count;
                    else
                        listBoxImg.SelectedIndex--;
                }
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (listBoxImg.ItemCount > 0)
            {
                int count = listBoxImg.ItemCount - 1;
                if (listBoxImg.SelectedIndex == count)
                {
                    listBoxImg.SelectedIndex = 0;
                }
                else
                {
                    listBoxImg.SelectedIndex++;
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string fullUrl = "";
            string fullUrlTemp = "";
            try
            {
                fullUrl = Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString());
                foreach (string item in urlTempAddImage)
                {
                    if (Path.GetFileName(item) == listBoxImg.SelectedItem.ToString())
                    {
                        fullUrlTemp = item;
                        break;
                    }
                }
                fullUrl = Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString());
                if (File.Exists(fullUrl))
                {
                    string tempFile = Path.GetTempPath() + Path.GetFileName(fullUrl);
                    File.Copy(fullUrl, tempFile);
                    System.Diagnostics.Process.Start(tempFile);
                } if (File.Exists(fullUrlTemp))
                {
                    string tempFile = Path.GetTempPath() + Path.GetFileName(fullUrlTemp);
                    File.Copy(fullUrlTemp, tempFile);
                    System.Diagnostics.Process.Start(tempFile);
                }
            }
            catch (System.Exception ex)
            {
                ClsMsgBox.Loi("Không tìm thấy file!");
               // MessageBox.Show("Không tìm thấy file!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            //bật thêm mới Ho So Luu Tru
            if (!isAdd && !isEdit)
            {
                //enableAdd_Edit();
                //isAdd = true;
                //btAdd.Text = "Lưu";
                //btDel.Enabled = false;
                //listBoxImg.DataSource = null;
                //urlImageFromDB.Clear();
                ////pictureBox1.Image = pictureBox1.ErrorImage;
                //txtTrichYeu.Text = "";
                //txtMaHSLT.Text = "";
                //btEdit.Enabled = true;
                //btEdit.Text = "Bỏ qua";
            }
            //bắt đầu thêm mới
            else if (isAdd)
            {
                if (txtMaHSLT.Text == "")
                {
                    txtMaHSLT.Focus();
                }
                else if (txtTrichYeu.Text == "")
                {
                    txtTrichYeu.Focus();
                }
                else
                {
                    AddNewHSLT();
                }
            }
            else if (isEdit)
            {
                if (txtMaHSLT.Text == "")
                {
                    txtMaHSLT.Focus();
                }
                else if (txtTrichYeu.Text == "")
                {
                    txtTrichYeu.Focus();
                }
                else
                {
                    UpdateHSLT();
                }
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            //bật sửa Ho So Luu Tru
            if (isAdd)
            {
                resetForm();
                if (dtbHSLT != null)
                {
                    if (dtbHSLT.Rows.Count > 0)
                    {
                        urlTempAddImage.Clear();
                        LoadDetailHSLT();
                    }
                }
                isAdd = false;
                isEdit = false;
                btEdit.Text = "Sửa";
            }
            else if (isEdit)
            {
                resetForm();
                urlTempAddImage.Clear();
                dtbHSLT.RejectChanges();
                LoadDetailHSLT();
                isAdd = false;
                isEdit = false;
                btEdit.Text = "Sửa";
                btAdd.Enabled = false;
                layoutAdd.Visibility = LayoutVisibility.Never;
                btDel.Enabled = false;
            }
            else if (!isEdit)
            {
                btDel.Enabled = true;
                btAdd.Enabled = true;
                layoutAdd.Visibility = LayoutVisibility.Always;
                enableAdd_Edit();
                btEdit.Text = "Bỏ qua";
                btAdd.Text = "Lưu";
                isEdit = true;
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {

            //if (MessageBox.Show("Dữ liệu đã xóa không thể phục lại. Bạn chắc chắn muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            //{
            //    dtbHSLT.Rows[0].Delete();
            //    if (hsltDataAccess.UpdateHSLT(dtbHSLT))
            //    {
            //        //xóa hình
            //        delAllFileInFolder();
            //        //delete trong database
            //        isEdit = false;
            //        btEdit.Text = "Sửa";
            //        btAdd.Enabled = true;
            //        txtMaHSLT.Properties.ReadOnly = true;
            //        txtTrichYeu.Properties.ReadOnly = true;
            //        resetForm();
            //    }
            //}
            string fullUrl = "";
            string fullUrlTemp = "";
            try
            {
                fullUrl = Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString());
                foreach (string item in urlTempAddImage)
                {
                    if (Path.GetFileName(item) == listBoxImg.SelectedItem.ToString())
                    {
                        fullUrlTemp = item;
                        break;
                    }
                }
                fullUrl = Path.Combine(GetDirectoryByID(hSLT_ID), listBoxImg.SelectedItem.ToString());
                if (File.Exists(fullUrl))
                {
                    image = listBoxImg.SelectedItem.ToString();
                } if (File.Exists(fullUrlTemp))
                {
                    //Bao loi
                }
            }
            catch (System.Exception ex)
            {
                ClsMsgBox.Loi("Không tìm thấy file!");
                //MessageBox.Show("Không tìm thấy file!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btOption_Click(object sender, EventArgs e)
        {
            FrmConfig obj = new FrmConfig();
            obj.ShowDialog();
        }


        private string image = string.Empty;
        void ShowHidePictureBox(bool pShow)
        {
            if (pShow)
            {
                layoutPictureBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //layoutRichEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
               // layoutBarManager.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                layoutPictureBox.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
               // layoutRichEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
               // layoutBarManager.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }

        private void UpdateHSLT()
        {
            disableAdd_Edit();
            foreach (string item in urlTempAddImage)
            {
                string fileNameNew = changeFileNameImage(hSLT_ID, Path.GetExtension(item)) + Path.GetExtension(item);
                string urlNew = Path.Combine(GetDirectoryByID(hSLT_ID), fileNameNew);
                File.Copy(item, urlNew);
                urlImageFromDB.Add(fileNameNew);
                urlImageFromDB.Remove(Path.GetFileName(item));
            }

            dtbHSLT.Rows[0]["Prod_Product_Name"] = txtTrichYeu.Text;
            dtbHSLT.Rows[0]["Prod_Product_ID"] = txtMaHSLT.Text;
            dtbHSLT.Rows[0]["Prod_Images"] = covertListImageToString();
            dtbHSLT.Rows[0]["Prod_Image"] = image;
            if (hsltDataAccess.UpdateHSLT(dtbHSLT))
            {
                isEdit = false;
                btAdd.Text = "Thêm mới";
                btEdit.Text = "Sửa";
                txtMaHSLT.Properties.ReadOnly = true;
                txtTrichYeu.Properties.ReadOnly = true;
            }
            LoadForm();
            urlTempAddImage.Clear();
        }

        private void AddNewHSLT()
        {
            if (dtbHSLT == null)
            {
                dtbHSLT = hsltDataAccess.GetHSLTFillByID(hSLT_ID);
            }
            dtbHSLT.Rows.Clear();
            DataRow newR = dtbHSLT.NewRow();

            newR["Prod_Product_Name"] = txtTrichYeu.Text;
            newR["Prod_Product_ID"] = txtMaHSLT.Text;
            newR["Prod_Images"] = covertListImageToString();
            newR["Prod_Image"] = image;
            dtbHSLT.Rows.Add(newR);
            if (hsltDataAccess.UpdateHSLT(dtbHSLT))
            {
                disableAdd_Edit();
                isAdd = false;
                btAdd.Text = "Thêm mới";
                txtMaHSLT.Properties.ReadOnly = true;
                txtTrichYeu.Properties.ReadOnly = true;
                btEdit.Text = "Sửa";

                //lấy HSLT_ID >> tạo thư mục HSLT_<ID> và đổi tên hình , copy hình
                hSLT_ID = dtbHSLT.Rows[0]["Identity"].ToString();
                dtbHSLT.Clear();
                dtbHSLT = hsltDataAccess.GetHSLTFillByID(hSLT_ID);


                if (!Directory.Exists(GetDirectoryByID(hSLT_ID)))
                    Directory.CreateDirectory(GetDirectoryByID(hSLT_ID));
                //đôir tên file,copy file,
                foreach (string item in urlTempAddImage)
                {
                    string fileNameNew = changeFileNameImage(hSLT_ID, Path.GetExtension(item)) + Path.GetExtension(item);
                    string urlNew = Path.Combine(GetDirectoryByID(hSLT_ID), fileNameNew);
                    File.Copy(item, urlNew);
                    urlImageFromDB.Add(fileNameNew);
                }
                dtbHSLT.Rows[0]["Prod_Images"] = covertListImageToString();
                //dtbHSLT.Rows[0]["HSLT_SOLUONG"] = urlImageFromDB.Count;

                hsltDataAccess.UpdateHSLT(dtbHSLT);
                LoadForm();
                urlTempAddImage.Clear();
            }
        }

        private void LoadDetailHSLT()
        {
            txtTrichYeu.Text = dtbHSLT.Rows[0]["Prod_Product_Name"].ToString();
            txtMaHSLT.Text = dtbHSLT.Rows[0]["Prod_Product_ID"].ToString();
            loadUrlImageFormDB(dtbHSLT.Rows[0]["Prod_Images"].ToString());
            reloadListBox();
            btEdit.Enabled = true;
            btDel.Enabled = false;
        }

        //cắt chuỗi HSLT_DUONGDAN --> lấy từng tên file bỏ vào mảng 
        void loadUrlImageFormDB(string _listUrlImage)
        {
            urlImageFromDB.Clear();
            string[] listUrl = _listUrlImage.Split(',');
            foreach (string item in listUrl)
            {
                if (item != "")
                    urlImageFromDB.Add(item);
            }
        }

        //lấy tât cả tên file hình nối thành 1 chuỗi string
        string covertListImageToString()
        {
            string list = "";
            for (int i = 0; i < urlImageFromDB.Count; i++)
            {
                if (i == urlImageFromDB.Count - 1)
                    list += urlImageFromDB[i];
                else
                    list = list + urlImageFromDB[i] + ",";

            }
            return list;
        }

        //bật chức năng thêm hoắc sửa
        void enableAdd_Edit()
        {
            btAddPic.Enabled = true;
            btDelPic.Enabled = true;
            // txtTrichYeu.Properties.ReadOnly = false;
            // txtMaHSLT.Properties.ReadOnly = false;
        }

        //tắt chức năng thêm hoắc sửa
        void disableAdd_Edit()
        {
            btAddPic.Enabled = false;
            btDelPic.Enabled = false;
            txtTrichYeu.Properties.ReadOnly = true;
            txtMaHSLT.Properties.ReadOnly = true;
        }

        //lấy đường dẫn thư muc HSLT_<HSLT_ID>
        string GetDirectoryByID(string _HSLT_ID)
        {
            return Path.Combine(urlDesImage, _HSLT_ID);
        }

        //thay đổi tên file hình theo HSLT_ID
        string changeFileNameImage(string _HSLT_ID, string _extension)
        {
            string fileNameNew = _HSLT_ID + "_";
            string urltemp;
            int i = 0;
            do
            {
                i++;
                urltemp = Path.Combine(GetDirectoryByID(_HSLT_ID), fileNameNew + i + _extension);
            }
            while (File.Exists(urltemp));

            return fileNameNew + i;
        }

        //xóa tất cả file trong 1 folder HSLT_<hSLT_ID>
        void delAllFileInFolder()
        {
            string[] file = { "" };
            if (Directory.Exists(GetDirectoryByID(hSLT_ID)))
                file = Directory.GetFiles(GetDirectoryByID(hSLT_ID));
            foreach (string item in file)
            {
                try
                {
                    File.Delete(item);
                }
                catch
                {
                    continue;
                }
            }

        }

        //zoom hinh --> picturebox. True là zoom in, False là zoom out
        void ZoomInOut(bool zoom)
        {
            pictureBox1.Dock = DockStyle.None;
            //Zoom ratio by which the images will be zoomed by default
            int zoomRatio = 10;
            //Set the zoomed width and height
            int widthZoom = pictureBox1.Width * zoomRatio / 100;
            int heightZoom = pictureBox1.Height * zoomRatio / 100;
            //zoom = true --> zoom in
            //zoom = false --> zoom out
            if (!zoom)
            {
                widthZoom *= -1;
                heightZoom *= -1;
            }
            //Add the width and height to the picture box dimensions
            pictureBox1.Width += widthZoom;
            pictureBox1.Height += heightZoom;

        }

        //load form theo HSLT ID
        void LoadForm()
        {
            if (hsltDataAccess == null)
                hsltDataAccess = new HSLTDataAccess();
           // urlDesImage = global::ClassLib.Properties.Settings.Default.LocationSave;
           // urlDesImage = Path.Combine(urlDesImage, "SanPham");
            dtbHSLT = hsltDataAccess.GetHSLTFillByID(hSLT_ID);

            if (dtbHSLT.Rows.Count > 0)
            {
                resetForm();
                LoadDetailHSLT();
            }
            else
            {
                ClsMsgBox.Loi("Không tìm thấy sản phẩm!");
               // MessageBox.Show("Không tìm thấy hồ sơ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hSLT_ID = "-1";
                btEdit.Enabled = false;
                btDel.Enabled = false;
                resetForm();
            }
        }

        void reloadListBox()
        {
            listBoxImg.DataSource = null;
            listBoxImg.DataSource = urlImageFromDB;
            if (listBoxImg.Items.Count > 0)
            {
                listBoxImg.SelectedIndex = 0;
                btnOpen.Enabled = true;
            }
            else
            {
                //pictureBox1.Image = pictureBox1.ErrorImage;
                btnOpen.Enabled = false;
            }
        }

        //reset form về trạng thái rỗng, không có HSLD_ID
        void resetForm()
        {
            txtTrichYeu.Text = "";
            txtMaHSLT.Text = "";
            urlImageFromDB.Clear();
            reloadListBox();
            btEdit.Enabled = false;
            btDel.Enabled = false;
            btAddPic.Enabled = false;
            btDelPic.Enabled = false;
            txtTrichYeu.Properties.ReadOnly = true;
            txtMaHSLT.Properties.ReadOnly = true;
            btAdd.Text = "Thêm mới";
            btEdit.Text = "Sửa";
            btAdd.Enabled = false;
            layoutAdd.Visibility = LayoutVisibility.Never;
            //pictureBox1.Image = pictureBox1.ErrorImage;
            isAdd = false;
            isEdit = false;
            pictureBox1.Image = null;
        }

        public string HSLT_ID
        {
            get { return hSLT_ID; }
            set
            {
                hSLT_ID = value;
                if (hSLT_ID != "-1")
                    LoadForm();


            }
        }

        public int HSLT_LHS_ID
        {
            get { return hSLT_LHS_ID; }
            set { hSLT_LHS_ID = value; }
        }


    }
}
