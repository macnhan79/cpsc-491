using System;
using System.ComponentModel;
using DevExpress.XtraEditors.Repository;

namespace PhoHa7.Library.UserControl
{
    public partial class UCCustomerSearchLookUp : DevExpress.XtraEditors.XtraUserControl
    {
        public UCCustomerSearchLookUp()
        {
            InitializeComponent();
        }

        public event System.EventHandler searchLookUpEdit_EditValueChanged;

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit_EditValueChanged != null)
                searchLookUpEdit_EditValueChanged(sender, e);
        }

        [DefaultValue((string)null), AttributeProvider(typeof(IListSource))]
        public virtual object DataSource
        {
            get
            {
                
                return searchLookUpEdit1.Properties.DataSource;
            }
            set
            {
                if (value == DBNull.Value)
                {
                    value = null;
                }
                if (((value != searchLookUpEdit1.Properties.DataSource) && (((value == null) || (searchLookUpEdit1.Properties.DataSource == null)) || !searchLookUpEdit1.Properties.DataSource.Equals(value))))
                {
                    searchLookUpEdit1.Properties.DataSource = value;
                }
            }
        }


        [DefaultValue((string)null), AttributeProvider(typeof(IListSource))]
        public virtual object DataSourceCustomerType
        {
            get
            {
                return resLookUpCusType.DataSource;
            }
            set
            {
                if (value == DBNull.Value)
                {
                    value = null;
                }
                if (((value != resLookUpCusType.DataSource) && (((value == null) || (resLookUpCusType.DataSource == null)) || !resLookUpCusType.DataSource.Equals(value))))
                {
                    resLookUpCusType.DataSource = value;
                }
            }
        }

        public virtual object EditValue
        {
            get
            {
                return searchLookUpEdit1.EditValue;
            }
            set
            {
                searchLookUpEdit1.EditValue = value;
            }
        }

        public RepositoryItemSearchLookUpEdit Properties
        {
            get
            {
                return searchLookUpEdit1.Properties;
            }
        }

        

    }
}
