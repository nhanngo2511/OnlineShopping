using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using ONLINE_SHOPPING.FO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONLINE_SHOPPING.FO.Commons
{
    public static class Cache
    {
        public static List<City> CacheGetCities() { 
            if (System.Web.HttpContext.Current.Cache["CityList"] == null)
            {
                var CacheCityList = System.Web.HttpContext.Current.Cache["CityList"] as List<City>;

                CacheCityList = AddressService.GetCities();

                System.Web.HttpContext.Current.Cache.Insert("CityList", CacheCityList, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

                return CacheCityList;
            }

            return System.Web.HttpContext.Current.Cache["CityList"] as List<City>;
        }

        public static List<District> CacheGetDistricts(int CityID)
        {
            string DistrictListCacheName = "DistrictList" + CityID;

            if (System.Web.HttpContext.Current.Cache[DistrictListCacheName] == null)
            {
                var CacheDistrictList = System.Web.HttpContext.Current.Cache[DistrictListCacheName] as List<District>;

                CacheDistrictList = AddressService.GetDistricts(CityID);

                System.Web.HttpContext.Current.Cache.Insert(DistrictListCacheName, CacheDistrictList, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

                return CacheDistrictList;
            }

            return System.Web.HttpContext.Current.Cache[DistrictListCacheName] as List<District>;
        }

        public static List<Ward> CacheGetWards(int DistrictID)
        {
            string WardListCacheName = "WardList" + DistrictID;

            if (System.Web.HttpContext.Current.Cache[WardListCacheName] == null)
            {
                var CacheWardList = System.Web.HttpContext.Current.Cache[WardListCacheName] as List<Ward>;

                CacheWardList = AddressService.GetWards(DistrictID);

                System.Web.HttpContext.Current.Cache.Insert(WardListCacheName, CacheWardList, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

                return CacheWardList;
            }

            return System.Web.HttpContext.Current.Cache[WardListCacheName] as List<Ward>;
        }

        public static List<Brand> CacheGetBrands()
        {
            
            if (System.Web.HttpContext.Current.Cache["Brand"] == null)
            {
                var CacheBrandList = System.Web.HttpContext.Current.Cache["Brand"] as List<Brand>;

                CacheBrandList = BrandService.GetBrands();

                System.Web.HttpContext.Current.Cache.Insert("Brand", CacheBrandList, null, DateTime.Now.AddMinutes(3), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

                return CacheBrandList;
            }

            return System.Web.HttpContext.Current.Cache["Brand"] as List<Brand>;
        }

        public static List<Category> CacheGetCategories()
        {

            if (System.Web.HttpContext.Current.Cache["Category"] == null)
            {
                var CacheCategoryList = System.Web.HttpContext.Current.Cache["Category"] as List<Category>;

                CacheCategoryList = CategoryService.GetCategories();

                System.Web.HttpContext.Current.Cache.Insert("Category", CacheCategoryList, null, DateTime.Now.AddMinutes(3), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

                return CacheCategoryList;
            }

            return System.Web.HttpContext.Current.Cache["Category"] as List<Category>;
        }

        public static Footer CacheGetFooters()
        {
            List<Brand> brands = CacheGetBrands();
            List<Category> categories = CacheGetCategories();

            Footer CacheFooter = new Footer();

            CacheFooter.Brands = new List<Brand>();
            CacheFooter.Brands = brands;

            CacheFooter.Categories = new List<Category>();
            CacheFooter.Categories = categories;

            return CacheFooter;

        }
       
    }
}