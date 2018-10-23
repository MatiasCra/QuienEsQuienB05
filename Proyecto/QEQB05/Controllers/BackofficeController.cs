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
            List<string> ListaUrlDataImagenes = new List<string>();
            string AuxFoto;
            List<Personaje> AuxListaPersonajes = new List<Personaje>();
            AuxListaPersonajes = BD.ListarPersonajes();
            ViewBag.ListaPersonajes = AuxListaPersonajes;
            foreach (Personaje p in AuxListaPersonajes)
            {
                AuxFoto = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(p.Foto));
                ListaUrlDataImagenes.Add(AuxFoto);
            }
            ViewBag.ListaUrlDataImagenes = ListaUrlDataImagenes;
            ViewBag.Contador = 0;
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
                String AuxFoto;
                Personaje P = BD.GetPersonaje(Id);
                AuxFoto = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(P.Foto));
                ViewBag.AuxFoto = AuxFoto;
                if (Accion == "Eliminar")
                {
                    return View("ConfirmarEliminarPersonaje", P);
                }
                else
                {
                    ViewBag.CatsP = P.Categorías;
                    return View("FormPersonaje", P);
                }
            }
        }

        public ActionResult OperacionesPersonaje(Personaje P, HttpPostedFileBase postedFile, int[] Box, string Accion, string AuxFoto)
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
                    path = path + fileName;
                }
                
                int I = BD.InsertPersonaje(P, path, Box);

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