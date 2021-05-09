using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumberAutoCarRental.Models
{
    public class Car
    {
     [Required]
     public int ID { get; set; }
     [Required]
     [DisplayName("Plate Id")]
        public String PlateId { get; set; }
        [DisplayName("Car Name")]
        [Required]
     public String CarName { get; set; }//example civic/corolo
     [Required]
     public String Maker { get; set; }//example Honda,Toyota
     [Required]
     public String Description { get; set; }
     [Required]
     public String ImageUrl { get; set; }
     [Required]
     public int Availability { get; set; }//If exist or rented or reserved

     [Required]
     [DataType(DataType.Currency)]
     public Double Price { get; set; }


        [DisplayName("Date Added")]
       // [DispalyFormat(DataFormatString="{0 : MMM dd yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DateAdded { get; set; }

        [DisplayName("Made Year")]
        [DispalyFormat(DataFormatString = "{0 : yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? MadeYear { get; set; }


        [Required]
        [DisplayName("Car Type Id")]
        public int CarTypeId { get; set; }
        [Required]
        [DisplayName("Car Type")]
        public CarType CarType { get; set; }
       
        internal class DispalyFormatAttribute : Attribute
    {
        public string DataFormatString { get; set; }
    }
}
}