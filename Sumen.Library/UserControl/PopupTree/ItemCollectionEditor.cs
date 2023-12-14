using System;
using System.ComponentModel.Design;
using DevExpress.Utils.UI;

namespace PhoHa7.Library.UserControl.PopupTree
{
    public class ItemCollectionEditor : DevExpress.Utils.UI.CollectionEditor//System.ComponentModel.Design.CollectionEditor
    {
        public ItemCollectionEditor(Type type) : base(type) { }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object returnObject = base.EditValue(context, provider, value);
            return returnObject;
        }

        protected override object CreateInstance(Type itemType)
        {
            Item item = new Item(string.Empty, string.Empty, string.Empty);
            return item;
        }
    }
}