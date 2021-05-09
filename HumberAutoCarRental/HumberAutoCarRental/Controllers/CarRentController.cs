using HumberAutoCarRental.Models;
using HumberAutoCarRental.Utility;
using HumberAutoCarRental.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

namespace HumberAutoCarRental.Controllers
{

   [Authorize]
    public class CarRentController : Controller
    {
        private ApplicationDbContext db;
        public CarRentController()
        {
            db = ApplicationDbContext.Create();
        }

        //Get Method
        public ActionResult Create(string carName = null, string platId = null)
        {
            if (carName != null && platId != null)
            {
              CarRentalViewModel model = new CarRentalViewModel
              {

                  CarName = carName,
                  PlateId = platId
              };
                return View(model);
            }
            return View(new CarRentalViewModel() );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarRentalViewModel carRent)
        {
            if (ModelState.IsValid)
            {
                var email = carRent.Email;

                var userDetails = from u in db.Users
                                  where u.Email.Equals(email)
                                  select new {u.Id};

                var PlateId = carRent.PlateId;

                Car carSelected = db.Cars.Where(b => b.PlateId == PlateId).FirstOrDefault();

                var rentalDuration = carRent.RentalDuration;
                double rentalPrice = 0;
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes
                                 on u.MembershipTypeId equals m.Id
                                 where u.Email.Equals(email)
                                 //select new { m.ChargeRateOneMonth, m.ChargeRateSixMonth };
                                 select new { m.ChargeRateOneDay,m.ChargeRateOneWeek,m.ChargeRateOneMonth,
                                     m.ChargeRateThreeMonth,m.ChargeRateSixMonth,m.ChargeRateOneYear };
                if (carRent.RentalDuration == SD.OneDayCount)
                {
                    rentalPrice = Convert.ToDouble(carSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneDay) / 100;
                }

                if (carRent.RentalDuration == SD.OneWeekCount)
                {

                    rentalPrice = (Convert.ToDouble(carSelected.Price * 7) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneWeek)) / 100;
                }
                if (carRent.RentalDuration == SD.OneMonthCount)
                {

                    rentalPrice = Convert.ToDouble(carSelected.Price * 30) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
                }
                if (carRent.RentalDuration == SD.ThreeMonthCount)
                {

                    rentalPrice = Convert.ToDouble(carSelected.Price * 90) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateThreeMonth) / 100;
                }
                if (carRent.RentalDuration == SD.SixMonthCount)
                {

                    rentalPrice = Convert.ToDouble(carSelected.Price * 180) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                }
                if (carRent.RentalDuration == SD.OneYearCount)
                {
                    rentalPrice = Convert.ToDouble(carSelected.Price * 360) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneYear) / 100;
                }

               
                CarRent modelToAddToDb = new CarRent
                {
                   CarId = carSelected.ID,
                    RentalPrice = rentalPrice,
                    ScheduledEndDate = carRent.ScheduledEndDate,
                    RentalDuration = carRent.RentalDuration,
                   Status = CarRent.StatusEnum.Approved,
                   UserId = userDetails.ToList()[0].Id
                };

