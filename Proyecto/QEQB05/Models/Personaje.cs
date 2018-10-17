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
        [Required(ErrorMessage = "Este campo es obligatorio")]
        CategoríaP _Categoría;

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

        public CategoríaP Categoría
        {
            get
            {
                return _Categoría;
            }

            set
            {
                _Categoría = value;
            }
        }

        public Personaje()
        {
            _Id = -1;
            _Nombre = null;
            _Foto = null;
            _Categoría = null;
        }

        public Personaje(int id, string nom, string url, CategoríaP cat)
        {
            _Id = id;
            _Nombre = nom;
            _Foto = url;
            _Categoría = cat;
        }
    }
}