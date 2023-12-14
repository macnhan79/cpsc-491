using PhoMac.Business.Data;
using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter.Entity
{
    public class PSizeDetailPresenter
    {
        Product_SizeDetails dic;
        Dao dao;
        public List<PSizeDetailPresenter> ListProductSizeDetails;

        public Product_SizeDetails ProductSizeDetails
        {
            //entity to database
            get
            {
                copyInstance();
                return dic;
            }
            //database to entity
            set
            {
                dic = value;

                this.PSize_ID = dic.PSize_ID;
                this.PSize_Size_ID = Convert.ToInt32(dic.PSize_Size_ID);
                this.PSize_Product_ID = Convert.ToInt32(dic.PSize_Product_ID);
                this.PSize_Price = Convert.ToDecimal(dic.PSize_Price);
                this.PSize_ExtraPrice = Convert.ToDecimal(dic.PSize_ExtraPrice);

            }
        }

        public PSizeDetailPresenter()
        {
            dao = new Dao();
            dic = new Product_SizeDetails();
            ListProductSizeDetails = new List<PSizeDetailPresenter>();
        }

        public void CopyToList(List<Product_SizeDetails> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                PSizeDetailPresenter obj = new PSizeDetailPresenter();
                obj.ProductSizeDetails = pListDic[i];
                ListProductSizeDetails.Add(obj);
            }
        }

        void copyInstance()
        {

            dic.PSize_ID = PSize_ID;
            dic.PSize_Size_ID = PSize_Size_ID;
            dic.PSize_Product_ID = PSize_Product_ID;
            dic.PSize_Price = PSize_Price;
            dic.PSize_ExtraPrice = PSize_ExtraPrice;

            // public virtual Product_Size Product_Size { get; set; }
            //  public virtual Product Product { get; set; }
        }



        #region Property

        public int PSize_ID { get; set; }
        public int PSize_Size_ID { get; set; }
        public int PSize_Product_ID { get; set; }
        public decimal PSize_Price { get; set; }
        public decimal PSize_ExtraPrice { get; set; }

        /// <summary>
        /// Get Instance Product Size that belong to Product Sizes
        /// </summary>
        public PSizePresenter ProductSize
        {
            get
            {
                PSizePresenter _PSizePresenter = new PSizePresenter();
                _PSizePresenter.ProductSize = dao.GetById<Product_Size>(PSize_Size_ID);
                return _PSizePresenter;
            }
        }

        /// <summary>
        /// Get Instance Product that belong to Product Sizes
        /// </summary>
        public ProductPresenter Products
        {
            get
            {
                ProductPresenter _ProductPresenter = new ProductPresenter();
                _ProductPresenter.Products = dao.GetById<Product>(PSize_Product_ID);
                return _ProductPresenter;
            }
        }

        #endregion
    }
}
