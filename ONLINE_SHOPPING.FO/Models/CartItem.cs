﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ONLINE_SHOPPING.ENTITIES;

namespace ONLINE_SHOPPING.FO.Models
{
    public class CartItem
    {
        public Product Product { set; get;}
        public int Quantity { set; get; }

    }
}