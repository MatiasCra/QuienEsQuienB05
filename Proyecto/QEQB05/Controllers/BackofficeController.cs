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
            List<Personaje> AuxListaPersonajes = new List<Personaje>();
            AuxListaPersonajes = BD.ListarPersonajes();
            ViewBag.ListaPersonajes = AuxListaPersonajes;
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
                if (Accion == "Eliminar")
                {
                    return View("ConfirmarEliminarPersonaje", P);
                }
                else
                {
                    ViewBag.AuxFoto = P.UrlDataFoto;
                    ViewBag.CatsP = P.Categorías;
                    return View("FormPersonaje", P);
                }
            }
        }

        public ActionResult OperacionesPersonaje(Personaje P, HttpPostedFileBase postedFile, int[] Box, string Accion, string AuxFoto)
        {
            string path = null;
            string fileName = null;
            if (postedFile != null)
            {
                path = Server.MapPath("~/Content/");
                fileName = Path.GetFileName(postedFile.FileName);
                string filename = postedFile.FileName;
                postedFile.SaveAs(path + fileName);
                path = path + fileName;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Accion = Accion;
                return View("FormPersonaje", P);
            }
            
            if (Accion == "Insertar")
            {
                
                
                int? I = BD.InsertPersonaje(P, path, Box);

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
                
                bool M = BD.UpdatePersonaje(P, path, Box);
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