using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QEQB05.Models
{
    public class Personaje
    {
        int _Id;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        string _Nombre;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        string _Foto;

        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public string Foto
        {
            get
            {
                return _Foto;
            }

            set
            {
                _Foto = value;
            }
        }

        public Personaje()
        {
            _Id = -1;
            _Nombre = null;
            _Foto = null;
        }

        public Personaje(int id, string nom, string url)
        {
            _Id = id;
            _Nombre = nom;
            _Foto = url;
        }
    }
}