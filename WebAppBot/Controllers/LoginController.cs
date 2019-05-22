using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBot.Models;

namespace WebAppBot.Controllers
{
    public class LoginController : Controller
    {
        private readonly IBusinessClass _businessClass;
        public LoginController(IBusinessClass businessClass)
        {
            _businessClass = businessClass;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginAction(User user) {
            if (ModelState.IsValid) {
                if (user.UserName == "abc") {
                    _businessClass.BusinessMethod();
                    return View("Close", user);
                }
            }
            return View("index");
        }

    }
}