using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using PhoHa7.Library.Classes.Connection;

namespace PhoHa7.Library.HoSoLuuTru
{
    public partial class FrmConfig : Form
    {
        HSLTDataAccess hsltDataAccess;
        string txtLHS_ID="";
        bool isAdd = true;//false-reset form //true-Cho nhap
        bool isEdit = true;//false-reset form //true-Cho nhap
        DataTable dm_lhs;
        public FrmConfig()
        {
            InitializeComponent();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            hsltDataAccess = new HSLTDataAccess();
            
          //  txtHost.Text = ClsConnection.ServerName;
           // txtDatabase.Text = ClsConnection.Database;
         //   txtUsename.Text = ClsConnection.Username;
          //  txtPass.Text = ClsConnection.Password;
            txtLocation.Text = hsltDataAccess.LocationSave;
            //check exist table NV_HOSOLUUTRU, DM_LOAIHOSO
            if (ClsConnection.MySqlConn != null)
            {
                checkExistTable();
                if (hsltDataAccess.CheckExistTableLHS())
                {
                    dm_lhs = hsltDataAccess.GetDmLHS();
                    //dataGridView1.DataSource = dm_lhs;
                }
                
            }
            else
            {
                //tồn tại bảng thì tắt 2 nút tạo bảng
               // btCreateHSLT.Enabled = false;
              //  btCreateLHS.Enabled = false;
            }
        }

        private void checkExistTable()
        {

            ////kiểm tra có tồn tại bảng HSLT DM_LOAIHOSO
            //if (hsltDataAccess.CheckExistTableLHS())
            //{
            //    //lblLHS.Text = "Tồn tại";
            //    //lblLHS.ForeColor = Color.Green;
            //    //btCreateLHS.Enabled = false;
            //    //btCreateHSLT.Enabled = true;
            //    //kiểm tra có tồn tại bảng HSLT
            //    if (hsltDataAccess.CheckExistTableHSLT())
            //    {
            //        lblHSLT.Text = "Tồn tại";
            //        lblHSLT.ForeColor = Color.Green;
            //        btCreateHSLT.Enabled = false;
            //    }
            //    else
            //    {
            //        lblHSLT.Text = "Không tồn tại";
            //        lblHSLT.ForeColor = Color.Red;
            //        btCreateHSLT.Enabled = true;
            //    }
            //}
            //else
            //{
            //    lblLHS.Text = "Không tồn tại";
            //    lblLHS.ForeColor = Color.Red;
            //    btCreateLHS.Enabled = true;
            //    btCreateHSLT.Enabled = false;
            //}
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ////lưu Servername,databse,username,pass,locationSave,Scon vào Settings
            //ClsConnection.ServerName = txtHost.Text;
            //ClsConnection.Database = txtDatabase.Text;
            //ClsConnection.Username = txtUsename.Text;
            //ClsConnection.Password = txtPass.Text;
            hsltDataAccess.LocationSave = txtLocation.Text;
            //string sCon = "Data Source=" + txtHost.Text + ";Initial Catalog=" + txtDatabase.Text + ";User ID=" + txtUsename.Text + ";Password=" + txtPass.Text;
            //ClsConnection.SCon = sCon;
            //MessageBox.Show("Lưu thành công!", "Thông báo");
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btDefault_Click(object sender, EventArgs e)
        {
            
            txtLocation.Text = global::PhoHa7.Library.Properties.Settings.Default.DefaultSave;
        }

        private void btOpenFolder_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Process.Start(txtLocation.Text);
        }

