using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QEQB05.Models;

namespace QEQB05.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CargarDatos(Usuario x)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", x);
            }
            else
            {
                Usuario usu = BD.VerificarLogin(x);
                Session["UsuarioActivo"] = usu;
                if (usu.Admin == false)
                {
                    return View("Bienvenido");
                }
                else
                {
                    return View("MenuBackoffice");
                }
            }
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