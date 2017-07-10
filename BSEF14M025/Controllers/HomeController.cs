using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;

namespace BSEF14M025.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            ViewBag.Message = "Your application description page.";
            List<TourDTO> tours = BAL.TourBO.GetAllTours();
            return View(tours);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register(int id)
        {
            TourDTO tour = BAL.TourBO.GetTourById(id);
            if (Security.SessionManager.IsValidUser == true)
            {
                Entities.RegisteredTravellerDTO rt = new RegisteredTravellerDTO();
                rt.RTID = 0;
                rt.TourID = id;
                rt.UserID = Security.SessionManager.User.UserID;
                BAL.RegisteredTravellerBO.Save(rt);
                return Redirect("~/Home/Show");
            }
            return Redirect("~/Home/Login");
        }
        public ActionResult UserLogin()
        {
            String UserName = Request["txtLogin"];
            String Password = Request["txtPassword"];

            List<UsersDTO> users = BAL.UsersBO.GetAllUsers();
            foreach(var user in users)
            {
                if (user.UserName.Equals(UserName) && user.Password.Equals(Password))
                {
                    Security.SessionManager.User = user;
                    return Redirect("~/Home/Show");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            if (Security.SessionManager.IsValidUser == true)
            {
                Security.SessionManager.ClearSession();
                return Redirect("~/Home/Show");
            }
            else
                return Redirect("~/Home/UserLogin");
        }
        public ActionResult AdminLogin()
        {
            String UserName = Request["txtLogin"];
            String Password = Request["txtPassword"];

            List<AdminDTO> admins = BAL.AdminBO.GetAllAdmins();
            foreach (var admin in admins)
            {
                if (admin.UserName.Equals(UserName) && admin.Password.Equals(Password))
                {
                    Security.AdminSessionManager.Admin = admin;
                    return Redirect("~/Home/AdminPanel");
                }
            }
            return View();
        }
        public ActionResult AdminPanel()
        {
            if (Security.AdminSessionManager.IsValidAdmin)
            {
                return View();
            }
            else
                return Redirect("~/Home/AdminLogin");
        }
        public ActionResult AdminTours()
        {
            if (Security.AdminSessionManager.IsValidAdmin)
            {
                List<Entities.TourDTO> tours = BAL.TourBO.GetAllTours();
                return View(tours);
            }
            else
                return Redirect("~/Home/AdminLogin");
        }
        public ActionResult AdminUsers()
        {
            if (Security.AdminSessionManager.IsValidAdmin)
            {
                List<UsersDTO> users = BAL.UsersBO.GetAllUsers();
                return View(users);
            }
            else
                return Redirect("~/Home/AdminLogin");
        }
        public ActionResult DeleteTour(int id)
        {
            if (Security.AdminSessionManager.IsValidAdmin)
            {
                BAL.TourBO.DeleteTour(id);
                return Redirect("~/Home/AdminTours");
            }
            else
                return Redirect("~/Home/AdminLogin");
        }
        public ActionResult SaveTour()
        {
            if (Security.AdminSessionManager.IsValidAdmin)
            {
                Entities.TourDTO newTour = new TourDTO();
                newTour.TourID = 0;
                newTour.FromCity = Request["txtFromCity"];
                newTour.ToCity = Request["txtToCity"];
                newTour.SubDeadLine = Convert.ToDateTime(Request["txtSubDeadLine"]);
                newTour.Departure = Convert.ToDateTime(Request["txtDeparture"]);
                newTour.ReturnDate = Convert.ToDateTime(Request["txtReturn"]);
                newTour.Dues = Convert.ToInt32(Request["txtDues"]);

                BAL.TourBO.Save(newTour);

                return Redirect("~/Home/AdminTours");
            }
            else
                return Redirect("~/Home/AdminLogin");

        }
        public ActionResult AdminLogout()
        {
            if (Security.AdminSessionManager.IsValidAdmin)
            {
                Security.AdminSessionManager.ClearSession();
                return Redirect("~/Home/Index");
            }
            else
                return Redirect("~/Home/AdminLogin");
        }
        public ActionResult SignUp()
        {
            Security.SessionManager.ClearSession();
            UsersDTO newUser = new UsersDTO();
            newUser.UserName = Request["txtLogin"];
            newUser.Password = Request["txtPassword"];
            newUser.Email = Convert.ToString(Request["txtEmail"]);
            newUser.PhoneNumber = Request["txtPhoneNumber"];
            BAL.UsersBO.Save(newUser);
            return Redirect("~/Home/Show");
        }
    }
}