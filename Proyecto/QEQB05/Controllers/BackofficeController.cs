using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QEQB05.Models;

namespace QEQB05.Controllers
{
    public class BackofficeController : Controller
    {
        // GET: Backoffice
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ABMPersonajes()
        {
            ViewBag.ListaPersonajes = BD.ListarPersonajes();
            return View();
        }

        public ActionResult EdicionPersonajes(int Id, string Accion)
        {
            ViewBag.TodasCategorías = BD.ListarTodasCategoriasP();
            ViewBag.Accion = Accion;
            if (Accion == "Insertar")
            {
                return View("FormPersonaje");
            }
            else
            {
                Personaje P = BD.GetPersonaje(Id);
                ViewBag.CategoríasP = BD.ListarCategoriasP(Id);
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

        public ActionResult OperacionesPersonaje(Personaje P, string Accion)
        {
            if (Accion == "Insertar")
            {
                bool I = BD.InsertPersonaje(P);
                if (I == true)
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