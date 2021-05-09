using HumberAutoCarRental.Models;
using HumberAutoCarRental.Utility;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HumberAutoCarRental.Controllers
{

    [Authorize(Roles = SD.AdminUserRole)]
    public class CarTypeController : Controller
    {
        private ApplicationDbContext db;
        public CarTypeController()
        {
            db = new ApplicationDbContext();
        }
        // GET: CarType
        public ActionResult Index()
        {
           return View(db.CarTypes.ToList());
            //
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarType carType)
        {

            if (ModelState.IsValid)
            {
                db.CarTypes.Add(carType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarType carType = db.CarTypes.Find(id);
            if (carType == null)
            {
                return HttpNotFound();
            }
            return View(carType);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarType carType = db.CarTypes.Find(id);
            if (carType == null)
            {
                return HttpNotFound();
            }
            return View(carType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarType carType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carType).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carType);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarType carType = db.CarTypes.Find(id);
            if (carType == null)
            {
                return HttpNotFound();
            }
            return View(carType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
           
                CarType carType = db.CarTypes.Find(id);
                db.CarTypes.Remove(carType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
        



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}