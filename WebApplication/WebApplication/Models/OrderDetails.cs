using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        //Foreign-key for MembershipType
        [Required]
        public int ProductId { get; set; }

        public int OrderId { get; set; }


        //Navigate Property
        public Product Product { get; set; }

        public Order Order { get; set; }


        public int Quantity { get; set; }

        public string UserId { get; set; }

        

    }
}