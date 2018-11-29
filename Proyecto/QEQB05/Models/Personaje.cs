using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QEQB05.Models
{
    public class Personaje
    {
        private int _Id;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        private string _Nombre;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        private string _UrlDataFoto;
        List<CategoríaP> _Categorías;
        List<int> _Respuestas;

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

        public string UrlDataFoto
        {
            get
            {
                return _UrlDataFoto;
            }

            set
            {
                _UrlDataFoto = value;
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

        public List<int> Respuestas
        {
            get
            {
                return _Respuestas;
            }

            set
            {
                _Respuestas = value;
            }
        }

        public Personaje()
        {
            _Id = -1;
            _Nombre = null;
            _UrlDataFoto = null;
            _Categorías = null;
        }

        public Personaje(int id, string nom, string foto, List<CategoríaP> cat)
        {
            _Id = id;
            _Nombre = nom;
            _UrlDataFoto = foto;
            _Categorías = cat;
        }
        public Personaje(int id, string nom, string foto, List<CategoríaP> cat, List<int> resp)
        {
            _Id = id;
            _Nombre = nom;
            _UrlDataFoto = foto;
            _Categorías = cat;
            _Respuestas = resp;
        }
    }
}