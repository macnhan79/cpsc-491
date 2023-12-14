using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace PhoHa7.Library.UserControl.PopupTree
{
    class ItemConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
        {
            if (t == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, t);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            if (destType == typeof(InstanceDescriptor) || destType == typeof(string))
                return true;
            else
                return base.CanConvertTo(context, destType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
        {
            if (value is string)
            {
                try
                {
                    string[] elements = ((string)value).Split(',');
                    return new Item(elements[0], elements[1], elements[2]);
                }
                catch
                {
                    throw new ArgumentException("Could not convert the value");
                }
            }
            return base.ConvertFrom(context, info, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo info, object value, Type destType)
        {
            if (destType == typeof(string))
            {
                Item item = (Item)value;
                return String.Format("{0}, {1}, {2}", item.TableName, item.ParentTableName, item.Caption);
            }
            else if (destType == typeof(InstanceDescriptor))
            {
                Item item = (Item)value;
                ConstructorInfo ctor =
                  typeof(Item).GetConstructor(
                  new Type[] { typeof(string), typeof(string), typeof(string) });

                return new InstanceDescriptor(ctor,
                   new object[] { item.TableName, item.ParentTableName, item.Caption });
            }
            else
            {
                return base.ConvertTo(context, info, value, destType);
            }
        }
    }
}