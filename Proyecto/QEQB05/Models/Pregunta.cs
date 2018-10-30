﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QEQB05.Models
{
    public class Pregunta
    {
        private int _IdPreg;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        private string _TextoPreg;

        public Pregunta(int IdPreg, string TextoPreg)
        {
            _IdPreg = IdPreg;
            _TextoPreg = TextoPreg;
        }
        public Pregunta()
        {
            _IdPreg = -1;
            _TextoPreg = null;
        }
        public int IdPreg {
            get
            {
                return _IdPreg;
            }

            set
            {
                _IdPreg = value;
            }
        }
        public string TextoPreg
        {
            get
            {
                return _TextoPreg;
            }

            set
            {
                _TextoPreg = value;
            }
        }
    }
}