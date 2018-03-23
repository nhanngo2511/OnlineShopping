using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ONLINE_SHOPPING.APIs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BrandController : ApiController
    {

        //  [BasicAuthentication]        

        // [Authorize(Roles="Admin")]
        // [Authorize] 
        public List<Brand> Get()
        {
            return BrandService.GetBrands();
        }

        public Brand Get(int ID)
        {
            return BrandService.GetBrandByID(ID);
        }

        public IHttpActionResult Post([FromBody]string BrandName)
        {
            try
            {
                BrandService.CreateBrand(BrandName);
                return Ok();

            }
            catch (Exception)
            {
                return Content(HttpStatusCode.BadRequest, "Can't create new brand");
            }
        }

        public List<Brand> Delete(int ID)
        {
            BrandService.DeleteBrand(ID);

            return BrandService.GetBrands();

        }

        [BasicAuthentication]
        public Brand Put(int ID, [FromBody]string BrandName)
        {
            BrandService.UpdateBrand(ID, BrandName);

            return BrandService.GetBrandByID(ID);
        }

    }
}
