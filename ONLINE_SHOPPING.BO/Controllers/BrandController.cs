using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.BO.Controllers
{
    public class BrandController : Controller
    {
        MyWebService.MyWebService mywebservice = new MyWebService.MyWebService();

        [HttpGet]
        public ActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBrand(Brand brand)
        {

            // web service
            mywebservice.CreateBrand(brand.Name);
            return RedirectToAction("GetProducts", "Product");


            // restful service
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:2096/");

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = client.PostAsJsonAsync("api/Brand/CreateBrand",brand).Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("GetProducts", "Product");
            //}

            //return View();

        }

    }
}
