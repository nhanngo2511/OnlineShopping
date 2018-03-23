using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using ONLINE_SHOPPING.FO.Commons;
using ONLINE_SHOPPING.FO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;
using System.Xml;


namespace ONLINE_SHOPPING.FO.Controllers
{
    public class ViewSiteController : Controller
    {
        MyWebService.MyWebService mywebservice = new MyWebService.MyWebService();

        [HttpGet]
        public ActionResult Home()
        {          
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult NewProductList() {

            List<Product> NewProducts = ProductService.GetProductByModeID(2, 9);

            return PartialView("~/Views/Shared/_NewProductPartial.cshtml", NewProducts);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult TopSellerProductList()
        {

            List<Product> TopSellerProduct = ProductService.GetProductByModeID(4, 6);

            return PartialView("~/Views/Shared/_TopSellerProductList.cshtml", TopSellerProduct);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult PaintProductList()
        {

            List<Product> PaintProducts = ProductService.GetProductByCategoryID(3, 4);

            return PartialView("~/Views/Shared/_PaintProductListPartial.cshtml", PaintProducts);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult ShirtProductList()
        {

            List<Product> ShirtProducts = ProductService.GetProductByCategoryID(4, 4);

            return PartialView("~/Views/Shared/_ShirtProductListPartial.cshtml", ShirtProducts);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult DryFoodList()
        {

            List<Product> DryFoods = ProductService.GetProductByCategoryID(7, 4);

            return PartialView("~/Views/Shared/_DryFoodListPartial.cshtml", DryFoods);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 180)]
        public ActionResult WetFoodList()
        {

            List<Product> WetFoods = ProductService.GetProductByCategoryID(8, 4); ;

            return PartialView("~/Views/Shared/_WetFoodListPartial.cshtml", WetFoods);
        }

        [HttpGet]
       // [OutputCache(Duration = 120, Location = OutputCacheLocation.ServerAndClient, VaryByParam = "ID")]
        public ActionResult ProductDetails(int ID)
        {
            DetailAndRelateProduct DetaRelaProd = new DetailAndRelateProduct();
            DetaRelaProd.ProductDetails = ProductService.GetProduct(ID);
            DetaRelaProd.RelateProducts = ProductService.GetRelateProducts(DetaRelaProd.ProductDetails.Brand.ID.Value, DetaRelaProd.ProductDetails.ID);
            return View(DetaRelaProd);
        }

        [HttpGet]
        [OutputCache(Duration = 120, Location = OutputCacheLocation.ServerAndClient, VaryByParam = "*")]
        public ActionResult ProductList(int? BrandID, int? CategoryID, int? Page = 1)
        {

            int TotalRecords = 0;

            List<Product> prods = ProductService.GetProducts(null, BrandID, 0, CategoryID, true, true, 12, out TotalRecords, Page.Value);

            ViewBag.ToTalRecords = TotalRecords;

            return View(prods);


            //if (System.Web.HttpContext.Current.Cache["ProductList"] == null)
            //{
            //    var CacheProductList = System.Web.HttpContext.Current.Cache["ProductList"] as List<Product>;

            //    CacheProductList = ProductService.GetProduct()

            //    System.Web.HttpContext.Current.Cache.Insert("brands", brandsCache, null, DateTime.Now.AddSeconds(10), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);

            //    return PartialView("~/Views/Shared/_BrandsListView.cshtml", brandsCache);
            //}

            //return PartialView("~/Views/Shared/_BrandsListView.cshtml", System.Web.HttpContext.Current.Cache["brands"] as List<Brand>);
        }

        [ChildActionOnly]
        public ActionResult FooterView()
        {
            //Footer footer = new Footer();
            //footer.Brands = BrandService.GetBrands();
            //footer.Categories = CategoryService.GetCategories();

            Footer footer = Cache.CacheGetFooters();

            return PartialView("~/Views/Shared/_FooterView.cshtml", footer);

        }

        [ChildActionOnly]      
        public ActionResult GetCategories()
        {
            //List<Category> cates = CategoryService.GetCategories();

            List<Category> cates = Cache.CacheGetCategories();
            return PartialView("~/Views/Shared/_CategoriesListView.cshtml", cates);
        }

        // example webservice
        [ChildActionOnly]
        public ActionResult GetBrands()
        {
             List<Brand> brands = BrandService.GetBrands();

            // List<Brand> brands = Cache.CacheGetBrands();

            //List<ONLINE_SHOPPING.ENTITIES.Brand> brs = new List<Brand>();

            // web service
            //DataTable dt = mywebservice.GetBrands().Tables[0];
            //foreach (DataRow dr in dt.Rows)
            //{
            //    Brand br = new Brand();
            //    br.ID = Convert.ToInt32(dr["ID"].ToString());
            //    br.Name = dr["Name"].ToString();

            //    brs.Add(br);
            //}

           

            // restful service
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:2096/");

            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = client.GetAsync("api/Brand/GetBrandsList").Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    // parse response body
            //    brs = response.Content.ReadAsAsync<List<Brand>>().Result;
            //}

            return PartialView("~/Views/Shared/_BrandsListView.cshtml", brands);
   
        }

     

        [OutputCache(Duration = 86400, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Contact()
        {
            return View();
        }




        public JsonResult ChangeMoney(float CurrentMoney, string CurrentMoneyRateName)
        {

            float ResultMonney = 0;

            if (CurrentMoneyRateName == "VND")
            {
                ResultMonney = mywebservice.ChangeMoneyToUSD(CurrentMoney);

                return Json(new { MoneyRateName = "USD", MoneyWithoutFormat = ResultMonney, ResultMoney = Helpers.ViewUltilities.FormatMoneytoUSD(ResultMonney)});
            }

            ResultMonney = mywebservice.ChangeMoneyToVND(CurrentMoney);
            return Json(new { MoneyRateName = "VND", MoneyWithoutFormat = ResultMonney, ResultMoney = Helpers.ViewUltilities.FormatMoneytoVND(ResultMonney) });

        }
    }
}
