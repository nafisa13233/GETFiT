using Newtonsoft.Json;
using GETFiT.Areas.Admin.Models;
using GETFiT.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GETFiT.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        ApplicationDbContext _db;

        public HomeController()
        {
            _db = new ApplicationDbContext();
        }

        // GET: Admin/Home
        public ActionResult Index()
        {

            var trainers = _db.Trainers.ToList();
            var clients = _db.Clients.ToList();
            var appoinments = _db.Appointments.ToList();

            var model = new HomeViewModel
            {
                numberOfTrainers = trainers.Count(),
                numberOfClients = clients.Count(),
                numberOfAppointments = appoinments.Count()
            };


            return View(model);
        }


        public ActionResult Trainers(string search, int? page)
        {

            if (!String.IsNullOrEmpty(search))
            {
                var trainers1 = _db.Trainers.Where(d => d.User.Name.ToLower().Contains(search.ToLower()) ||
                    d.Speciality.ToLower().Contains(search.ToLower()) ||
                    d.Address.ToLower().Contains(search.ToLower()) ||
                    d.User.Email.ToLower().Contains(search.ToLower())).ToList().ToPagedList(page ?? 1, 6);
                return View(trainers1);
            }

            var trainers = _db.Trainers.ToList().ToPagedList(page ?? 1, 6);
            return View(trainers);
        }


        public ActionResult EditTrainerAccount(string id)
        {

            var user = _db.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();

            var model = new TrainerEditViewModel
            {
                id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.dateOfBirth.ToString("dd-MMMM-yyyy"),
                Address = user.trainer.Address,
                Education = user.trainer.Education,
                StartTime = user.trainer.StartTime,
                EndTime = user.trainer.EndTime,
                Qualification = user.trainer.Qualification,
                Speciality = user.trainer.Speciality,
                Gender = user.Gender
            };

            ViewBag.Gender = new List<SelectListItem>
            {
                new SelectListItem(){ Text = "Male", Value = "Male",},
                new SelectListItem(){ Text = "Female", Value = "Female",}
            };


            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult EditTrainerAccount(TrainerEditViewModel model)
        {
            var user = _db.Users.Where(d => d.Id.Equals(model.id)).FirstOrDefault();

            _db.Entry(user).State = EntityState.Modified;

            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.dateOfBirth = DateTime.Parse(model.DateOfBirth);
            user.trainer.Address = model.Address;
            user.trainer.Education = model.Education;
            user.trainer.StartTime = model.StartTime;
            user.trainer.EndTime = model.EndTime;
            user.trainer.Qualification = model.Qualification;
            user.trainer.Speciality = model.Speciality;
            user.Gender = model.Gender;

            TempData["Success"] = "Changes Saved!";

            ViewBag.Gender = new List<SelectListItem>
            {
                new SelectListItem(){ Text = "Male", Value = "Male",},
                new SelectListItem(){ Text = "Female", Value = "Female",}
            };

            _db.SaveChanges();

            return View(model);
        }

        public ActionResult Appointments()
        {
            var model = _db.Appointments.Where(ap => ap.trainer != null).ToList();
            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public String getAppointments()
        {
            var model = _db.Appointments.ToList();
            string jsonString = JsonConvert.SerializeObject(model, Formatting.Indented);
            return jsonString;
        }

        [Authorize(Roles = "Admin")]

        public ActionResult TrainerProfile(int id)
        {
            var model = _db.Trainers.Where(d => d.Id == id).FirstOrDefault();
            return View(model);
        }

        public ActionResult Clients(string search, int? page)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var clients = _db.Clients.Where(p => p.User.Name.ToLower().Contains(search.ToLower()) ||
                p.User.Email.ToLower().Contains(search.ToLower()) ||
                p.User.PhoneNumber.ToLower().Contains(search.ToLower())
                ).ToList().ToPagedList(page ?? 1, 6);

                return View(clients);
            }

            var client = _db.Clients.ToList().ToPagedList(page ?? 1, 6);

            return View(client);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult ClientsProfile(int id)
        {
            var model = _db.Clients.Where(d => d.Id == id).FirstOrDefault();
            return View(model);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult EditClientsAccount(string id)
        {
            var user = _db.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();

            var model = new EditViewModel
            {
                id = user.Id,
                Name = user.Name,
                DateOfBirth = user.dateOfBirth.ToShortDateString(),
                PhoneNumber = user.PhoneNumber
            };

            ViewBag.Gender = new List<SelectListItem>
            {
                new SelectListItem(){ Text = "Male", Value = "Male",},
                new SelectListItem(){ Text = "Female", Value = "Female",}
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult EditClientAccount(EditViewModel model)
        {
            var user = _db.Users.Where(u => u.Id.Equals(model.id)).FirstOrDefault();

            _db.Entry(user).State = EntityState.Modified;
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.dateOfBirth = DateTime.Parse(model.DateOfBirth);

            TempData["Success"] = "Changes Saved!";

            ViewBag.Gender = new List<SelectListItem>
            {
                new SelectListItem(){ Text = "Male", Value = "Male",},
                new SelectListItem(){ Text = "Female", Value = "Female",}
            };

            _db.SaveChanges();

            return View(model);
        }
    }
}