               carSelected.Availability = 0;
                db.CarRental.Add(modelToAddToDb);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: CarRent
       public ActionResult Index(int? pageNumber, string option = null, string search = null)
        {
            string userid = User.Identity.GetUserId();

            var model = from br in db.CarRental
                        join b in db.Cars on br.CarId equals b.ID
                        join u in db.Users on br.UserId equals u.Id
                        select new CarRentalViewModel
                        {
                            Id = br.Id,
                            CarId = b.ID,
                            RentalPrice = br.RentalPrice,
                            Price = b.Price,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            BirthDate = u.BirthDate,
                            ScheduledEndDate = br.ScheduledEndDate,
                            Maker = b.Maker,
                            Avaibility = b.Availability,
                            DateAdded = b.DateAdded,
                            Description = b.Description,
                            Email = u.Email,
                            StartDate = br.StartDate,
                            CarTypeId = b.CarTypeId,
                            CarType = db.CarTypes.Where(g => g.Id.Equals(b.CarTypeId)).FirstOrDefault(),
                            PlateId = b.PlateId,
                            ImageUrl = b.ImageUrl,
                            MadeYear = b.MadeYear,
                            RentalDuration = br.RentalDuration,
                            Status = br.Status.ToString(),
                            CarName = b.CarName,
                            UserId = u.Id

                        };

            if (option == "email" && search.Length > 0)
            {
                model = model.Where(u => u.Email.Contains(search));
            }
            if (option == "name" && search.Length > 0)
            {
                model = model.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search));
            }
            if (option == "status" && search.Length > 0)
            {
                model = model.Where(u => u.Status.Contains(search));
            }

            if (!User.IsInRole(SD.AdminUserRole))
            {
                model = model.Where(u => u.UserId.Equals(userid));
            }

        
          return View(model.ToList().ToPagedList(pageNumber ?? 1, 5));
        }


        //The user will reserve the car and the data will be inserted in database
        [HttpPost]
        public ActionResult Reserve(CarRentalViewModel car)
        {
            var userid = User.Identity.GetUserId();
            Car carToRent = db.Cars.Find(car.CarId);
            double rentalPrice = 0;

            if (userid != null)
            {
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes
                                 on u.MembershipTypeId equals m.Id
                                 where u.Id.Equals(userid)
                                 select new { m.ChargeRateOneDay,m.ChargeRateOneWeek,m.ChargeRateOneMonth,
                                     m.ChargeRateThreeMonth,m.ChargeRateSixMonth,m.ChargeRateOneYear };
                if (car.RentalDuration == SD.OneDayCount)
                {
                    rentalPrice = Convert.ToDouble(carToRent.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneDay) / 100;
                } 
                
                if (car.RentalDuration == SD.OneWeekCount) {

                    rentalPrice = (Convert.ToDouble(carToRent.Price*7) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneWeek) )/ 100;
                }
                 if (car.RentalDuration == SD.OneMonthCount) {

                    rentalPrice = Convert.ToDouble(carToRent.Price*30) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
                }
                if (car.RentalDuration == SD.ThreeMonthCount)
                {

                    rentalPrice = Convert.ToDouble(carToRent.Price*90) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateThreeMonth) / 100;
                }
                if (car.RentalDuration == SD.SixMonthCount)
                {

                    rentalPrice = Convert.ToDouble(carToRent.Price*180) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                }
                if (car.RentalDuration == SD.OneYearCount)
                {
                    rentalPrice = Convert.ToDouble(carToRent.Price*360) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneYear) / 100;
                }


                var userInDb = db.Users.SingleOrDefault(c => c.Id == userid);
                //if (userInDb.RentalCount == 10)
                //{
                //    userInDb.RentalCount++;
                //    rentalPr = rentalPr - (rentalPr * 20 / 100);
                //}
                CarRent carRent = new CarRent
                {
                    CarId = carToRent.ID,
                    UserId = userid,
                    RentalDuration = car.RentalDuration,
                    RentalPrice = rentalPrice,

                    ScheduledEndDate = car.ScheduledEndDate,
                  
                    Status = CarRent.StatusEnum.Requested,

                };

                db.CarRental.Add(carRent);
                var carInDb = db.Cars.SingleOrDefault(c => c.ID == car.CarId);

                carInDb.Availability=0;

                db.SaveChanges();
                return RedirectToAction("Index", "CarRent");
            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarRent carRent = db.CarRental.Find(id);

            var model = getVMFromCarRent(carRent);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        //Decline GET Method
        public ActionResult Decline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarRent carRent = db.CarRental.Find(id);

            var model = getVMFromCarRent(carRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decline(CarRentalViewModel model)
        {
            if (model.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
               CarRent carRent = db.CarRental.Find(model.Id);
                carRent.Status = CarRent.StatusEnum.Rejected;
              //  var userInDb = db.Users.SingleOrDefault(c => c.Id == carRent.UserId);
            //    if (userInDb.RentalCount == 11)
             //   {
                //    userInDb.RentalCount--;
             //   }
                Car carInDb = db.Cars.Find(carRent.CarId);
               carInDb.Availability = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Approve GET Method
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarRent carRent = db.CarRental.Find(id);

            var model = getVMFromCarRent(carRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Approve", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(CarRentalViewModel model)
        {
            if (model.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                CarRent carRent = db.CarRental.Find(model.Id);
                carRent.Status = CarRent.StatusEnum.Approved;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        //PickUp Get Method
        public ActionResult PickUp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           CarRent carRent = db.CarRental.Find(id);

            var model = getVMFromCarRent(carRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Approve", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickUp(CarRentalViewModel model)
        {
            if (model.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                CarRent carRent = db.CarRental.Find(model.Id);
                carRent.Status = CarRent.StatusEnum.Rented;
                carRent.StartDate = DateTime.Now;


                if (carRent.RentalDuration == SD.OneDayCount) {
                    carRent.ScheduledEndDate = DateTime.Now.AddDays(Convert.ToInt32(1));
                } else if (carRent.RentalDuration == SD.OneWeekCount)
                {
                    carRent.ScheduledEndDate = DateTime.Now.AddDays(Convert.ToInt32(7));
                }
                else if (carRent.RentalDuration == SD.OneMonthCount)
                {
                    carRent.ScheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(1));
                }
                else if (carRent.RentalDuration == SD.ThreeMonthCount) {
                    carRent.ScheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(3));
                }
                else if (carRent.RentalDuration == SD.SixMonthCount)
                {
                    carRent.ScheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(6));
                }
                else if (carRent.RentalDuration == SD.OneYearCount)
                {
                    carRent.ScheduledEndDate = DateTime.Now.AddYears(Convert.ToInt32(1));
                }


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Return Get Method
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarRent carRent = db.CarRental.Find(id);

            var model = getVMFromCarRent(carRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Approve", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(CarRentalViewModel model)
        {
            if (model.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                CarRent carRent = db.CarRental.Find(model.Id);
               carRent.Status = CarRent.StatusEnum.Closed;

                carRent.AdditionalCharge = model.AdditionalCharge;
                Car carInDb = db.Cars.Find(carRent.CarId);

               // var userInDb = db.Users.Single(u => u.Id == bookRent.UserId);
             //   if (userInDb.RentalCount == 11)
              //  {
              //      userInDb.RentalCount = 0;
              //  }
               // else
              //  {
              //      userInDb.RentalCount++;
             //   }

                carInDb.Availability = 1;
                carRent.ActualEndDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Delete GET Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CarRent carRent = db.CarRental.Find(id);

            var model = getVMFromCarRent(carRent);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            if (Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                CarRent carRent = db.CarRental.Find(Id);
                

                var carInDb = db.Cars.Where(b => b.ID.Equals(carRent.CarId)).FirstOrDefault();
             //   var userInDb = db.Users.SingleOrDefault(c => c.Id == carRent.UserId);

                if (!carRent.Status.ToString().ToLower().Equals("rented")
                    || !carRent.Status.ToString().ToLower().Equals("closed"))
                {
                    carInDb.Availability = 1;
                }
              //  else
              //  {
                    //This else would be called in all scenarios except when user has a book with them and never returns
                  //  if (userInDb.RentalCount == 11)
                  //  {
                   //     userInDb.RentalCount--;
                  //  }
              //  }

                db.CarRental.Remove(carRent);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }






        //This function to get view model from Car Rent

        private CarRentalViewModel getVMFromCarRent(CarRent carRent)
        {
            Car carSelected = db.Cars.Where(b => b.ID == carRent.CarId).FirstOrDefault();
            var userDetails = from u in db.Users
                              where u.Id.Equals(carRent.UserId)
                              select new { u.Id, u.FirstName, u.LastName, u.BirthDate, u.Email };

            CarRentalViewModel model = new CarRentalViewModel
            {
                Id = carRent.Id,
                CarId = carSelected.ID,
                RentalPrice = carRent.RentalPrice,
                Price = carSelected.Price,
                FirstName = userDetails.ToList()[0].FirstName,
                LastName = userDetails.ToList()[0].LastName,
                BirthDate = userDetails.ToList()[0].BirthDate,
                ScheduledEndDate = carRent.ScheduledEndDate,
                Maker = carSelected.Maker,
                AdditionalCharge = carRent.AdditionalCharge,
                StartDate = carRent.StartDate,
                Avaibility = carSelected.Availability,
                DateAdded = carSelected.DateAdded,
                Description = carSelected.Description,
                Email = userDetails.ToList()[0].Email,
                CarTypeId = carSelected.CarTypeId,
                CarType = db.CarTypes.FirstOrDefault(g => g.Id.Equals(carSelected.CarTypeId)),
                PlateId = carSelected.PlateId,
                ImageUrl = carSelected.ImageUrl,
                MadeYear = carSelected.MadeYear,
                RentalDuration = carRent.RentalDuration,
                Status = carRent.Status.ToString(),
                CarName = carSelected.CarName,
                UserId = userDetails.ToList()[0].Id
            };

            return model;
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