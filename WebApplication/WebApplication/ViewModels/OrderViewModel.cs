using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {

        }

        public OrderViewModel(Order row)
        {

            
        }


        
        public Order Order { get; set; }
                

        public DateTime CreatedAt { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }

        public decimal Price { get; set; }

        //Navigate Property
        public Product Product { get; set; }

        //Foreign-key for MembershipType
        [Required]
        public int ProductId { get; set; }
    }
}