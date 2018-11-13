using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QEQB05.Models
{
    public class Pregunta
    {
        private int _IdPreg;
       
        private string _TextoPreg;

        public Pregunta(int IdPregu, string TextoPregu)
        {
            _IdPreg = IdPregu;
            _TextoPreg = TextoPregu;
        }
        public Pregunta()
        {
            _IdPreg = 0;
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