using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Main.Entities
{
    public class Product1
    {
        public Product1()
        {
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string KitchenName { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
        public bool HasSmallSize { get; set; }
        public int CategoryID { get; set; }
        public int InCategoryID { get; set; }
        public string ProductImage { get; set; }
        public double LargePrice { get; set; }
        public double SmallPrice { get; set; }
        public string Description { get; set; }
        public int OrderBy { get; set; }
        public int MType { get; set; }
        public string PrintName { get; set; }
        public FontStyle PrintFrontStyle { get; set; }
        public int SaleItemID { get; set; }
    }
}
