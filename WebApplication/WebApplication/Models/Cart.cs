using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get { return Quantity * Price; } }

        public string Image { get; set; }

        //Navigate Property
        public Product Product { get; set; }

        public string ProductName { get; set; }
        
        public string ProductDescription { get; set; }

        //Foreign-key for MembershipType
        [Required]
        public int ProductId { get; set; }

    }
} 