﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        
        public String UserId { get; set; }

        

        public DateTime CreatedAt { get; set; }




    }
}