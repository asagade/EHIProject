using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EHIProject.Data
{
    public class CreateContactDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "First name is to long")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "Last name is to long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone Number is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string PhoneNumber { get; set; }
    }
    public class UpdateContactDTO : CreateContactDTO
    {
        
    }

    public class ContactDTO : CreateContactDTO
    {
        public int Id { get; set; }
    }
}
