using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HumberAutoCarRental.Models
{

    //Model class for car type example( big, small, van, luxurius car and so on}
  [Required]
    public class CarType { 
    public int Id { get; set; }

        [Required]
        [DisplayName("Car Type")]
    public String Name { get; set; }


    }

    internal class RequiredAttribute : Attribute
    {
    }
}