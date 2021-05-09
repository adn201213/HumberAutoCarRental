using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;//this for eager loading
using HumberAutoCarRental.Models;
using HumberAutoCarRental.Utility;
using HumberAutoCarRental.ViewModel;
using Microsoft.AspNet.Identity;

namespace HumberAutoCarRental.Controllers
{
    public class CarDetailController : Controller
    {

        private ApplicationDbContext db;

        public CarDetailController()
        {
            db = ApplicationDbContext.Create();
        }


        // GET: CarDetail
        public ActionResult Index(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == userid);

            var carModel = db.Cars.Include(b => b.CarType).SingleOrDefault(b => b.ID == id);
          //variables will hold the prices after discount
            var  rentalPrice = 0.0;
            var oneDayRental = 0.0;
            var oneWeekRental = 0.0;
            var oneMonthRental = 0.0;
            var threeMonthRental = 0.0;
            var sixMonthRental = 0.0;
            var oneYearRental = 0.0;
          
            //if normal user we get the prive due to their membership
            if (userid != null && !User.IsInRole(SD.AdminUserRole))
            {
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Id.Equals(userid)
                                 select new { m.ChargeRateOneDay,m.ChargeRateOneWeek, m.ChargeRateOneMonth,
                                     m.ChargeRateThreeMonth,m.ChargeRateSixMonth,m.ChargeRateOneYear};
                oneDayRental = Convert.ToDouble(carModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneDay) / 100;
                oneWeekRental = Convert.ToDouble(carModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneWeek) / 100;
                oneMonthRental = Convert.ToDouble(carModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
                threeMonthRental = Convert.ToDouble(carModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateThreeMonth) / 100;
                sixMonthRental = Convert.ToDouble(carModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                oneYearRental = Convert.ToDouble(carModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneYear) / 100;
            }


            CarRentalViewModel model = new CarRentalViewModel
            {
                CarId = carModel.ID,
                PlateId = carModel.PlateId,
                Maker = carModel.Maker,
                Avaibility = carModel.Availability,
                DateAdded = carModel.DateAdded,
                MadeYear = carModel.MadeYear,
                Description = carModel.Description,
                CarType = db.CarTypes.FirstOrDefault(g => g.Id.Equals(carModel.CarTypeId)),
                CarTypeId = carModel.CarTypeId,
                ImageUrl = carModel.ImageUrl,
                Price = carModel.Price,
                CarName = carModel.CarName,
                UserId = userid,
                RentalPrice = rentalPrice,
              //these variable with new prices  
                RentalPriceOneDay=oneDayRental,
                 RentalPriceOneWeek=oneWeekRental,
                  RentalPriceOneMonth=oneMonthRental,
                  RentalPriceThreeMonth=threeMonthRental,
                  RentalPriceSixMonth=sixMonthRental,
                  RentalPriceOneYear=oneYearRental
            };

            return View(model);
        }


        //To dispose db object
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }




    }
}