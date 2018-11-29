using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QEQB05.Models;

namespace QEQB05.Models
{
    public static class Partida
    {
        public static int IdCategoria;

        public static List<Personaje> Elegidos;

        public static List<Personaje> Todos;

        public static double puntaje;

        public static Personaje Seleccionado;

        public static List<Pregunta> Preguntas;

        public static void ReiniciarPartida()
        {
            IdCategoria = -1;
            Elegidos = null;
            Todos = null;
            puntaje = 1000000;
            Seleccionado = null;
            Preguntas = null;
        }
    }
}