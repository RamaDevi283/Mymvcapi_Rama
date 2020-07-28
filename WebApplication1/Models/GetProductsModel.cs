using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetProductsModel
    {
        public List<UGCountriesModel> Countries { get; set; }
        public List<UGProductsModel> Products { get; set; }
      
    }
      public class UGCountriesModel
        {
            public int ID { get; set; }
            public string Country { get; set; }
        }
        public class UGProductsModel
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Count { get; set; }
        }
}