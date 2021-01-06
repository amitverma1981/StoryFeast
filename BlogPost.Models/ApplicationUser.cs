using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogPost.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string UserImage { get; set; }

        [MaxLength(500)]
        public string StreetAddress { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string PostalCode { get; set; }

        [NotMapped]
        public string Role { get; set; }

    }
}
