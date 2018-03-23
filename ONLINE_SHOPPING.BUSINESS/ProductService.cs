using ONLINE_SHOPPING.DAL;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ONLINE_SHOPPING.BUSINESS
{
    public static class ProductService
    {
        static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

        public static Product GetProduct(int ID)
        {

            Product Pro = AccessDB.GetProduct(ID);

            return Pro;
        }

        public static List<Product> GetRelateProducts(int BrandID, int CurrentProductID)
        {
            List<Product> prods = new List<Product>();
            prods = AccessDB.GetRelateProducts(BrandID, CurrentProductID);
            return prods;
        }


        public static void CreateProduct(Product prod, List<string> ImageNames)
        {
            int ID;
            AccessDB.CreateProduct(prod.Name, prod.Price, prod.Quantity, prod.DiscountPrice, prod.WholesalePrice, prod.ShortDescription, prod.LongDescription, prod.WholesaleDescription, prod.BrandDetail, prod.InStock, prod.IsPublic, prod.IsHidden, prod.OrderIndex, prod.Brand.ID, prod.Level2Category.ID, prod.AgeType.ID, prod.CountryType.ID, prod.Mode.ID, ImageNames, out ID);
        }


        public static void UploadImages(int? ProductID, List<string> ImageNames)
        {
            AccessDB.CreateImages(ProductID.Value, ImageNames);

        }


        public static void DeleteImage(int? ImageID, string ImageName)
        {

            AccessDB.DeleteImage(ImageID.Value);

        }

  
        public static void UpdateProduct(Product prod, List<string> ImageNames)
        {
            AccessDB.UpdateProduct(prod.ID, prod.Name, prod.Price, prod.Quantity, prod.DiscountPrice, prod.WholesalePrice, prod.ShortDescription, prod.LongDescription, prod.WholesaleDescription, prod.BrandDetail, prod.InStock, prod.IsPublic, prod.IsHidden, prod.OrderIndex, prod.Brand.ID, prod.Level2Category.ID, prod.AgeType.ID, prod.CountryType.ID, prod.Mode.ID, ImageNames);         
        }

        public static List<Image> GetImages(int ProductID) {

            List<Image> images = AccessDB.GetImages(ProductID);
            return images;
        }


        public static List<Product> GetProducts(int? ID, int? BrandID, int? ModeID, int? CategoryID, bool? InStock, bool? IsPublic, int PageSize, out int TotalRecords, int? Page = 1)
        {

            List<Product> prods = AccessDB.GetProducts(ID, BrandID, ModeID, CategoryID, InStock, IsPublic, PageSize, out TotalRecords, Page.Value);

            return prods;
        }



        public static List<Product> GetWholesalePriceOfProduct(string strIDs)
        {
                   
            strIDs = Regex.Replace(strIDs, @"\s+", "");
            int[] IDs = Array.ConvertAll(strIDs.Split(','), int.Parse);

            IDs = IDs.Distinct().ToArray();

            List<Product> prods = new List<Product>();
            prods = AccessDB.GetWholesalePrice(IDs);

            return prods;
        }



        public static List<Product> QuickSearch(string strIDs)
        {         

            strIDs = Regex.Replace(strIDs, @"\s+", "");
            int[] IDs = Array.ConvertAll(strIDs.Split(','), int.Parse);

            IDs = IDs.Distinct().ToArray();

            List<Product> prods = new List<Product>();
            prods = AccessDB.GetWholesalePrice(IDs);       

            return prods;
        }


        public static List<Product> GetProductByModeID(int ModeID, int LimitRowNumber)
        {
            List<Product> ProductList = AccessDB.GetProductsByModeID(ModeID, LimitRowNumber);

            return ProductList;
        }

        public static List<Product> GetProductByCategoryID(int CategoryID, int LimitRowNumber)
        {
            List<Product> ProductList = AccessDB.GetProductsByCategoryID(CategoryID, LimitRowNumber);

            return ProductList;
        }

        public static void UpdateQuantityProduct(int ProductID, int Quantity) {

            AccessDB.UpdateQuantityProduct(ProductID, Quantity);
        }

    }
}
