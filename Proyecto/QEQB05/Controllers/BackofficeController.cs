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
        public int ValidarUsuario()
        {
            int val = 0;
            if ((Usuario)Session["UsuarioActivo"] == null)
            {
                val =1;
            }
            if ((Usuario)Session["UsuarioActivo"] != null && (bool)Session["Admin"] != true)
            {
                val=2;
            }
            return val;
        }
        public ActionResult Index()
        {
            int v = ValidarUsuario();
            if(v == 0)
            {
                return View();
            }
            if(v == 1)
            {
                return View("../Home/Index");
            }
            if(v == 2)
            {
                return View("../Home/Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ModificarAdmin(int[] Box)
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
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
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            ViewBag.Users = BD.TraerUsuarios();
            return View();
        }
        public ActionResult ABMPersonajes()
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            List<Personaje> AuxListaPersonajes = new List<Personaje>();
            AuxListaPersonajes = BD.ListarPersonajes();
            ViewBag.ListaPersonajes = AuxListaPersonajes;
            return View();
        }

        public ActionResult EdicionPersonajes(int Id, string Accion)
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
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
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
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

        public ActionResult ABMCatPersonajes()
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            List<CategoríaP> AuxListaCat = new List<CategoríaP>();
            AuxListaCat = BD.ListarCategoriasPersonajes();
            ViewBag.ListaCatPers= AuxListaCat;
            return View();
        }
        public ActionResult ABMPreguntas()
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }

            ViewBag.ListaPreguntas = BD.ListarPreguntas();
            return View();
        }
        public ActionResult EdicionPreguntas(int Id, string Accion)
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            
            ViewBag.Accion = Accion;
            if (Accion == "Insertar")
            {
                return View("FormPregunta");
            }
            else
            {
                Pregunta Pre= BD.GetPregunta(Id);
                if (Accion == "Eliminar")
                {
                    return View("ConfirmarEliminarPreguntas", Pre);
                }
                else
                {
                    return View("FormPregunta", Pre);
                }
            }
        }
        public ActionResult OperacionesPregunta (Pregunta P, string Accion)
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            if (!ModelState.IsValidField("TextoPreg"))
            {
                ViewBag.Accion = Accion;
                return View("FormPregunta", P);
            }

            if (Accion == "Insertar")
            {

                bool E = BD.InsertPregunta(P.TextoPreg);
                if (E == true)
                {
                    ViewBag.Estado = "Exitosa";
                    return View("ResultadoOp");
                }
                else
                {
                    ViewBag.Estado = "Erronea";
                    return View("ResultadoOp");
                }
            }
            
            if (Accion == "Modificar")
            {
                bool E = BD.ModificarPregunta(P);
                if (E == true)
                {
                    ViewBag.Estado = "Exitosa";
                    return View("ResultadoOp");
                }
                else
                {
                    ViewBag.Estado = "Erronea";
                    return View("ResultadoOp");
                }
               
                
            }
            if (Accion == "Eliminar")
            {
                
                bool E = BD.EliminarPregunta(P.IdPreg);
                if (E == true)
                {
                    ViewBag.Estado = "Exitosa";
                    return View("ResultadoOp");
                }
                else
                {
                    ViewBag.Estado = "Erronea";
                    return View("ResultadoOp");
                }
            }
            ViewBag.Estado = "Erronea";
            return View("ResultadoOp");
        }

        public ActionResult CargarRespuestas()
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            List<Pregunta> preguntas = BD.ListarPreguntas();
            ViewBag.ListaPreg = preguntas;
            return View();
        }

        public ActionResult FormRespuestas(string Texto, int Id)
        {
            int v = ValidarUsuario();
            if (v == 1)
            {
                return View("../Home/Index");
            }
            if (v == 2)
            {
                return View("../Home/Login");
            }
            Pregunta Preg = new Pregunta(Id, Texto);
            List<Personaje> ListaTodosPers = BD.ListarPersonajes();
            ViewBag.ListaTodosPers = ListaTodosPers;
            List<Personaje> ListaPersPreg = BD.ListarPersonajesXRespuesta(Id);
            ViewBag.ListaPersPreg = ListaPersPreg;
            return View("FormRespuestas", Preg);
        }

    }
}