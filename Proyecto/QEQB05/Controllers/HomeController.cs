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
        public ActionResult ComenzarJuego()
        {
            Partida.puntaje = 1000000;
            ViewBag.categorias = BD.ListarTodasCategoriasP();
            return View();
        }
        public ActionResult Jugar(string Accion)
        {
            ViewBag.puntaje = Partida.puntaje;
            if (Accion=="Comenzar")
            {
                
                int espacios = Partida.Todos.Count;
                Random rand = new Random((int)DateTime.Now.Ticks);
                int PersonajeSeleccionado = -1;
                PersonajeSeleccionado = rand.Next(0, espacios);
                Personaje Selected = new Personaje();
                Partida.Seleccionado = Partida.Todos[PersonajeSeleccionado];
            }
            List<Pregunta> ListaPregs = BD.ListarPreguntas();
            ViewBag.ListaPregs = ListaPregs;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Index");
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
        public ActionResult CargarDatos2(Usuario x)
        {
            ViewBag.mensaje = "Los datos ingresados son incorrectos";
            bool veracidad =new bool();
            veracidad = false;
            List<Usuario> u = BD.TraerTodosUsuarios();
            foreach(Usuario usu in u)
            {
                if(usu.Mail==x.Mail&&usu.Pregunta==x.Pregunta&&usu.respuesta==x.respuesta)
                {
                    veracidad = true;
                }
            }
            ViewBag.veracidad = veracidad;
            if (!ModelState.IsValid||veracidad==false)
            {
                return View("OlvidoContraseña", x);
            }
            else
            {
                BD.CambiarContraseña(x);
                Usuario usu = BD.VerificarLogin(x);
               
                    Session["Nombre"] = usu.Nombre;
                    if (usu.Admin == true)
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
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("AboutUs");
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
                bool m = BD.ValidarMail(y.Mail);
                bool n = BD.ValidarNombre(y.Nombre);
                if (m == true)
                {
                    ViewBag.Error = "Este mail ya está registrado";
                }
                if (n == true)
                {
                    ViewBag.Error2 = "Este nombre ya existe, elija otro";
                }
                if(n == true || m == true)
                {
                    return View("Registro");
                }
                y.Admin = false;
                BD.RegistrarUsuario(y);
                Session["UsuarioActivo"] = y;
                Session["Admin"] = y.Admin;
                Session["Nombre"] = y.Nombre;
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult MostrarTodosPersonajes(int IdC)
        {
            List<Personaje> personajes = BD.ListarPersonajes();
            List<Personaje> elegidos = new List<Personaje>();
            Partida.IdCategoria = IdC;
            if (IdC == -1)
            {
                Partida.Todos = personajes;
            }
            else
            {
                foreach (Personaje per in personajes)
                {
                    foreach (CategoríaP cat in per.Categorías)
                    {
                        if (IdC == cat.Id)
                        {
                            elegidos.Add(per);
                        }
                    }
                }
                Partida.Todos = elegidos;
            }
            Partida.Elegidos = Partida.Todos;
            ViewBag.ListaPersonajes = Partida.Todos;
            ViewBag.Accion = "Comenzar";
            return View("TodosPersonajes");
        }

        public ActionResult VerPersonajesRestantes()
        {
            Partida.puntaje = Partida.puntaje - 5000;
            ViewBag.ListaPersonajes = Partida.Elegidos;
            return View("TodosPersonajes");
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Instrucciones()
        {
            return View();
        }
    }
}