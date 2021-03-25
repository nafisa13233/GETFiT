using GETFiT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GETFiT.Areas.Trainer.Controllers
{
    [Authorize(Roles ="Trainer")]
    public class HomeController : Controller
    {

        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Trainer/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientProfile(int id)
        {
            var client = _db.Clients.Where(p => p.Id == id).FirstOrDefault();
            return View(client);
        }
    }
}