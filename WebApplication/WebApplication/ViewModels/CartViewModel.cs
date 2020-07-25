using System;
using WebApplication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModels
{
    public class CartViewModel
    {

        public CartViewModel()
        {

        }

        public CartViewModel(Cart row)
        {
            Id = row.Id;
            
            Quantity = row.Quantity;
            Price = row.Price;
            Total = row.Total;
            Image = row.Image;
            ProductName = row.ProductName;
            ProductDescription = row.ProductDescription;
            ProductId = row.ProductId;

        }
      
        //Navigate Property
        public Product Product { get; set; }

        public int Id { get; set; }
                

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

        

        public string Image { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        //Foreign-key for MembershipType
        [Required]
        public int ProductId { get; set; }
    }
}