using System;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace PhoHa7.Library.UserControl.DanhMuc
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    class ListUITypeEditor : System.Drawing.Design.UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        /// <summary>        
        /// Called by VS whenever the user clicks on the ellipsis in the         
        /// properties window for a property to which this editor is linked.        
        /// </summary>        
        /// <param name="context"></param>        
        /// <param name="provider"></param>        
        /// <param name="value"></param>        
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // Return the value if the value is not of type Int32, Double and Single.
            //if (value.GetType() != typeof(DataTable))
            //    return value;

            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                // Display an angle selection control and retrieve the value.
                FrmListUITypeEditor theForm = new FrmListUITypeEditor((DataRowCollection)value);
                edSvc.DropDownControl(theForm);
                value = theForm.GetValues();
                //System.Windows.Forms.MessageBox.Show("" + ((DataRowCollection)value).Count);
            }
            return value;
        }
    }
}
