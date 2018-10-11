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
        [Required(ErrorMessage = "Ingrese su contraseña para poder continuar")]
        private string _password;
        [Required(ErrorMessage = "Ingrese su mail para poder continuar")]
        private string _mail;
        private bool _Admin;
        private int _puntos;

        public Usuario(string Nombre, string password, string mail, bool Admin, int puntos,int id)
        {
            _Nombre = Nombre;
            _password = password;
            _mail = mail;
            _Admin = Admin;
            _puntos = puntos;
            _ID = id;
        }
        public Usuario()
        {
            _Nombre = "";
            _password = "";
            _mail = "";
            _Admin = false;
            _puntos = 0;
            _ID = 0;
        }

        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Password { get => _password; set => _password = value; }
        public string Mail { get => _mail; set => _mail = value; }
        public bool Admin { get => _Admin; set => _Admin = value; }
        public int Puntos { get => _puntos; set => _puntos = value; }
        public int ID { get => _ID; set => _ID = value; }
    }
}