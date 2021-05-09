using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HumberAutoCarRental.Models;
using HumberAutoCarRental.Utility;
using HumberAutoCarRental.ViewModel;

namespace HumberAutoCarRental.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]

    public class CarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Car
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.CarType);
            return View(cars.ToList());
        }

        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            var model = new CarViewModel
            {
            Car=car,
            CarTypes=db.CarTypes.ToList()
            };
            return View(model);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            var carTypes = db.CarTypes.ToList();

            var model = new CarViewModel
            {
                CarTypes = db.CarTypes.ToList()
            };
            return View(model);
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarViewModel carVM)
        {
            var car = new Car
            {
                PlateId = carVM.Car.PlateId,
                CarName = carVM.Car.CarName,
                Maker = carVM.Car.Maker,
                Description = carVM.Car.Description,
                ImageUrl = carVM.Car.ImageUrl,
                Availability = carVM.Car.Availability,
                Price = carVM.Car.Price,
                DateAdded = carVM.Car.DateAdded,
                MadeYear = carVM.Car.MadeYear,
                CarTypeId = carVM.Car.CarTypeId,
                CarType = carVM.Car.CarType
            };
            
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            carVM.CarTypes = db.CarTypes.ToList();
            return View(carVM);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            var model = new CarViewModel
            {
                Car=car,
                CarTypes=db.CarTypes.ToList()
            };
            return View(model);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Edit(CarViewModel carVM)
        {

            var car = new Car
            {
                ID= carVM.Car.ID,
                PlateId = carVM.Car.PlateId,
                CarName = carVM.Car.CarName,
                Maker = carVM.Car.Maker,
                Description = carVM.Car.Description,

                ImageUrl = carVM.Car.ImageUrl,
                Availability = carVM.Car.Availability,
                Price = carVM.Car.Price,
                DateAdded = carVM.Car.DateAdded,

                MadeYear = carVM.Car.MadeYear,

                CarTypeId = carVM.Car.CarTypeId,

                CarType = carVM.Car.CarType
            };

            if (ModelState.IsValid)
            {
                //for update
                var CarInDb = db.Cars.FirstOrDefault(g => g.ID.Equals(carVM.Car.ID));
                CarInDb.ID = carVM.Car.ID;
                CarInDb.PlateId = carVM.Car.PlateId;
                CarInDb.CarName = carVM.Car.CarName;
                CarInDb.Maker = carVM.Car.Maker;
                CarInDb.Description = carVM.Car.Description;
                CarInDb.ImageUrl = carVM.Car.ImageUrl;
                CarInDb.Availability = carVM.Car.Availability;
                CarInDb.Price = carVM.Car.Price;
                CarInDb.DateAdded = carVM.Car.DateAdded;
                CarInDb.MadeYear = carVM.Car.MadeYear;
                CarInDb.CarTypeId = carVM.Car.CarTypeId;
                CarInDb.CarType = carVM.Car.CarType;
              //  db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            carVM.CarTypes = db.CarTypes.ToList();


            return View(carVM);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            var model = new CarViewModel
            {
                Car = car,
                CarTypes = db.CarTypes.ToList()
            };
            return View(model);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
