#region Main
//  File name    	:	ClsAddress.cs
//  Purpose		    :	Lấy danh sách tỉnh, huyện, xã trong dataset
//  Creater date	:	11-09-2009
//  Author		    :	Phạm Văn Duy
//  Version		    :	V1.0
//  Copyright		:	Cusc
#endregion

using System;
using System.ComponentModel.Design;
using System.Data;
using System.Windows.Forms.Design;
using MySql.Data.MySqlClient;
using Library.Classes.Connection;

namespace Library.UserControl
{
    /// <summary>
    /// Lớp lấy danh sách tỉnh, huyện, xã trong dataset
    /// </summary>
    public class ClsAddress
    {
        /// <summary>
        /// Tên bảng chứa danh sách tỉnh trong dataset
        /// </summary>
        private readonly string bangTinh = "TINH";
        /// <summary>
        /// Tên bảng chứa danh sách huyện trong dataset
        /// </summary>
        private readonly string bangHuyen = "HUYEN";
        /// <summary>
        /// Tên bảng chứa danh sách xã trong dataset
        /// </summary>
        private readonly string bangXa = "XA";
        /// <summary>
        /// Store lấy danh sách địa chỉ theo dạng cây
        /// </summary>
        private readonly string sqlAddressTree = "DM_DIA_CHI_SELECT_BY_TREE";
        /// <summary>
        /// Store lấy danh sách tỉnh, huyện, xã
        /// </summary>
        private readonly string sqlAddress = "DM_DIA_CHI_SELECT_BY";
        /// <summary>
        /// Store lấy danh sách tỉnh theo tên tỉnh
        /// </summary>
        private readonly string sqlAddressTinh = "DM_DIA_CHI_SELECT_BY_TENTINH";
        /// <summary>
        /// Store lấy danh sách huyện theo tên huyện
        /// </summary>
        private readonly string sqlAddressHuyen = "DM_DIA_CHI_SELECT_BY_TENHUYEN";
        /// <summary>
        /// Store lấy danh sách xã theo tên xã
        /// </summary>
        private readonly string sqlAddressXa = "DM_DIA_CHI_SELECT_BY_TENXA";
        /// <summary>
        /// Store lấy thông tin địa chỉ thei id xã
        /// </summary>
        private readonly string sqlAddressInfo = "DM_DIA_CHI_SELECT_BY_INFO";


        /// <summary>
        /// Command lấy danh sách địa chỉ theo dạng cây
        /// </summary>
        private MySqlCommand cmdAddressTree;
        /// <summary>
        /// Command lấy danh sách tỉnh, huyện, xã
        /// </summary>
        private MySqlCommand cmdAddress;
        /// <summary>
        /// Command lấy danh sách tỉnh theo tên tỉnh
        /// </summary>
        private MySqlCommand cmdAddressTinh;
        /// <summary>
        /// Command lấy danh sách huyện theo tên huyện
        /// </summary>
        private MySqlCommand cmdAddressHuyen;
        /// <summary>
        /// Command lấy danh sách xã theo tên xã
        /// </summary>
        private MySqlCommand cmdAddressXa;
        /// <summary>
        /// Command ấy thông tin địa chỉ thei id xã
        /// </summary>
        private MySqlCommand cmdAddressInfo;

        /// <summary>
        /// Adapter lấy danh sách tỉnh, huyện, xã
        /// </summary>
        private MySqlDataAdapter daAddress;
        /// <summary>
        /// Adapter lấy danh sách tỉnh, huyện, xã theo tên
        /// </summary>
        private MySqlDataAdapter daAddressByName;
        /// <summary>
        /// Chứa danh sách tỉnh, huyện, xã
        /// </summary>
        private DataSet dsAddress;

