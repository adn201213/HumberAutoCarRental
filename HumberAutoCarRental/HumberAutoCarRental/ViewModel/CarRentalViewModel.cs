using HumberAutoCarRental.Models;
using HumberAutoCarRental.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static HumberAutoCarRental.Models.Car;

namespace HumberAutoCarRental.ViewModel
{
    public class CarRentalViewModel
    {

        ////   private const object DataType;
        //This id for car Rental
        public int Id { get; set; }



        //Car Details
        public int CarId { get; set; }
        public string PlateId { get; set; }

     
        public string CarName { get; set; }
        public string Maker { get; set; }
        public string Description { get; set; }


        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }


        [Range(0, 1000)]
        public int Avaibility { get; set; }


        [DataType(DataType.Currency)]
        public Double Price { get; set; }

        [System.ComponentModel.DisplayName("Date Added")]
        [DispalyFormat(DataFormatString = "{0 : MMM dd yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DateAdded { get; set; }

        [DisplayName("Made Year")]
        [DispalyFormat(DataFormatString = "{0 : yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? MadeYear { get; set; }

        [DisplayName("Car Type Id")]
        public int CarTypeId { get; set; }


        [DisplayName("Car Type")]
        public CarType CarType { get; set; }

        //Rental Detailes
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Additional End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime? ActualEndDate { get; set; }

        [DisplayName("Scheduled End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime? ScheduledEndDate { get; set; }


        [DisplayName("Additional Charge")]
        public double? AdditionalCharge { get; set; }


        [DisplayName("Rental Price")]
        public double RentalPrice { get; set; }


        public string RentalDuration { get; set; }

        public string Status { get; set; }

        [DisplayName("Rental Price")]
        public double RentalPriceOneDay { get; set; }

        [DisplayName("Rental Price")]
        public double RentalPriceOneWeek { get; set; }

        [DisplayName("Rental Price")]
        public double RentalPriceOneMonth { get; set; }
        [DisplayName("Rental Price")]
        public double RentalPriceThreeMonth { get; set; }


        [DisplayName("Rental Price")]
        public double RentalPriceSixMonth { get; set; }

        [DisplayName("Rental Price")]
        public double RentalPriceOneYear { get; set; }

        //Users Details
        public string UserId { get; set; }

       
        public string Email { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }


        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Name { get { return FirstName + " " + LastName; } }

        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime? BirthDate { get; set; }


        public int RentalCount { get; set; }
        public object Statusenum { get; internal set; }
        public string actionName
        {
            get
            {
                if (Status.ToLower().Contains(SD.RequestedLower))
                {
                    return "Approve";
                }
                if (Status.ToLower().Contains(SD.ApprovedLower))
                {
                    return "PickUp";
                }
                if (Status.ToLower().Contains(SD.RentedLower))
                {
                    return "Return";
                }
                return null;
            }
        }

    }

}