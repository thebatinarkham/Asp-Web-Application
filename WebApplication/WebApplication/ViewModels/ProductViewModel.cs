using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class ProductViewModel
    {

        public ProductViewModel()
        {
            Id = 0;
        }

        public ProductViewModel(Product row)
        {
            Id = row.Id;
            Name = row.Name;
            Price = row.Price;
            ImagePath = row.ImagePath;
            Description = row.Description;
            //CategoryType = row.CategoryType;
            CategoryTypeId = row.CategoryTypeId;
            
            
        }
        
        public IEnumerable<CategoryType> CategoryTypes { get; set; }
        public Product Product { get; set; }
        
        public int? Id { get; set; }        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        //Navigate Property
        public CategoryType CategoryType { get; set; }

        //Foreign-key for MembershipType
        [Required]
        [Display(Name = "CategoryTypes")]
        public int? CategoryTypeId { get; set; }
    }
}