        /// <summary>
        /// Hàm khởi tạo lớp lấy danh sách tỉnh, huyện, xã trong dataset
        /// </summary>
        public ClsAddress()
        {
            //
            // Command lấy danh sách địa chỉ theo dạng cây
            //
            this.cmdAddressTree = new MySqlCommand();
            this.cmdAddressTree.CommandText = sqlAddressTree;
            this.cmdAddressTree.CommandType = CommandType.StoredProcedure;
            this.cmdAddressTree.Connection = ClsConnection.MySqlConn;
            //
            // cmdAddress - Command lấy danh sách tỉnh, huyện, xã
            //
            this.cmdAddress = new MySqlCommand();
            this.cmdAddress.CommandText = sqlAddress;
            this.cmdAddress.CommandType = CommandType.StoredProcedure;
            this.cmdAddress.Connection = ClsConnection.MySqlConn;
            this.cmdAddress.Parameters.Add("@DC_TINH", MySqlDbType.Decimal, 9);
            this.cmdAddress.Parameters.Add("@DC_QUAN", MySqlDbType.Decimal, 9);
            //
            // cmdAddressTinh - Command lấy danh sách tỉnh theo tên tỉnh
            //
            this.cmdAddressTinh = new MySqlCommand();
            this.cmdAddressTinh.CommandText = sqlAddressTinh;
            this.cmdAddressTinh.CommandType = CommandType.StoredProcedure;
            this.cmdAddressTinh.Connection = ClsConnection.MySqlConn;
            this.cmdAddressTinh.Parameters.Add("@CURENT_DC_TEN", MySqlDbType.Text, 100);
            //
            // cmdAddressHuyen - Command lấy danh sách huyện theo tên huyện
            //
            this.cmdAddressHuyen = new MySqlCommand();
            this.cmdAddressHuyen.CommandText = sqlAddressHuyen;
            this.cmdAddressHuyen.CommandType = CommandType.StoredProcedure;
            this.cmdAddressHuyen.Connection = ClsConnection.MySqlConn;
            this.cmdAddressHuyen.Parameters.Add("@CURENT_DC_TEN", MySqlDbType.Text, 100);
            //
            // cmdAddressXa - Command lấy danh sách xã theo tên xã
            //
            this.cmdAddressXa = new MySqlCommand();
            this.cmdAddressXa.CommandText = sqlAddressXa;
            this.cmdAddressXa.CommandType = CommandType.StoredProcedure;
            this.cmdAddressXa.Connection = ClsConnection.MySqlConn;
            this.cmdAddressXa.Parameters.Add("@CURENT_DC_TEN", MySqlDbType.Text, 100);
            //
            // cmdAddressInfo - Command ấy thông tin địa chỉ thei id xã
            //
            this.cmdAddressInfo = new MySqlCommand();
            this.cmdAddressInfo.CommandText = sqlAddressInfo;
            this.cmdAddressInfo.CommandType = CommandType.StoredProcedure;
            this.cmdAddressInfo.Connection = ClsConnection.MySqlConn;
            this.cmdAddressInfo.Parameters.Add("@CURENT_DC_ID", MySqlDbType.Decimal, 9);
            //
            // daAddress - Adapter lấy danh sách tỉnh, huyện, xã
            //
            this.daAddress = new MySqlDataAdapter();
            this.daAddress.SelectCommand = cmdAddress;
            //
            // Adapter lấy danh sách tỉnh, huyện, xã theo tên
            //
            this.daAddressByName = new MySqlDataAdapter();
            //
            // Chứa danh sách tỉnh, huyện, xã
            //
            this.dsAddress = new DataSet();
        }

        /// <summary>
        /// Tên bảng chứa danh sách tỉnh trong dataset
        /// </summary>
        public string BangTinh
        {
            get { return bangTinh; }
        }
        /// <summary>
        /// Tên bảng chứa danh sách huyện trong dataset
        /// </summary>
        public string BangHuyen
        {
            get { return bangHuyen; }
        }
        /// <summary>
        /// Tên bảng chứa danh sách xã trong dataset
        /// </summary>
        public string BangXa
        {
            get { return bangXa; }
        }
        /// <summary>
        /// Chứa danh sách tỉnh, huyện, xã
        /// </summary>
        public DataSet DsAddress
        {
            get { return dsAddress; }
            set { dsAddress = value; }
        }

