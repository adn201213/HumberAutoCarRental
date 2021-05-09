using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumberAutoCarRental.Extensions;
using HumberAutoCarRental.Models;
using HumberAutoCarRental.ViewModel;

namespace HumberAutoCarRental.Controllers
{
   

    public class HomeController : Controller
    {
        public ActionResult Index(string search = null)
        {
            var homeimages = new List<HomeImageModel>().GetCarHomeImages(ApplicationDbContext.Create(), search);
            var count = homeimages.Count() / 4;

            var model = new List<HomeImageViewModel>();

            for (int i = 0; i <= count; i++)
            {
                model.Add(new HomeImageViewModel
                {
                    HomeImages = homeimages.Skip(i * 4).Take(4)
                });
            }


            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}