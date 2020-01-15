using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Genre.Name.Contains(query) ||
                        g.Venue.Contains(query));
            }
            
            var userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances
                .GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                Heading = "Upcoming Gigs",
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = query,
                Attendances = attendances
            };


            return View("Gigs",viewModel);
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