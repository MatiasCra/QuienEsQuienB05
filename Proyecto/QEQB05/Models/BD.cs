using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;

namespace QEQB05.Models
{
    public class BD
    {
        public static string connectionString = "Server=10.128.8.16;Database=QEQB05;User Id=QEQB05; Password=QEQB05;";
        public static string archivo = "D:/Tareas 4IB/Programación/QEQB05/QuienEsQuienB05/Proyecto/QEQB05/Content/";

        private static SqlConnection Conectar()
        {
            SqlConnection Conexion = new SqlConnection(connectionString);
            Conexion.Open();
            return Conexion;
        }

        private static void Desconectar(SqlConnection Conexion)
        {
            Conexion.Close();
        }
        public static void AgregarAdmins(int id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_HacerAdmin";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pId", id);
            consulta.ExecuteNonQuery();
            Desconectar(Conexion);
        }
        public static List<Usuario> TraerUsuarios()
        {
            List<Usuario> Usus = new List<Usuario>();
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_TraerTodosUsuarios";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader datareader = consulta.ExecuteReader();

            while (datareader.Read())
            {
                Usuario x = new Usuario();
                x.Nombre = datareader["Nombre"].ToString();
                x.Password = datareader["Contraseña"].ToString();
                x.Mail = datareader["mail"].ToString();
                x.Admin = Convert.ToBoolean(datareader["Administrador"]);
                x.Puntos = Convert.ToInt32(datareader["PuntosAcumulados"]);
                x.ID = Convert.ToInt32(datareader["ID_Usuario"]);
                Usus.Add(x);
            }
            Desconectar(Conexion);
            return Usus;
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
                byte[] Foto = (byte[])Lector["Foto"];
                CategoríaP C = BD.TraerCategoriaP(Id);
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
            Consulta.Parameters.AddWithValue("@pIdPers", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int id = Convert.ToInt32(Lector["IdPersonaje"]);
            string Nombre = Lector["Nombre"].ToString();
            byte[] Foto = (byte[])Lector["Foto"];
            Desconectar(Conexion);
            CategoríaP C = BD.TraerCategoriaP(Id);
            Personaje P = new Personaje(id, Nombre, Foto);
            return P;
        }

        public static CategoríaP TraerCategoriaP(int Id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerCategoríasDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int id = Convert.ToInt32(Lector["IdCategoría"]);
            string cat = (Lector["Categoría"]).ToString();
            CategoríaP C = new CategoríaP(id, cat);
            Desconectar(Conexion);
            return C;
        }

        public static void InsertarCategoríaP(int IdC, int IdP)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_InsertarCategoríaDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdP", IdP);
            Consulta.Parameters.AddWithValue("@pIdC", IdC);
            Consulta.ExecuteNonQuery();
            Desconectar(Conexion);
        }

        public static void BorrarCategoríaP(int IdC, int IdP)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_DeleteCategoríaDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdP", IdP);
            Consulta.Parameters.AddWithValue("@pIdC", IdC);
            Consulta.ExecuteNonQuery();
            Desconectar(Conexion);
        }



        public static int TraerIdP(string nom)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerIdPersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pNombre", nom);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int Id = Convert.ToInt32(Lector["IdPersonaje"]);
            Desconectar(Conexion);
            return Id;
        }

        public static bool UpdatePersonaje(Personaje P)
        {
            bool val = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeModif";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", P.Id);
            Consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            Consulta.Parameters.AddWithValue("@pFoto", P.Foto);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            CategoríaP Anterior = BD.TraerCategoriaP(P.Id);
            
            return val;
        }

        public static bool DeletePersonaje(int Id)
        {
            bool val = false;
            BD.BorrarCategoríaP(BD.TraerCategoriaP(Id).Id, Id);
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
                int idc = Convert.ToInt32(Lector["IdCategoriaP"]);
                string Cat = (Lector["Categoría"]).ToString();
                CategoríaP C = new CategoríaP(idc, Cat);
                AuxLista.Add(C);
            }
            Desconectar(Conexion);
            return AuxLista;
        }

        public static Usuario VerificarLogin(Usuario usu)
        {
            Usuario x = new Usuario();
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_Login";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pMail", usu.Mail);
            consulta.Parameters.AddWithValue("@pContraseña", usu.Password);
            SqlDataReader datareader = consulta.ExecuteReader();

            while (datareader.Read())
            {
                x.Nombre = datareader["Nombre"].ToString();
                x.Password = datareader["Contraseña"].ToString();
                x.Mail = datareader["mail"].ToString();
                x.Admin = Convert.ToBoolean(datareader["Administrador"]);
                x.Puntos = Convert.ToInt32(datareader["PuntosAcumulados"]);
                x.ID = Convert.ToInt32(datareader["ID_Usuario"]);
            }
            Desconectar(Conexion);
            return x;
        }
        public static bool RegistrarUsuario(Usuario U)
        {
            bool val = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_RegistrarNuevoUsuario";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pNombre", U.Nombre);
            Consulta.Parameters.AddWithValue("@pMail", U.Mail);
            Consulta.Parameters.AddWithValue("@pContraseña", U.Password);
            Consulta.Parameters.AddWithValue("@pAdmin", U.Admin);
            Consulta.Parameters.AddWithValue("@pPuntosAcum", 0);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            return val;
        }

        public static bool InsertPersonaje(Personaje P, string FileName)
        {
            bool val;
            val = false;
            archivo = archivo + FileName;
            FileStream fs = new FileStream(archivo, FileMode.Open);
            FileInfo fi = new FileInfo(archivo);
            
            long temp = fi.Length;
            int lung = Convert.ToInt32(temp);
            byte[] picture = new byte[lung];
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeAlta";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            Consulta.Parameters.AddWithValue("@pFoto", picture);
            BD.InsertarCategoríaP(P.IdCategoría, P.Id);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            P.Id = BD.TraerIdP(P.Nombre);
            BD.InsertarCategoríaP(P.IdCategoría, P.Id);
            return val;
        }
    }
}