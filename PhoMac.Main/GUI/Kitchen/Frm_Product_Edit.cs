using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PhoHa7.Library.Enum;
using DevExpress.XtraSplashScreen;
using PhoHa7.Library.Froms;
using PhoHa7.Library.Classes.Common;
using PhoMac.Main.Entities;
using PhoHa7.Library.Classes.Connection;
using System.IO;
using PhoMac.Model;
using PhoMac.Model.Data;

namespace PhoMac.Main.GUI
{
    public partial class Frm_Product_Edit : XtraForm
    {
        private EnumFormStatus _enumFormStatus;
        private int _productID;
        Product _productInfo;
        private EnumFormCode _fromCode = EnumFormCode.FrmCategory;
        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }
        public Frm_Product_Edit()
        {
            InitializeComponent();
        }

        private void Frm_Product_Category_Edit_Load(object sender, EventArgs e)
        {
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "select * from categories";
            comboBoxCategory.Properties.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);

            sql = "select * from PhoHa7_ProductType";
            lookUpType.Properties.DataSource = sqlHelper.ExecuteDataTable(sql, CommandType.Text, null, null);
            LoadForm();
        }


        public EnumFormStatus EnumStatus
        {
            get { return _enumFormStatus; }
            set
            {
                _enumFormStatus = value;
            }
        }

        private void LoadForm()
        {
            if (_enumFormStatus == EnumFormStatus.Add)
            {
                txtProductName.EditValue = null;
                txtBarCode.EditValue = null;
                txtDescription.EditValue = null;
                txtKitchenName.EditValue = null;
                txtKitchenName.EditValue = null;
                // cmbStatus.ItemIndex = 0;
                chkActived.CheckState = CheckState.Checked;
                //popupTreeDM.PrimaryKey = null;
                txtLargePrice.EditValue = null;
                txtSmallPrice.EditValue = null;
                txtWeb.Text = ClsPublic.WebsiteLocation;
                txtImage.Text = ClsPublic.ImageURL;
                //////////////////////
                txtPage.EditValue = "";
                txtCol.EditValue = "";
                txtRow.EditValue = "";
            }
            else if (_enumFormStatus == EnumFormStatus.Modify)
            {
                //sql load product detail
                Dao dao = new Dao();
                //string sql = "select * from products where ProductID = @ProductID";
                //DataTable dt = sqlHelper.ExecuteDataTable(sql, CommandType.Text, new object[] { "@ProductID" }, new object[] { ProductID });
                _productInfo = dao.GetById<Product>(ProductID);
                _productInfo.ProductID = ProductID;


                _productInfo.LargePrice = Convert.ToDecimal(_productInfo.Price != null ? _productInfo.Price : 0);
                _productInfo.SmallPrice = Convert.ToDecimal(_productInfo.CPrice != null ? _productInfo.CPrice : 0);
                _productInfo.Active = _productInfo.Active;
                if (_productInfo.SmallPrice == 0)
                    _productInfo.HasSmallSize = false;
                else
                    _productInfo.HasSmallSize = true;

                try
                {
                    _productInfo.ExpandCategoryID = Convert.ToInt32(_productInfo.ExpandCategoryID + string.Empty);
                }
                catch (System.Exception ex)
                {
                    _productInfo.ExpandCategoryID = 0;
                }

                _productInfo.ProductImage = _productInfo.ProductImage + string.Empty;


                txtProductName.EditValue = _productInfo.ProductName;
                txtKitchenName.EditValue = _productInfo.KitchenName;
                txtBarCode.EditValue = _productInfo.BarCode;
                lookUpType.EditValue = _productInfo.MType;
                comboBoxCategory.EditValue = _productInfo.CategoryID;
                popupTreeDM.PrimaryKey = _productInfo.ExpandCategoryID;
                txtOrderBy.EditValue = _productInfo.OrderBy;
                chkSize.CheckState = _productInfo.HasSmallSize == true ? CheckState.Checked : CheckState.Unchecked;
                txtLargePrice.EditValue = _productInfo.LargePrice;
                txtSmallPrice.EditValue = _productInfo.SmallPrice;

                txtDescription.EditValue = _productInfo.Description;
                chkActived.CheckState = _productInfo.Active == true ? CheckState.Checked : CheckState.Unchecked;
                txtWeb.Text = ClsPublic.WebsiteLocation;
                txtImage.Text = ClsPublic.ImageURL;
                //load image
                destPath = txtWeb.Text + _productInfo.ProductImage;

                //////////////////////
                txtPage.EditValue = _productInfo.Page;
                txtCol.EditValue = _productInfo.Col;
                txtRow.EditValue = _productInfo.Row;
                if (System.IO.File.Exists(destPath))
                {
                    FileStream fs = new FileStream(destPath, FileMode.Open);
                    Image img = Image.FromStream(fs);
                    fs.Close();
                    pictureEdit1.Image = img;
                }
                else
                {
                    pictureEdit1.Image = null;
                }

            }
            popupTreeDM.ReLoad();
        }



        private void buttonsArray1_btnEventUpdate_click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Dao dao = new Dao();
                SqlHelper sqlhelper = new SqlHelper();
                if (_enumFormStatus == EnumFormStatus.Add)
                {
                    int count = 0;
                    Product newProduct = Add();
                    count = dao.Add<Product>(newProduct);
                    //sql
                    //         string sql = "INSERT INTO [Products]"
                    //+ "([ProductName],[CategoryID],[Price],[Active],[ProductImage],[BarCode],[CPrice],[KitchenName],[Description],[ExpandCategoryID],[OrderBy],[MType]) VALUES "
                    //+ "( @ProductName,@CategoryID,@Price,@Active,@ProductImage,@BarCode,@CPrice,@KitchenName,@Description,@ExpandCategoryID,@OrderBy,@MType )";
                    //         count = sqlhelper.ExecuteNonQuery(sql, CommandType.Text,
                    //             new object[] { "@ProductName", "@CategoryID", "@Price", "@Active", "@ProductImage", "@BarCode", "@CPrice", "@KitchenName", "@Description", "@ExpandCategoryID", "@OrderBy", "@MType" },
                    //             new object[] { newProduct.ProductName, newProduct.CategoryID, newProduct.LargePrice, newProduct.Active, newProduct.ProductImage, newProduct.BarCode, newProduct.SmallPrice, newProduct.KitchenName, newProduct.Description, newProduct.InCategoryID, newProduct.OrderBy, newProduct.MType });
                    if (count == 1)
                    {
                        //_enumFormStatus = EnumFormStatus.Modify;
                        //ProductID = newProduct.ProductID;
                        try
                        {
                            ClsMsgBox.ThongTin("Thêm mới thành công");
                            imageName = "";
                            sourceFile = openFileDialog1.FileName;
                            destPath = txtWeb.Text + txtImage.Text;
                            imageName = openFileDialog1.SafeFileName;
                            string destFile = System.IO.Path.Combine(destPath, openFileDialog1.SafeFileName);
                            System.IO.File.Copy(sourceFile, destFile, true);
                            //this.Close();
                        }
                        catch
                        {

                        }
                    }
                }
                else if (_enumFormStatus == EnumFormStatus.Modify)
                {
                    if (ProductID.ToString() != popupTreeDM.PrimaryKey.ToString())
                    {
                        int count = 0;
                        Product newProduct = update();
                        newProduct.ProductID = ProductID;
                        count = dao.AddOrUpdate<Product>(newProduct);
                        //sql
                        //                        string sql = "update [Products] " +
                        //"set [ProductName]=@ProductName,[CategoryID]=@CategoryID,[Price]=@Price,[Active]=@Active, " +
                        //    "[ProductImage]=@ProductImage,[CPrice]=@CPrice,[BarCode]=@BarCode,[KitchenName]=@KitchenName, " +
                        //    "[Description]=@Description,[ExpandCategoryID]=@ExpandCategoryID , [OrderBy] = @OrderBy, [MType] = @MType " +
                        // "WHERE ProductID =@ProductID";
                        //                        count = sqlhelper.ExecuteNonQuery(sql, CommandType.Text,
                        //                        new object[] { "@ProductID", "@ProductName", "@CategoryID", "@Price", "@Active", "@ProductImage", "@BarCode", "@CPrice", "@KitchenName", "@Description", "@ExpandCategoryID", "@OrderBy", "@MType" },
                        //                        new object[] { ProductID, newProduct.ProductName, newProduct.CategoryID, newProduct.LargePrice, newProduct.Active, newProduct.ProductImage, newProduct.BarCode, newProduct.SmallPrice, newProduct.KitchenName, newProduct.Description, newProduct.InCategoryID, newProduct.OrderBy, newProduct.MType });
                        if (count == 1)
                        {
                            ClsMsgBox.ThongTin("Cập nhật thành công");
                            imageName = "";
                            if (openFileDialog1.FileName != "")
                            {
                                sourceFile = openFileDialog1.FileName;
                                destPath = txtWeb.Text + txtImage.Text;
                                imageName = openFileDialog1.SafeFileName;
                                string destFile = System.IO.Path.Combine(destPath, openFileDialog1.SafeFileName);
                                System.IO.File.Copy(sourceFile, destFile, true);
                                openFileDialog1.FileName = "";
                            }
                            this.Close();
                        }
                    }
                    else
                    {
                        ClsMsgBox.CanhBao("Không thể đặt danh mục cha bởi chính nó.");
                    }

                }
            }
        }

        private bool Validation()
        {
            dxErrorProvider1.ClearErrors();
            if ((string)txtProductName.EditValue == null || txtProductName.EditValue == "")
            {
                dxErrorProvider1.SetError(txtProductName, "Vui lòng nhập");
                return false;
            }
            else if (string.IsNullOrEmpty((string)txtBarCode.EditValue))
            {
                dxErrorProvider1.SetError(txtBarCode, "Vui lòng nhập");
                return false;
            }
            else if (string.IsNullOrEmpty((string)txtKitchenName.EditValue))
            {
                dxErrorProvider1.SetError(txtKitchenName, "Vui lòng nhập");
                return false;
            }
            else if (string.IsNullOrEmpty(txtLargePrice.EditValue + string.Empty))
            {
                dxErrorProvider1.SetError(txtLargePrice, "Vui lòng nhập");
                return false;
            }
            else if (string.IsNullOrEmpty(lookUpType.EditValue + string.Empty))
            {
                dxErrorProvider1.SetError(lookUpType, "Vui lòng nhập");
                return false;
            }
            return true;
        }

        private Product Add()
        {
            Product newCat = new Product();
            newCat.ProductName = txtProductName.EditValue + string.Empty;
            newCat.KitchenName = txtKitchenName.EditValue + string.Empty;
            newCat.BarCode = txtBarCode.EditValue + string.Empty;
            newCat.CategoryID = Convert.ToInt32(comboBoxCategory.EditValue + string.Empty);
            newCat.ExpandCategoryID = Convert.ToInt32(popupTreeDM.PrimaryKey);
            newCat.SmallPrice = Convert.ToDecimal(txtSmallPrice.EditValue);
            newCat.LargePrice = Convert.ToDecimal(txtLargePrice.EditValue);
            //newCat.Description = Convert.ToInt32(popupTreeDM.PrimaryKey);
            newCat.Description = txtDescription.EditValue + string.Empty;
            newCat.HasSmallSize = chkSize.CheckState == CheckState.Checked ? true : false;
            newCat.Active = chkActived.CheckState == CheckState.Checked ? true : false;
            newCat.MType = Convert.ToInt32(lookUpType.EditValue + string.Empty);
            newCat.OrderBy = Convert.ToInt32(txtOrderBy.EditValue);
            imageName += string.Empty;
            if (imageName != "")
            {
                newCat.ProductImage = txtImage.Text + imageName;
            }
            else
            {
                if (_productInfo != null)
                {
                    newCat.ProductImage = _productInfo.ProductImage;
                }
                else
                {
                    newCat.ProductImage = "";
                }
            }

            if ((txtOrderBy.EditValue + string.Empty) == "")
            {
                newCat.OrderBy = 999;
            }
            else
            {
                newCat.OrderBy = Convert.ToInt32(txtOrderBy.EditValue);
            }
            return newCat;
        }

        private Product update()
        {
            Dao dao = new Dao();
            Product newCat = dao.GetById<Product>(ProductID);
            newCat.ProductName = txtProductName.EditValue + string.Empty;
            newCat.KitchenName = txtKitchenName.EditValue + string.Empty;
            newCat.BarCode = txtBarCode.EditValue + string.Empty;
            newCat.CategoryID = Convert.ToInt32(comboBoxCategory.EditValue + string.Empty);
            newCat.ExpandCategoryID = Convert.ToInt32(popupTreeDM.PrimaryKey);
            newCat.SmallPrice = Convert.ToDecimal(txtSmallPrice.EditValue);
            newCat.LargePrice = Convert.ToDecimal(txtLargePrice.EditValue);
            //newCat.Description = Convert.ToInt32(popupTreeDM.PrimaryKey);
            newCat.Description = txtDescription.EditValue + string.Empty;
            newCat.HasSmallSize = chkSize.CheckState == CheckState.Checked ? true : false;
            newCat.Active = chkActived.CheckState == CheckState.Checked ? true : false;
            newCat.MType = Convert.ToInt32(lookUpType.EditValue + string.Empty);
            newCat.OrderBy = Convert.ToInt32(txtOrderBy.EditValue);
            imageName += string.Empty;

            ////////////////////////////
            newCat.Page = Convert.ToInt32(txtPage.EditValue);
            newCat.Col = Convert.ToInt32(txtCol.EditValue);
            newCat.Row = Convert.ToInt32(txtRow.EditValue);
            if (imageName != "")
            {
                newCat.ProductImage = txtImage.Text + imageName;
            }
            else
            {
                if (_productInfo != null)
                {
                    newCat.ProductImage = _productInfo.ProductImage;
                }
                else
                {
                    newCat.ProductImage = "";
                }
            }

            if ((txtOrderBy.EditValue + string.Empty) == "")
            {
                newCat.OrderBy = 999;
            }
            else
            {
                newCat.OrderBy = Convert.ToInt32(txtOrderBy.EditValue);
            }
            return newCat;
        }

        private void buttonsArray1_btnEventClose_click(object sender, EventArgs e)
        {
            this.Close();
        }



        string sourceFile;
        string destPath;
        string imageName;
        private void btnQuanLyHinh_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFile = openFileDialog1.FileName;
                destPath = txtWeb.Text + txtImage.Text;
                imageName = openFileDialog1.SafeFileName;
                //string destFile = System.IO.Path.Combine(destPath, openFileDialog1.SafeFileName);
                //System.IO.File.Copy(sourceFile, destFile, true);

                FileStream fs = new FileStream(sourceFile, FileMode.Open);
                Image img = Image.FromStream(fs);
                fs.Close();
                pictureEdit1.Image = img;


            }
        }




    }
}