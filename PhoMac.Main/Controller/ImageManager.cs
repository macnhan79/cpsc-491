using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoMac.Model.Data;
using PhoMac.Model;

namespace PhoMac.Main.Controller
{
    public static class ImageManager
    {
        static Dictionary<int, Image> ListImage = new Dictionary<int, Image>();

        public static Image GetImageByProductID(int proudctID)
        {
            if (ListImage.Count == 0)
            {
                Dao dao = new Dao();
                var listProduct = dao.GetAll<Product>();
                foreach (Product pro in listProduct)
                {
                    PhoHa7_Sys_Option opt = ClsPublic.ListSystemOption["ProductImageLocation"];
                    string location = string.Empty;
                    if (opt != null)
                    {
                        location = opt.Opt_Value;
                    }
                    ListImage.Add(pro.ProductID, GetPicture(location, pro.ProductID + string.Empty, pro.ProductImage));
                }
            }
            return ListImage[proudctID];
        }

        public static Image GetImage(string imageName)
        {
            PhoHa7_Sys_Option opt = ClsPublic.ListSystemOption["ProductImageLocation"];
            return GetPicture(opt.Opt_Value,"",imageName);
        }

        static Image GetPicture(string pUrlDesImage, string pProductID, string pImageName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (string.IsNullOrEmpty(pImageName))
                    return null;
                else
                {
                    try
                    {
                        string urlStoreImage = string.Empty;
                        if (pProductID != "")
                            urlStoreImage = Path.Combine(pUrlDesImage, pProductID, pImageName);
                        else
                            urlStoreImage = Path.Combine(pUrlDesImage, pImageName);
                        Image img = Image.FromFile(urlStoreImage, false);
                        img.GetThumbnailImage(200, img.Height / (img.Width / 200), null, IntPtr.Zero).Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        return img;
                        //return ms.ToArray();
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }





    }
}
