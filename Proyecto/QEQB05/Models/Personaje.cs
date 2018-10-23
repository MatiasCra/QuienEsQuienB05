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
        byte[] _Foto;
        List<CategoríaP> _Categorías; 
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

        public byte[] Foto
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

        public List<CategoríaP> Categorías
        {
            get
            {
                return _Categorías;
            }

            set
            {
                Categorías = value;
            }
        }

        public Personaje()
        {
            _Id = -1;
            _Nombre = null;
            _Foto = null;
            _Categorías = null;
        }

        public Personaje(int id, string nom, byte[] foto, List<CategoríaP> cat)
        {
            _Id = id;
            _Nombre = nom;
            _Foto = foto;
            _Categorías = cat;
        }
    }
}