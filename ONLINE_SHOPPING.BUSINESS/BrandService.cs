using ONLINE_SHOPPING.DAL;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.BUSINESS
{
    public static class BrandService
    {
        static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

        public static List<Brand> GetBrands()
        {            
            List<Brand> Brands = AccessDB.GetBrands();
            return Brands;
        }

        public static void CreateBrand(string Name) {
            AccessDB.CreateBrand(Name);
        }

        public static Brand GetBrandByID(int ID)
        {
            Brand br = AccessDB.GetBrandByID(ID);
            return br;
        }

        public static void UpdateBrand(int ID, string Name)
        {
            AccessDB.UpdateBrand(ID, Name);
        }

        public static void DeleteBrand(int ID)
        {
            AccessDB.DeleteBrand(ID);
        }
        
    }
}
