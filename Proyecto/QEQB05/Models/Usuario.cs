using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QEQB05.Models
{
    public class Usuario
    {
        private int _ID;
        private string _Nombre;
        private string _password;
        private string _mail;
        private bool _Admin;
        private int _puntos;
        private string _Pregunta;
        private string _respuesta;

        public Usuario(string Nombre, string password, string mail, bool Admin, int puntos,int id,string preg,string resp)
        {
            _Nombre = Nombre;
            _password = password;
            _mail = mail;
            _Admin = Admin;
            _puntos = puntos;
            _ID = id;
            _Pregunta = preg;
            _respuesta = resp;
        }
        public Usuario()
        {
            _Nombre = null;
            _password = null;
            _mail = null;
            _Admin = false;
            _puntos = 0;
            _ID = -1;
            _Pregunta = null;
            _respuesta = null;
        }

        public string Nombre {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }
        [Required(ErrorMessage = "Ingrese su contraseña para poder continuar")]
        public string Password {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }
        [Required(ErrorMessage = "Ingrese su mail para poder continuar")]
        public string Mail {
            get
            {
                return _mail;
            }

            set
            {
                _mail = value;
            }
        }
        public bool Admin {
            get
            {
                return _Admin;
            }

            set
            {
                _Admin = value;
            }
        }
        public int Puntos {
            get
            {
                return _puntos;
            }

            set
            {
                _puntos = value;
            }
        }
        public int ID {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }
        public string Pregunta
        {
            get
            {
                return _Pregunta;
            }

            set
            {
                _Pregunta = value;
            }
        }
        public string respuesta
        {
            get
            {
                return _respuesta;
            }

            set
            {
                _respuesta = value;
            }
        }
    }
}