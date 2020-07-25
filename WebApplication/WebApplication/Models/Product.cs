using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "ImageUrl")]
        [MaxLength(255)]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }


        //Navigate Property
        //[ForeignKey("CategoryTypesId")]
        //public virtual CategoryType CategoryType { get; set; }
        public CategoryType CategoryType { get; set; }

        //public IEnumerable<CategoryType> CategoryTypes { get; set; }

        //Foreign-key for MembershipType
        [Required]
        public int CategoryTypeId { get; set; }


              

        
        

        
    }
}