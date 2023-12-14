using System;
using System.ComponentModel;

namespace PhoHa7.Library.UserControl.PopupTree
{
    [Serializable()]
    [TypeConverter(typeof(ItemConverter))]
    public class Item
    {
        private string tableName = string.Empty;
        private string parentTableName = string.Empty;
        private string caption = string.Empty;
        private object primaryKey = string.Empty;
        private object parentKey = string.Empty;
        private object code = string.Empty;
        private object name = string.Empty;
        private int index;


        public Item(string tableName, string parentTableName, string caption)
        {
            this.tableName = tableName;
            this.parentTableName = parentTableName;
            this.caption = caption;
        }

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        public string ParentTableName
        {
            get { return parentTableName; }
            set { parentTableName = value; }
        }
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }
        public object PrimaryKey
        {
            get { return primaryKey; }
            set { primaryKey = value; }
        }
        public object ParentKey
        {
            get { return parentKey; }
            set { parentKey = value; }
        }
        public object Code
        {
            get { return code; }
            set { code = value; }
        }
        public object Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    }
}