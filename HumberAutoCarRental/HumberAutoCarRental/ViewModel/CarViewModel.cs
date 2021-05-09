using HumberAutoCarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAutoCarRental.ViewModel
{
    public class CarViewModel
    {

        public IEnumerable<CarType> CarTypes{ get; set; }
        public Car Car { get; set; }

    }
}