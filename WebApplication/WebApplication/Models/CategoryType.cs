﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CategoryType
    {
        
        [Key]
        public int Id { get; set; }

        [Display(Name="Category Type")]
        public String Name { get; set; }

        
        
    }
}