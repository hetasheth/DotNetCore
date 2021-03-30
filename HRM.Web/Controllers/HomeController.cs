using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using HRM.Web.Models;
using Microsoft.AspNetCore.Http;

namespace HRM.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginUser(LoginVM loginVM)
        {
            if(loginVM.EmailAddress=="admin@gmail.com" && loginVM.Password=="12345678")
            {
                HttpContext.Session.SetString("UserId", loginVM.EmailAddress);
                
                return RedirectToAction("Index", "Employee");
            }
            return View("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();            
            return RedirectToAction("Login");
        }

        public IActionResult Dashboard()
        {
            ViewBag.username = HttpContext.Session.GetString("UserId");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Unauthorize()
        {
            return View();
        }
    }
}
