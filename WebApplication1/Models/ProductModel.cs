using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter product name")]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select value = or > 1")]
        public int Count { get; set; }
        public List<UGCountriesModel1> Countries { get; set; }

    
    }
    public class UGCountriesModel1
    {
        public int ID { get; set; }
        public string Country { get; set; }
    }

}