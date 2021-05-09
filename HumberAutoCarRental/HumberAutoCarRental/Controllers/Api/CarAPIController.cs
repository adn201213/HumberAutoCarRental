using HumberAutoCarRental.Models;
using HumberAutoCarRental.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HumberAutoCarRental.Controllers.Api
{
    public class CarAPIController : ApiController
    {

        private ApplicationDbContext db;
        public CarAPIController()
        {
            db = ApplicationDbContext.Create();
        }
        // get by carName
        public IHttpActionResult Get(string query = null)
        {
            var carQuery = db.Cars.Where(b => b.CarName.ToLower().Contains(query.ToLower()));

            return Ok(carQuery.ToList());
        }

        //get by Type could be price or availbility
        public IHttpActionResult Get(string type, string plateId = null, string rentalDuration = null, string email = null)
        {

            if (type != null && plateId != null)
            {

                if (type.Equals("price"))
                {
                    Car CarQuery = db.Cars.Where(b => b.PlateId.Equals(plateId)).SingleOrDefault();
                    var chargeRate = from u in db.Users
                                     join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                     where u.Email.Equals(email)
                                     select new
                                     {
                                         m.ChargeRateOneMonth,
                                         m.ChargeRateSixMonth,
                                         m.ChargeRateOneDay,
                                         m.ChargeRateOneWeek,
                                         m.ChargeRateOneYear,
                                         m.ChargeRateThreeMonth
                                     };

                    var price = Convert.ToDouble(CarQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;

                    if (rentalDuration == SD.SixMonthCount)
                    {
                        price = Convert.ToDouble(CarQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                    }
                    if (rentalDuration == SD.OneDayCount)
                    {
                        price = Convert.ToDouble(CarQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneDay) / 100;
                    }
                    if (rentalDuration == SD.OneWeekCount)
                    {
                        price = Convert.ToDouble(CarQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneWeek) / 100;
                    }
                    if (rentalDuration == SD.ThreeMonthCount)
                    {
                        price = Convert.ToDouble(CarQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateThreeMonth) / 100;
                    }

                    if (rentalDuration == SD.OneYearCount)
                    {
                        price = Convert.ToDouble(CarQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneYear) / 100;
                    }

                    return Ok(price);
                }
                else
                {

                    Car CarQuery = db.Cars.Where(b => b.PlateId.Equals(plateId)).SingleOrDefault();

                    return Ok(CarQuery.Availability);

                }
            }
            else {
                return null;
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }







    }
}
