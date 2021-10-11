using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class Sale
    {

        public int SaleId { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage ="You must inform the sale title")]
        [StringLength(255, MinimumLength = 5, ErrorMessage ="The title must have a minimum of 5 and a maximum of 255 characters")]
        public string SaleTitle { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage ="You must inform where are you from")]
        public string Country { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "You must inform the price")]
        public double Price { get; set; }

        [Display(Name = "Vendor")]
        [Required(ErrorMessage = "You must inform the Vendor")]
        public int PersonID { get; set; }

        [Display(Name = "Category")]

        [Required(ErrorMessage ="You must choose one category")]
        public int CategoryId { get; set; }
        [Display(Name = "Photo")]
        [Required(ErrorMessage ="You must upload at least one photo")]
        public int Photo { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage ="It's necessary to fill this form.")]
        public string DescriptionSale { get; set; }
    }
}
