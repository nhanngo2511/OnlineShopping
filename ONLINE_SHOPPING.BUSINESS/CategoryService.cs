using ONLINE_SHOPPING.DAL;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.BUSINESS
{
    public static class CategoryService
    {

        static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

        public static void GetAllList(out List<Brand> Brands, out List<Category> Categories, out List<AgeType> AgeTypes, out List<CountryType> CoutryTypes, out List<Mode> Modes)
        {

            AccessDB.GetAllSelectList(out Brands, out Categories, out AgeTypes, out CoutryTypes, out Modes);

        }

        public static List<Category> GetLevel2Categories(int ID)
        {
            List<Category> level2Categories = AccessDB.GetLevel2CategoriesByMainCategoryID(ID);
            return level2Categories;
        }

        public static List<Category> GetCategories()
        {

            List<Category> Cates = AccessDB.GetCategories();

            return Cates;
        }
    }
}
