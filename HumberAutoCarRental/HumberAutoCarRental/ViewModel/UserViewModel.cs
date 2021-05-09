using HumberAutoCarRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumberAutoCarRental.ViewModel
{
    public class UserViewModel
    {

        [System.ComponentModel.DataAnnotations.Required]
        public string Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public ICollection<MembershipType> MembershipTypes { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int MembershipTypeId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string FirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string LastName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime BirthDate { get; set; }

        public bool Disabled { get; set; }

    }
}