using ONLINE_SHOPPING.DAL;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.BUSINESS
{
    public static class AddressService
    {
        static OnlineShoppingDB AccessDB = new ONLINE_SHOPPING.DAL.OnlineShoppingDB();

        public static List<City> GetCities (){
            List<City> Cities = AccessDB.GetCities();
            return Cities;
        }

        public static List<District> GetDistricts(int CityID)
        {
            List<District> Districts = AccessDB.GetDistricts(CityID);

            return Districts;

        }

        public static List<Ward> GetWards(int DistrictsID)
        {
            List<Ward> Wards = AccessDB.GetWards(DistrictsID);

            return Wards;

        }
    }
}
