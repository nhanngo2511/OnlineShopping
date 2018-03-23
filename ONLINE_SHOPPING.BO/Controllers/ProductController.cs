using ONLINE_SHOPPING.BO.Commons;
using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.BO.Controllers
{


    public class ProductController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateProduct()
        {
            int UserID = (User as MyPrincipal).ID;

            Product prod = new Product();
            List<Brand> Brands = new List<Brand>();
            List<Category> Categories = new List<Category>();
            List<AgeType> AgeTypes = new List<AgeType>();
            List<CountryType> CoutryTypes = new List<CountryType>();
            List<Mode> Modes = new List<Mode>();

            CategoryService.GetAllList(out Brands, out Categories, out AgeTypes, out CoutryTypes, out Modes);
            List<Category> Level2Categories = CategoryService.GetLevel2Categories(Categories[0].ID);

            ViewBag.Brands = new SelectList(Brands, "ID", "Name");
            ViewBag.Categories = new SelectList(Categories, "ID", "Name");
            ViewBag.AgeTypes = new SelectList(AgeTypes, "ID", "Name");
            ViewBag.CountryTypes = new SelectList(CoutryTypes, "ID", "Name");
            ViewBag.Modes = new SelectList(Modes, "ID", "Name");
            ViewBag.level2Categories = new SelectList(Level2Categories, "ID", "Name");
            
            return View(prod);
        }

        [HttpPost]

        public ActionResult CreateProduct(Product prod, List<string> ImageNames)
        {

            if (!ModelState.IsValid)
            {

                List<Brand> Brands = new List<Brand>();
                List<Category> Categories = new List<Category>();
                List<AgeType> AgeTypes = new List<AgeType>();
                List<CountryType> CoutryTypes = new List<CountryType>();
                List<Mode> Modes = new List<Mode>();

                CategoryService.GetAllList(out Brands, out Categories, out AgeTypes, out CoutryTypes, out Modes);
                List<Category> Level2Categories = CategoryService.GetLevel2Categories(Categories[0].ID);

                ViewBag.Brands = new SelectList(Brands, "ID", "Name");
                ViewBag.Categories = new SelectList(Categories, "ID", "Name");
                ViewBag.AgeTypes = new SelectList(AgeTypes, "ID", "Name");
                ViewBag.CountryTypes = new SelectList(CoutryTypes, "ID", "Name");
                ViewBag.Modes = new SelectList(Modes, "ID", "Name");
                ViewBag.level2Categories = new SelectList(Level2Categories, "ID", "Name");

                return View(prod);
            }

            ProductService.CreateProduct(prod, ImageNames);

            return RedirectToAction("GetProducts", "Product");
        }

        [HttpPost]
        public ActionResult UploadImages(int? ProductID)
        {

            List<string> ImageNames = new List<string>();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var photo = Request.Files[i];
                var fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"));
                photo.SaveAs(path + "//" + fileName);

                ImageNames.Add(fileName);

                if (ProductID != null)
                {
                    ProductService.UploadImages(ProductID.Value, ImageNames);
                }
            }

            return Json(ImageNames, JsonRequestBehavior.AllowGet);
        }
      
        public void DeleteImage(int? ImageID, string ImageName)
        {
            string filePath = Request.MapPath("~/Images/" + ImageName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            if (ImageID != null)
            {
                ProductService.DeleteImage(ImageID.Value, ImageName);
            }
        }


        public ActionResult GetLevel2CategoriesByRootID(int MainCategoryID)
        {
            List<Category> lv2cates = CategoryService.GetLevel2Categories(MainCategoryID);
            return Json(lv2cates, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateProduct(int ID)
        {

            Product prod = ProductService.GetProduct(ID);

            List<Brand> Brands = new List<Brand>();
            List<Category> Categories = new List<Category>();
            List<AgeType> AgeTypes = new List<AgeType>();
            List<CountryType> CoutryTypes = new List<CountryType>();
            List<Mode> Modes = new List<Mode>();

            CategoryService.GetAllList(out Brands, out Categories, out AgeTypes, out CoutryTypes, out Modes);
            List<Category> Level2Categories = CategoryService.GetLevel2Categories(Categories[0].ID);

            ViewBag.Brands = new SelectList(Brands, "ID", "Name");
            ViewBag.Categories = new SelectList(Categories, "ID", "Name");
            ViewBag.AgeTypes = new SelectList(AgeTypes, "ID", "Name");
            ViewBag.CountryTypes = new SelectList(CoutryTypes, "ID", "Name");
            ViewBag.Modes = new SelectList(Modes, "ID", "Name");
            ViewBag.level2Categories = new SelectList(Level2Categories, "ID", "Name");

            return View(prod);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product prod, List<string> ImageNames)
        {
            if (!ModelState.IsValid)
            {
                prod.Images = ProductService.GetImages(prod.ID);

                List<Category> Level2Categories = CategoryService.GetLevel2Categories(prod.Category.ID);
                List<Brand> Brands = new List<Brand>();
                List<Category> Categories = new List<Category>();
                List<AgeType> AgeTypes = new List<AgeType>();
                List<CountryType> CoutryTypes = new List<CountryType>();
                List<Mode> Modes = new List<Mode>();

                CategoryService.GetAllList(out Brands, out Categories, out AgeTypes, out CoutryTypes, out Modes);

                ViewBag.Brands = new SelectList(Brands, "ID", "Name");
                ViewBag.Categories = new SelectList(Categories, "ID", "Name");
                ViewBag.AgeTypes = new SelectList(AgeTypes, "ID", "Name");
                ViewBag.CountryTypes = new SelectList(CoutryTypes, "ID", "Name");
                ViewBag.Modes = new SelectList(Modes, "ID", "Name");
                ViewBag.Level2Categories = new SelectList(Level2Categories, "ID", "Name");

                return View(prod);
            }

            ProductService.UpdateProduct(prod, ImageNames);

            return RedirectToAction("GetProducts");
        }

        [Authorize]
        //[OutputCache(Duration = 3600, Location = System.Web.UI.OutputCacheLocation.ServerAndClient, VaryByParam = "*")]
        public ActionResult GetProducts(int? ID, int? BrandID, int? ModeID, int? CategoryID, bool? InStock, bool? IsPublic, int? Page = 1)
        {

            CategoryID = (CategoryID.HasValue) ? CategoryID.Value : 0;

            //using Restfult API

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "sourav:kayal");


            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(new Uri("http://localhost:19878/api/Brand")).Result;

            List<Brand> brands = new List<Brand>();
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body
                var dataObjects = response.Content.ReadAsAsync<List<Brand>>().Result;

                foreach (var d in dataObjects)
                {
                    brands.Add(d as Brand);
                }
            }
            else
            {
                return Content("{0} ({1})", (int)response.StatusCode + response.ReasonPhrase);
            } 

            //List<Brand> brands = new List<Brand>();
            //brands = BrandService.GetBrands();

            ViewBag.Brands = new SelectList(brands, "ID", "Name");

            int PageSize = 10;
            int TotalRecords = 0;

            List<Product> prods = ProductService.GetProducts(ID, BrandID, CategoryID, ModeID, InStock, IsPublic, PageSize, out TotalRecords, Page.Value);

            ViewBag.ToTalRecords = TotalRecords;

            return View(prods);
        }


        [HttpPost]
        public ActionResult GetProducts(int? ID, int? BrandID, bool? InStock, bool? IsPublic)
        {
            return RedirectToAction("GetProducts", new { ID = ID, BrandID = BrandID, CategoryID = (int?)null, InStock = InStock, IsPublic = IsPublic });
        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public ActionResult CheckWholesalePrice()
        {
            return View();
        }


        [HttpPost]
        public string CheckWholesalePrice(string strIDs)
        {
            string strHTML = "";

            List<Product> prods = ProductService.GetWholesalePriceOfProduct(strIDs);

            strHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_CheckWholesalePrice.cshtml", prods);

            return strHTML;
        }

        [HttpPost]
        public string QuickSearch(string strIDs)
        {
            string strHTML = "";

            List<Product> prods = ProductService.QuickSearch(strIDs);

            strHTML = Helpers.ViewUltilities.RenderViewToString02(this, "~/Views/Shared/_QuickSearch.cshtml", prods);

            return strHTML;
        }

    }
}
