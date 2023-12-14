using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model;
using PhoMac.Business.Data;

namespace PhoMac.Business.Presenter.Entity
{
    public class PSizePresenter
    {
        Product_Size dic;
        Dao dao;
        public List<PSizePresenter> ListProductSize;

        public Product_Size ProductSize
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
                this.Size_ID = dic.Size_ID;
                this.Size_Code = dic.Size_Code;
                this.Size_Name = dic.Size_Name;
                this.Size_Description = dic.Size_Description;
                this.Size_Actived = Convert.ToBoolean(dic.Size_Actived);
            }
        }

        public PSizePresenter()
        {
            dao = new Dao();
            dic = new Product_Size();
            ListProductSize = new List<PSizePresenter>();
        }

        public void CopyToList(List<Product_Size> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                PSizePresenter obj = new PSizePresenter();
                obj.ProductSize = pListDic[i];
                ListProductSize.Add(obj);
            }
        }

        void copyInstance()
        {
            dic.Size_ID = Size_ID;
            dic.Size_Code = Size_Code;
            dic.Size_Name = Size_Name;
            dic.Size_Description = Size_Description;
            dic.Size_Actived = Size_Actived;
        }



        #region Property

        public int Size_ID { get; set; }
        public string Size_Code { get; set; }
        public string Size_Name { get; set; }
        public string Size_Description { get; set; }
        public bool Size_Actived { get; set; }

        /// <summary>
        /// Get List product size details that belong to Product Sizess
        /// </summary>
        public PSizeDetailPresenter ProductSizeDetails
        {
            get
            {
                PSizeDetailPresenter _productSizeDetails = new PSizeDetailPresenter();
                _productSizeDetails.CopyToList(dao.FindByMultiColumnAnd<Product_SizeDetails>(new[] { "PSize_Size_ID" }, Size_ID).ToList());
                return _productSizeDetails;
            }
        }

        #endregion
    }
}
