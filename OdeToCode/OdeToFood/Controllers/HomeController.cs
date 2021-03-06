﻿using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OdeToFood.Controllers
{
    
    public class HomeController : Controller
    {
        
        OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.Restaurants
                .Where(r => r.Name.StartsWith(term))
                //.Take(10)
                .Select(r => new
                {
                    label = r.Name
                });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string SearchTerm = null, int page = 1)
        {
            //try
            //{
            //commented below two lines for implementing linq 
            //var model = _db.Restaurants.ToList();
            //return View(model);
            //}
            //catch(Exception e)
            //{
            //    throw e.InnerException;
            //}

            //using normal linq
            //    var model =
            //        from r in _db.Restaurants
            //        orderby r.Reviews.Average(review => review.Rating)
            //        select new RestaurantListViewModel
            //        {
            //            Id = r.Id,
            //            Name = r.Name,
            //            City = r.City,
            //            Country = r.Country,
            //            CountOfReviews = r.Reviews.Count()
            //};


            //using lamda expressions

            var model =
                _db.Restaurants
                .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                .Where(r => SearchTerm == null || r.Name.StartsWith(SearchTerm))
                .Select(r => new RestaurantListViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    City = r.City,
                    Country = r.Country,
                    CountOfReviews = r.Reviews.Count()
                }).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }
            return View(model);
        }

        
        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Scott";
            model.Location = "Maryland, USA";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
