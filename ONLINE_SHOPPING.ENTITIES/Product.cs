using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.ENTITIES
{ // TODO: tang Entities chi de mo phong lai du lieu duoi database
    // Con khong phai nhu vay thi o tang model theo project do
    public class Product
    {
        public int ID { set; get; }

        [Required]
        public string Name { set; get; }
        [Required]
        public float Price { set; get; }

        public int Quantity { set; get; }

        public float DiscountPrice { set; get; }

        public float WholesalePrice { set; get; }

        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string ShortDescription { set; get; }

        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string LongDescription { set; get; }

        [Required]
        public string WholesaleDescription { set; get; }


        public string BrandDetail { set; get; }


        public Boolean InStock { set; get; }


        public Boolean IsPublic { set; get; }


        public Boolean IsHidden { set; get; }

        public int? OrderIndex { set; get; }

        public Brand Brand { set; get; }

        public Category Category { set; get; }

        public Level2Category Level2Category { set; get; }

        public AgeType AgeType { set; get; }

        public CountryType CountryType { set; get; }

        public Mode Mode { set; get; }

        public string DefaultImage { set; get; }

        public List<Image> Images { set; get; }

    }
}