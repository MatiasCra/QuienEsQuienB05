using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QEQB05.Models;
using System.Drawing;

namespace QEQB05.Controllers
{
    public class BackofficeController : Controller
    {
        // GET: Backoffice
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModificarAdmin(int[] Box)
        {
            if (Box != null)
            {
                foreach (int i in Box)
                {
                    BD.AgregarAdmins(i);
                }
            }
            return RedirectToAction("Index", "Backoffice");
        }
        public ActionResult HacerAdmin()
        {
            ViewBag.Users = BD.TraerUsuarios();
            return View();
        }
        public ActionResult ABMPersonajes()
        {
            ViewBag.ListaPersonajes = BD.ListarPersonajes();
            return View();
        }

        public ActionResult EdicionPersonajes(int Id, string Accion)
        {
            ViewBag.Categorias = BD.ListarTodasCategoriasP();
            ViewBag.Accion = Accion;
            if (Accion == "Insertar")
            {
                return View("FormPersonaje");
            }
            else
            {
                Personaje P = BD.GetPersonaje(Id);
                MemoryStream ms = new MemoryStream(P.Foto, 0, P.Foto.Length);
                Image im = Image.FromStream(ms);
                ViewBag.AuxFoto = im;
                if (Accion == "Eliminar")
                {
                    return View("ConfirmarEliminarPersonaje", P);
                }
                else
                {
                    return View("FormPersonaje", P);
                }
            }
        }

        public ActionResult OperacionesPersonaje(Personaje P, HttpPostedFileBase postedFile, string Accion, string AuxFoto)
        {
            string path = Server.MapPath("~/Content/");

            if (!ModelState.IsValid)
            {
                ViewBag.Accion = Accion;
                return View("FormPersonaje", P);
            }
            
            if (Accion == "Insertar")
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(path + fileName);
                }
                int I = BD.InsertPersonaje(P, postedFile.FileName);

                if (I != null)
                {     
                    return View("ExitoOp");

                }
                else
                {
                    return View("ErrorOp");
                }
            }
            if (Accion == "Ver")
            {
                ViewBag.ListaPersonajes = BD.ListarPersonajes();
                return View("ABMPersonajes");
            }
            if (Accion == "Modificar")
            {
                bool M = BD.UpdatePersonaje(P);
                if (M == true)
                {
                    return View("ExitoOp");
                }
                else
                {
                    return View("ErrorOp");
                }
            }
            if (Accion == "Eliminar")
            {
                bool E = BD.DeletePersonaje(P.Id);
                if (E == true)
                {
                    return View("ExitoOp");
                }
                else
                {
                    return View("ErrorOp");
                }
            }
            return View("ErrorOp");
        }

    }
}