        /// <summary>
        /// Lấy danh sách các tỉnh
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public bool FillTinh()
        {
            cmdAddress.Parameters["@DC_TINH"].Value = 1;
            cmdAddress.Parameters["@DC_QUAN"].Value = DBNull.Value;

            if (dsAddress.Tables[bangTinh] != null)
            {
                dsAddress.Tables[bangTinh].Clear();
            }

            try
            {
                daAddress.Fill(dsAddress, bangTinh);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách các huyện
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public bool FillHuyen()
        {
            cmdAddress.Parameters["@DC_TINH"].Value = DBNull.Value;
            cmdAddress.Parameters["@DC_QUAN"].Value = 1;

            if (dsAddress.Tables[bangHuyen] != null)
            {
                dsAddress.Tables[bangHuyen].Clear();
            }

            try
            {
                daAddress.Fill(dsAddress, bangHuyen);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách các xã
        /// </summary>
        /// <returns>True nếu thành công, ngược lại là False</returns>
        public bool FillXa()
        {
            cmdAddress.Parameters["@DC_TINH"].Value = DBNull.Value;
            cmdAddress.Parameters["@DC_QUAN"].Value = DBNull.Value;

            if (dsAddress.Tables[bangXa] != null)
            {
                dsAddress.Tables[bangXa].Clear();
            }

            try
            {
                daAddress.Fill(dsAddress, bangXa);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy thông tin địa chỉ theo dạng cây phân cấp
        /// </summary>
        /// <returns>Chứa danh sách địa chỉ theo dạng cây</returns>
        public DataTable GetAddressTree()
        {
            daAddressByName.SelectCommand = cmdAddressTree;
            DataTable dtAddress = new DataTable();
            try
            {
                daAddressByName.Fill(dtAddress);
                return dtAddress;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin tỉnh theo tên tỉnh
        /// </summary>
        /// <param name="tenTinh">Tên tỉnh</param>
        /// <returns>Thông tin tỉnh</returns>
        public DataRow GetTinhByTenTinh(string tenTinh)
        {
            cmdAddressTinh.Parameters["@CURENT_DC_TEN"].Value = tenTinh;
            daAddressByName.SelectCommand = cmdAddressTinh;
            DataTable dt = new DataTable();
            try
            {
                daAddressByName.Fill(dt);
                return dt.Rows[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin huyện theo tên huyện
        /// </summary>
        /// <param name="tenHuyen">Tên huyện</param>
        /// <returns>Thông tin huyện</returns>
        public DataRow GetHuyenByTenHuyen(string tenHuyen)
        {
            cmdAddressHuyen.Parameters["@CURENT_DC_TEN"].Value = tenHuyen;
            daAddressByName.SelectCommand = cmdAddressHuyen;
            DataTable dt = new DataTable();
            try
            {
                daAddressByName.Fill(dt);
                return dt.Rows[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin xã theo tên xã
        /// </summary>
        /// <param name="tenXa">Tên xã</param>
        /// <returns>Thông tin xã</returns>
        public DataRow GetXaByTenXa(string tenXa)
        {
            cmdAddressXa.Parameters["@CURENT_DC_TEN"].Value = tenXa;
            daAddressByName.SelectCommand = cmdAddressXa;
            DataTable dt = new DataTable();
            try
            {
                daAddressByName.Fill(dt);
                return dt.Rows[0];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy thông tin tỉnh, huyện, xã theo id xã
        /// </summary>
        /// <param name="DC_ID">Id xã</param>
        /// <returns>Thông tin địa chỉ</returns>
        public DataRow GetAddressInfo(int DC_ID)
        {
            cmdAddressInfo.Parameters["@CURENT_DC_ID"].Value = DC_ID;
            daAddressByName.SelectCommand = cmdAddressInfo;
            DataTable dt = new DataTable();
            try
            {
                daAddressByName.Fill(dt);
                return dt.Rows[0];
            }
            catch
            {
                return null;
            }
        }
    }

    public class AddressControlDesigner : ControlDesigner
    {
        private DesignerVerbCollection verbs = new DesignerVerbCollection();

        public override DesignerVerbCollection Verbs
        {
            get { return verbs; }
        }

        public AddressControlDesigner()
        {
            verbs.Add(new DesignerVerb("About",
              new EventHandler(OnClick)));
        }

        protected void OnClick(object sender, EventArgs e)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            DesignerTransaction tran = host.CreateTransaction("About");
            string info = "Author\t: Phạm Văn Duy\n";
            info += "Purpose\t: Cho phép nhập địa chỉ theo dạng tỉnh, huyện, xã trên cùng một ô nhập\n";
            info += "Version\t: 1.0\n";
            info += "Copyright\t: CUSC";
            System.Windows.Forms.MessageBox.Show(info, "Thông tin", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            tran.Commit();
        }
    }
}
