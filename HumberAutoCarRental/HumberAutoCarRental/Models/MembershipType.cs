using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumberAutoCarRental.Models
{
    public class MembershipType
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("SignUp Fee")]
        [DataType(DataType.Currency)]
        public short SignUpFee { get; set; }



        [Required]
        [DisplayName("One Day ")]
        public Byte ChargeRateOneDay { get; set; }


        [Required]
        [DisplayName("One Week ")]
        public Byte ChargeRateOneWeek { get; set; }

        [Required]
        [DisplayName("One Month ")]
        public Byte ChargeRateOneMonth { get; set; }


        [Required]
        [DisplayName("Three Months ")]
        public Byte ChargeRateThreeMonth { get; set; }

        [Required]
        [DisplayName("Six Months ")]
        public Byte ChargeRateSixMonth { get; set; }

        [Required]
        [DisplayName("One Year ")]
        public Byte ChargeRateOneYear { get; set; }


    }
}