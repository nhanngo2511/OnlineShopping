using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RestfulService.Controllers
{
    public class BrandController : ApiController
    {

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public List<Brand> GetBrandsList()
        {
            List<Brand> brands = BrandService.GetBrands();

            return brands;
        }

        // GET

        public Brand GetBrandDetails(int ID)
        {
            Brand brand = BrandService.GetBrandByID(ID);

            return brand;
        }

        // POST

        public void CreateBrand(string name)
        {
            BrandService.CreateBrand(name);
        }

        // PUT

        public List<Brand> UpdateBrand(int ID, string Name)
        {
            BrandService.UpdateBrand(ID, Name);

            List<Brand> brands = BrandService.GetBrands();

            return brands;
        }

        // DELETE

        public List<Brand> DeleteBrand(int ID)
        {
            BrandService.DeleteBrand(ID);

            List<Brand> brands = BrandService.GetBrands();

            return brands;
        }
    }
}
