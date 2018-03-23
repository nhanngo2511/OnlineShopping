using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestfulService.Controllers
{
    public class BrandV2Controller : ApiController
    {
        
        //public IEnumerable<Brand> GetBrands()
        //{
        //    List<Brand> brands = BrandService.GetBrands();

        //    return brands;
        //}

        [BasicAuthentication]
        public HttpResponseMessage GetBrandsV2()
        {
            List<Brand> brands = BrandService.GetBrands();

            return Request.CreateResponse(HttpStatusCode.OK, brands);
        }


        public Brand GetBrand(int ID)
        {
            Brand brand = BrandService.GetBrandByID(ID);

            return brand;
        }
    }
}
