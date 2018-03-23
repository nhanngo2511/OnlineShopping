using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebService_DEMO.Controllers
{
    public class BrandController : Controller
    {
        //
        // GET: /Brand/

        [HttpGet]
        public ActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBrand(string Name)
        {

            // web service
            //mywebservice.CreateBrand(brand.Name);
            //return RedirectToAction("GetProducts", "Product");


            // restful service
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2096/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Brand/CreateBrand",Name).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetProducts", "Product");
            }

            return View();

        }


    }
}
