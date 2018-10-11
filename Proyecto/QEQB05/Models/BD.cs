using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace QEQB05.Models
{
    public class BD
    {
        private static SqlConnection Conectar()
        {
            SqlConnection Conexion = new SqlConnection(connectionString);
            Conexion.Open();
            return Conexion;
        }

        public static string connectionString = "Server=10.128.8.16;Database=QEQB05;Trusted_Connection=True;";
        private static void Desconectar(SqlConnection Conexion)
        {
            Conexion.Close();
        }

        public static List<Personaje> ListarPersonajes()
        {
            List<Personaje> AuxLista = new List<Personaje>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_ListarTodosLosPersonajes";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int Id = Convert.ToInt32(Lector["IdPersonaje"]);
                string Nombre = Lector["Nombre"].ToString();
                string Foto = Lector["Foto"].ToString();
                Personaje P = new Personaje(Id, Nombre, Foto);
                AuxLista.Add(P);
            }
            Desconectar(Conexion);
            return AuxLista;
        }

        public static Personaje GetPersonaje(int Id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_Traer1Personaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int id = Convert.ToInt32(Lector["IdPersonaje"]);
            string Nombre = Lector["Nombre"].ToString();
            string Foto = Lector["Foto"].ToString(); ;
            Personaje P = new Personaje(id, Nombre, Foto);
            Desconectar(Conexion);
            return P;
        }

        public static bool InsertPersonaje(Personaje P)
        {
            bool val = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeAlta";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            Consulta.Parameters.AddWithValue("@pApellido", P.Foto);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            return val;
        }

        public static bool UpdatePersonaje(Personaje P)
        {
            bool val = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeModif";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", P.Id);
            Consulta.Parameters.AddWithValue("@pFoto", P.Foto);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            return val;
        }

        public static bool DeletePersonaje(int Id)
        {
            bool val = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeBaja";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", Id);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            return val;
        }

        public static List<CategoríaP> ListarCategoriasP(int Id)
        {
            List<CategoríaP> AuxLista = new List<CategoríaP>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerCategoríasDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int idc = Convert.ToInt32(Lector["IdCategoría"]);
                string Cat = (Lector["Categoría"]).ToString();
                CategoríaP C = new CategoríaP(idc, Cat);
                AuxLista.Add(C);
            }
            Desconectar(Conexion);
            return AuxLista;
        }

        public static List<CategoríaP> ListarTodasCategoriasP()
        {
            List<CategoríaP> AuxLista = new List<CategoríaP>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerTodasCategoríasP";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int idc = Convert.ToInt32(Lector["IdCategoría"]);
                string Cat = (Lector["Categoría"]).ToString();
                CategoríaP C = new CategoríaP(idc, Cat);
                AuxLista.Add(C);
            }
            Desconectar(Conexion);
            return AuxLista;
        }

        public static void UpdateCategoríasP(int IdP, List<CategoríaP> Nuevas)
        {
            bool val = false;
            List<CategoríaP> Anteriores = BD.ListarCategoriasP(IdP);
            List<int> Agregar = new List<int>();
            List<int> Eliminar = new List<int>();
            int x = 0;
            int y = 0;
            while(x  < Nuevas.Count)
            {
                while(y < Anteriores.Count && val == false)
                {
                    if(Nuevas[x].Id == Anteriores[y].Id)
                    {
                        val = true;
                    }
                    y++;
                }
                if(val == false)
                {
                    BD.InsertarCategoríaP(Nuevas[x], IdP);
                }
                x++;
            }
            x = 0;
            y = 0;
            while (x < Anteriores.Count)
            {
                while (y < Nuevas.Count && val == false)
                {
                    if (Anteriores[x].Id == Nuevas[y].Id)
                    {
                        val = true;
                    }
                    y++;
                }
                if (val == false)
                {
                    BD.BorrarCategoríaP(Anteriores[x], IdP);
                }
                x++;
            }
        }

        public static void InsertarCategoríaP(CategoríaP C, int Id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_InsertarCategoríaDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdP", Id);
            Consulta.Parameters.AddWithValue("@pIdC", C.Id);
            Consulta.ExecuteNonQuery();
            Desconectar(Conexion);
        }

        public static void BorrarCategoríaP(CategoríaP C, int Id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_InsertarCategoríaDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdP", Id);
            Consulta.Parameters.AddWithValue("@pIdC", C.Id);
            Consulta.ExecuteNonQuery();
            Desconectar(Conexion);
        }

        public static Usuario VerificarLogin(Usuario usu)
        {
            Usuario x = new Usuario();
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_Login";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pMail", usu.Mail);
            consulta.Parameters.AddWithValue("@Contraseña", usu.Password);
            SqlDataReader datareader = consulta.ExecuteReader();

            while (datareader.Read())
            {
                x.Nombre = datareader["Nombre"].ToString();
                x.Password = datareader["Contraseña"].ToString();
                x.Mail = datareader["mail"].ToString();
                x.Admin = Convert.ToBoolean(datareader["Admin"]);
                x.Puntos = Convert.ToInt32(datareader["PuntosAcumulados"]);
                x.ID = Convert.ToInt32(datareader["ID_Usuario"]);
            }
            Desconectar(Conexion);
            return x;
        }
    }
}