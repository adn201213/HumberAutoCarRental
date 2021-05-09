using HumberAutoCarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumberAutoCarRental.Extensions
{
    public static class ImagesExtensions
    {
        public static IEnumerable<HomeImageModel> GetCarHomeImages(this List<HomeImageModel> homeImages, ApplicationDbContext db = null, string search = null)
        {
            try
            {
                if (db == null) 
                {
                    db = ApplicationDbContext.Create();
                        
                 }

                homeImages = (from b in db.Cars
                              select new HomeImageModel
                              {
                                  CarId = b.ID,
                                  CarName = b.CarName,
                                  Description = b.Description,
                                  ImageUrl = b.ImageUrl,
                                  Link = "/CarDetail/Index/" + b.ID
                              }).ToList();

                if (search != null)
                {
                    return homeImages.Where(t => t.CarName.ToLower().Contains(search.ToLower())).OrderBy(t => t.CarName);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return homeImages.OrderBy(t => t.CarName);

        }
    }
}