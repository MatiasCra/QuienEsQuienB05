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
                if (usu.Nombre != null)
                {
                    Session["Nombre"] = usu.Nombre;
                    if (usu.Admin==true)
                    {
                        Session["Admin"] = true;
                    }
                    Session["UsuarioActivo"] = usu;
                    if (usu.Admin == false)
                    {
                        Session["Admin"] = false;
                        return View("Index");
                    }
                    else
                    {
                        return View("../BackOffice/Index");
                    }
                }
                else
                {
                    ViewBag.Error = "Contraseña o usuario incorrectos";
                    return View("Login");
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

        public ActionResult OlvidoContraseña()
        {
            return View();
        }

        public ActionResult RegistrarUsuario(Usuario y)
        {
            if (!ModelState.IsValid)
            {
                return View("Registro", y);
            }
            else
            {
                y.Admin = false;
                BD.RegistrarUsuario(y);
                Session["UsuarioActivo"] = y;
                Session["Admin"] = y.Admin;
                Session["Nombre"] = y.Nombre;
                return View("Index");
                }
            }
        

        public ActionResult Registro()
        {
            return View();
        }
    }
}