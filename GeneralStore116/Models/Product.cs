using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneralStore116.Models
{
    public class Product
    {
        public int ProductId {get; set;}
        public string Name {get; set;}
        public int InventoryCount { get; set; }
        public bool IsFood { get; set; }
        public decimal Price { get; set; }
    }
}