        private void btMove_Click(object sender, EventArgs e)
        {
            try
            {
                //lấy đường dẫn đang lưu hình hiện tại
                string sourceDir = global::PhoHa7.Library.Properties.Settings.Default.LocationSave;
                //di chuyển thư mục qua đường dẫn mới
                Directory.Move(sourceDir, txtLocation.Text);
                //lưu đường dẫn mới
                global::PhoHa7.Library.Properties.Settings.Default.LocationSave = txtLocation.Text;
                global::PhoHa7.Library.Properties.Settings.Default.Save();
                MessageBox.Show("Di chuyển thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btCreateHSLT_Click(object sender, EventArgs e)
        {
            //tạo mới bảng HSLT
            if (hsltDataAccess.CreateTableHSLT())
            {
                MessageBox.Show("Tạo bảng NV_HOSOLUUTRU thành công!", "Thông báo", MessageBoxButtons.OK);
                checkExistTable();
            } 
            else
            {
                MessageBox.Show("Không thể tạo bảng NV_HOSOLUUTRU!", "Có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btCheckCon_Click(object sender, EventArgs e)
        {
            ////kiểm tra kết nôis mssql
            //string sCon = "Data Source=" + txtHost.Text + ";Initial Catalog=" + txtDatabase.Text + ";User ID=" + txtUsename.Text + ";Password=" + txtPass.Text;
            //MySqlConnection m_conn = new MySqlConnection();
            //try
            //{
            //    m_conn.ConnectionString = sCon;
            //    m_conn.Open();
            //    MessageBox.Show("Kết nối server thành công!", "Thông báo", MessageBoxButtons.OK);
            //    ClsConnection.Conn = m_conn;
            //    //lưu connectionString vào Setting
            //    ClsConnection.SCon = sCon;
            //    //check lại có tồn tại 2 bảng HSLT và LHS không
            //    checkExistTable();
            //}
            //catch
            //{
            //    MessageBox.Show("Không thể kết nối đến server!",
            //                 "Thông báo", MessageBoxButtons.OK,
            //                 MessageBoxIcon.Error);
            //}
        }

        private void btCreateLHS_Click(object sender, EventArgs e)
        {
            ////tại mới bảng LHS
            //if (hsltDataAccess.CreateTableLHS())
            //{
            //    MessageBox.Show("Tạo bảng NV_DMLOAIHOSO thành công!", "Thông báo", MessageBoxButtons.OK);
            //    checkExistTable();
            //}
            //else
            //{
            //    MessageBox.Show("Không thể tạo bảng NV_DMLOAIHOSO!", "Có lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        
        private void btAdd_Click(object sender, EventArgs e)
        {
            ////bật thêm mới LHS
            //if (isAdd == true)
            //{
            //    btEdit.Enabled = false;
            //    btReset.Enabled = true;
            //    btAdd.Text = "Thêm mới";
            //    txtMaLHS.Enabled = true;
            //    txtTenLHS.Enabled = true;
            //    txtMaLHS.Text = "";
            //    txtTenLHS.Text = "";
            //    isAdd = false;
            //}
            //else
            //{
            //    //kiểm tra rỗng LHS_MA và LHS_TEN
            //    if (txtMaLHS.Text.Trim() == "")
            //    {
            //        txtMaLHS.Focus();
            //    }
            //    else if (txtTenLHS.Text.Trim() == "")
            //    {
            //        txtTenLHS.Focus();
            //    }
            //    else
            //    {
            //        isAdd = true;
            //        btEdit.Enabled = true;
            //        btReset.Enabled = true;
            //        btAdd.Text = "Thêm";
            //        txtMaLHS.Enabled = false;
            //        txtTenLHS.Enabled = false;
            //        //thêm mới LHS
            //        if (hsltDataAccess.AddNewLHS(txtMaLHS.Text, txtTenLHS.Text))
            //        {
            //            MessageBox.Show("Thêm mới thành công!", "Thông báo");
            //            dm_lhs = hsltDataAccess.GetDmLHS();
            //            dataGridView1.DataSource = dm_lhs;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Có lỗi khi thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            ////bật cập nhật khi LHS_ID khác rỗng
            //if (isEdit == true && txtLHS_ID != "")
            //{
            //    btAdd.Enabled = false;
            //    btEdit.Text = "Cập nhật";
            //    txtMaLHS.Enabled = true;
            //    txtTenLHS.Enabled = true;
            //    isEdit = false;
            //}
            ////nếu LHS_ID = rỗng thì báo chọn LHS
            //else if (isEdit == true && txtLHS_ID == "")
            //{
            //    MessageBox.Show("Vui lòng chọn Loại Hồ Sơ");
            //}
            //else
            //{
            //    //kiểm tra rỗng
            //    if (txtMaLHS.Text.Trim() == "")
            //    {
            //        txtMaLHS.Focus();
            //    }
            //    else if (txtTenLHS.Text.Trim() == "")
            //    {
            //        txtTenLHS.Focus();
            //    }
            //    else
            //    {
            //        isEdit = true;
            //        btAdd.Enabled = true;
            //        btEdit.Text = "Sửa";
            //        txtMaLHS.Enabled = false;
            //        txtTenLHS.Enabled = false;
            //        //update to database
            //        if (hsltDataAccess.UpdateLHSByID(int.Parse(txtLHS_ID), txtMaLHS.Text, txtTenLHS.Text))
            //        {
            //            MessageBox.Show("Cập nhật thành công!");
            //            dm_lhs = hsltDataAccess.GetDmLHS();
            //            dataGridView1.DataSource = dm_lhs;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Có lỗi khi cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            //if (!isAdd)
            //{
            //    txtLHS_ID = "";
            //}
            //isAdd = true;
            //isEdit = true;
            //txtMaLHS.Enabled = false;
            //txtTenLHS.Enabled = false;
            //btEdit.Enabled = true;
            //btAdd.Enabled = true;
            //btEdit.Text = "Sửa";
            //btAdd.Text = "Thêm";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentRow!=null)
            //{
            //    //tạo binding cho textbox LHS_MA và LHS_TEn
            //    int rowIndex = dataGridView1.CurrentRow.Index;
            //    if (txtMaLHS.DataBindings.Count > 0)
            //        txtMaLHS.DataBindings.RemoveAt(0);
            //    txtMaLHS.DataBindings.Add(
            //        new Binding("Text", dataGridView1["LHS_MA", rowIndex], "Value", false));

            //    if (txtTenLHS.DataBindings.Count > 0)
            //        txtTenLHS.DataBindings.RemoveAt(0);
            //    txtTenLHS.DataBindings.Add(
            //        new Binding("Text", dataGridView1["LHS_TEN", rowIndex], "Value", false));
            //    //lấy LHS_ID dùng để cập nhâtj
            //    txtLHS_ID = dataGridView1["LHS_ID", rowIndex].Value.ToString();
            //}
         
        }

        
    }
}
