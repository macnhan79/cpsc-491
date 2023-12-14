using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.Collections;
using PhoMac.Model.Data;
using PhoMac.Model;

namespace PhoMac.Main.POS.Views.UC
{
    public partial class UCGridCategory : UserControl
    {
        public UCGridCategory()
        {
            InitializeComponent();
           
        }

        delegate void ItemClickDelegateHandler(object sender, TileItemEventArgs e);

        public TileItemClickEventHandler ItemClickEvent
        {
            get;
            set;
        }

        ICollection<FilterItem> _dataSouce;
        public ICollection<FilterItem> DataSource
        {
            get {return _dataSouce;}
            set
            {
                _dataSouce = value;
                //if (_dataSouce==null)
                //{
                //    _dataSouce = (new Dao()).GetAll<Category>();
                //}
                foreach (var item in _dataSouce)
                {
                    TileItem tile = CreateTileForFilter(item);
                    tileGroup2.Items.Add(tile);
                    tile.ItemClick += ItemClickEvent;
                }
            }
        }

        void tile_ItemClick(object sender, TileItemEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected TileItem CreateTileForFilter(FilterItem filter)
        {
            TileItem tile = new TileItem();
            tile.ItemSize = TileItemSize.Wide;
            tile.Tag = filter;
            TileItemElement element1 = new TileItemElement();
            element1.Appearance.Normal.FontSizeDelta = 128;
            element1.Appearance.Normal.ForeColor = Color.FromArgb(171, 171, 171);
            element1.Appearance.Normal.Options.UseFont = true;
            element1.Appearance.Normal.Options.UseForeColor = true;
            element1.Appearance.Selected.FontSizeDelta = 128;
            element1.Appearance.Selected.ForeColor = Color.FromArgb(151, 168, 209);
            element1.Appearance.Selected.Options.UseFont = true;
            element1.Appearance.Selected.Options.UseForeColor = true;
            element1.Appearance.Pressed.FontSizeDelta = 128;
            element1.Appearance.Pressed.ForeColor = Color.FromArgb(151, 168, 209);
            element1.Appearance.Pressed.Options.UseFont = true;
            element1.Appearance.Pressed.Options.UseForeColor = true;
            element1.TextAlignment = TileItemContentAlignment.TopRight;
            element1.TextLocation = new Point(-2, -12);
            element1.Text = filter.Count.ToString();
            tile.Elements.Add(element1);
            TileItemElement element2 = new TileItemElement();
            element2.ImageAlignment = TileItemContentAlignment.TopLeft;
            string[] location = filter.ImageUri.Split('/');
            string fileName = location[location.Length - 1];
            string imageName = fileName.Substring(0, fileName.IndexOf('.'));
            //element2.ImageUri = CommonExtension.GetImageUri(location[location.Length - 2], imageName);
            element2.Text = filter.Name;
            element2.TextAlignment = TileItemContentAlignment.BottomLeft;
            tile.Elements.Add(element2);
            return tile;
        }

        #region Filter Item ViewModel

        public class FilterItem
        {
            protected FilterItem(string name, CriteriaOperator filterCriteria, string imageUri)
            {
                this.Name = name;
                this.FilterCriteria = filterCriteria;
                this.ImageUri = imageUri;
            }

            public virtual string Name { get; set; }
            public virtual CriteriaOperator FilterCriteria { get; set; }
            public virtual string ImageUri { get; set; }
            public virtual int Count { get; set; }
            //public static FilterItem Create(string name, CriteriaOperator filterCriteria, string imageUri)
            //{
            //    return ViewModelSource.Create(() => new FilterItem(name, filterCriteria, imageUri));
            //}

            public FilterItem Clone()
            {
                //FilterItem item = FilterItem.Create(Name, FilterCriteria, ImageUri);
                //item.Count = this.Count;
                //return item;
                return null;
            }
        }

        #endregion Filter Item ViewModel

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void tileItem1_ItemClick_1(object sender, TileItemEventArgs e)
        {

        }





    }
}
