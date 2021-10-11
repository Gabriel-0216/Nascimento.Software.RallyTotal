using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.RallyTotal.WebApp.Models
{
    public class Person
    {
        [Display(Name = "Id")]

        public int PersonId { get; set; }
        [Display(Name = "Name")]
        [StringLength(255, ErrorMessage = "The maximum length is 255 characters.")]
        [Required(ErrorMessage = "You have to write a name.")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "You have to inform your phoner number")]
        [StringLength(20, ErrorMessage = "Your phone number must have a maximum of 20 characters")]
        public string PhoneNumber { get; set; }

        [StringLength(255, ErrorMessage = "The country name can have a maximum of 255 characters")]
        public string Country { get; set; }

    }
}
