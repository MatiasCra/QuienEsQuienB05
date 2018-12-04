using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace QEQB05.Models
{
    public class BD
    {
        private static string connectionString = "Server=10.128.8.16;Database=QEQB05;User Id=QEQB05; Password=QEQB05;"; 
        //private static string connectionString = "Server=DESKTOP-FJ4BM8M\\SQLEXPRESS;Database=QEQB05;Trusted_Connection=True;";

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
                Usuario x = new Usuario
                {
                    Nombre = datareader["Nombre"].ToString(),
                    Password = datareader["Contraseña"].ToString(),
                    Mail = datareader["mail"].ToString(),
                    Admin = Convert.ToBoolean(datareader["Administrador"]),
                    Puntos = Convert.ToInt32(datareader["PuntosAcumulados"]),
                    ID = Convert.ToInt32(datareader["ID_Usuario"]),
                    Pregunta = datareader["PreguntaDeSeguridad"].ToString(),
                    respuesta = datareader["RespuestaPregSeg"].ToString()
                };
                Usus.Add(x);
            }
            Desconectar(Conexion);
            return Usus;
        }
        public static List<Usuario> TraerTodosUsuarios()
        {
            List<Usuario> Usus = new List<Usuario>();
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_TraerUsuarios";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader datareader = consulta.ExecuteReader();

            while (datareader.Read())
            {
                Usuario x = new Usuario
                {
                    Nombre = datareader["Nombre"].ToString(),
                    Password = datareader["Contraseña"].ToString(),
                    Mail = datareader["mail"].ToString(),
                    Admin = Convert.ToBoolean(datareader["Administrador"]),
                    Puntos = Convert.ToInt32(datareader["PuntosAcumulados"]),
                    ID = Convert.ToInt32(datareader["ID_Usuario"]),
                    Pregunta = datareader["PreguntaDeSeguridad"].ToString(),
                    respuesta=datareader["RespuestaPregSeg"].ToString()
                };
                Usus.Add(x);
            }
            Desconectar(Conexion);
            return Usus;
        }
        public static List<Personaje> ListarPersonajes()
        {
            List<Personaje> AuxLista = new List<Personaje>();
            string Foto = null;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_ListarTodosLosPersonajes";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int Id = Convert.ToInt32(Lector["IdPersonaje"]);
                string Nombre = Lector["Nombre"].ToString();
                byte[] AuxFoto = (byte[])Lector["Foto"];
                Foto = ConversionIMG.ConvertirAURLData(AuxFoto);
                List<CategoríaP> cat = BD.TraerCategoriaP(Id);
                Personaje P = new Personaje(Id, Nombre, Foto, cat);
                AuxLista.Add(P);
            }
            Desconectar(Conexion);
            return AuxLista;
        }

        public static Personaje GetPersonaje(int Id)
        {
            string Foto = null;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_Traer1Personaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdPers", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int id = Convert.ToInt32(Lector["IdPersonaje"]);
            string Nombre = Lector["Nombre"].ToString();
            byte[] AuxFoto = (byte[])Lector["Foto"];
            Foto = ConversionIMG.ConvertirAURLData(AuxFoto);
            Desconectar(Conexion);
            List<CategoríaP> cat = BD.TraerCategoriaP(Id);
            Personaje P = new Personaje(id, Nombre, Foto, cat);
            return P;
        }

        public static CategoríaP GetCategoria(int Id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerCategoria";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int id = Convert.ToInt32(Lector["IdCategoriaP"]);
            string Nombre = Lector["Categoría"].ToString();
            Desconectar(Conexion);
            CategoríaP C = new CategoríaP(id, Nombre);
            return C;
        }

        public static bool InsertarCategoría(string Cat)
        {
            bool aux = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_CategoriasPersAlta";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pCat", Cat);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                aux = true;
            }
            Desconectar(Conexion);
            return aux;
        }

        public static bool UpdateCategoría(int id, string Cat)
        {
            bool aux = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_CategoriasPersModif";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pID", id);
            Consulta.Parameters.AddWithValue("@pCat", Cat);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                aux = true;
            }
            Desconectar(Conexion);
            return aux;
        }

        public static bool DeleteCategoría(int Id)
        {
            bool val = false;
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_CategoriasPersBaja";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pID", Id);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            return val;
        }

        public static List<CategoríaP> TraerCategoriaP(int Id)
        {
            List<CategoríaP> Aux = new List<CategoríaP>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerCategoríasDePersonaje";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int id = Convert.ToInt32(Lector["IdCategoría"]);
                string cat = (Lector["Categoría"]).ToString();
                CategoríaP C = new CategoríaP(id, cat);
                Aux.Add(C);
            }
            Desconectar(Conexion);
            return Aux;
        }


        public static void InsertarCategoríaP(int IdC, int? IdP)
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

        public static bool UpdatePersonaje(Personaje P, string pathArchivo, int[] Box)
        {
            bool val = false;
            byte[] picture = null;
            if (pathArchivo != null)
            {
                picture = ConversionIMG.ConvertirAByteArray(pathArchivo);
            }
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeModif";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pId", P.Id);
            Consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            if (pathArchivo != null)
            {
                Consulta.Parameters.AddWithValue("@pFoto", picture);
            }
            else
            {
                Consulta.CommandText = "sp_PersonajeModifSinImg";
            }
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            List<CategoríaP> Anterior = BD.TraerCategoriaP(P.Id);
            bool v;
            if (Box != null)
            {
                foreach (CategoríaP C in Anterior)
                {
                    v = true;
                    foreach (int id in Box)
                    {
                        if (C.Id == id)
                        {
                            v = false;
                        }
                    }
                    if (v == true)
                    {
                        BD.BorrarCategoríaP(C.Id, P.Id);
                    }
                }
                foreach (int id in Box)
                {
                    v = true;
                    foreach (CategoríaP C in Anterior)
                    {
                        if (C.Id == id)
                        {
                            v = false;
                        }
                    }
                    if (v == true)
                    {
                        BD.InsertarCategoríaP(id, P.Id);
                    }
                }
            }
            if (Box == null && Anterior != null)
            {
                foreach (CategoríaP C in Anterior)
                {
                    BD.BorrarCategoríaP(C.Id, P.Id);
                }
            }
            if (pathArchivo != null)
            {
                File.Delete(pathArchivo);
            }
            return val;
        }

        public static bool DeletePersonaje(int Id)
        {
            bool val = false;
            List<CategoríaP> categorías = BD.TraerCategoriaP(Id);
            if (categorías != null)
            {
                foreach (CategoríaP c in categorías)
                {
                    BD.BorrarCategoríaP(c.Id, Id);
                }
            }
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
            Consulta.CommandText = "sp_TraerTodasCategoríasPersonaje";
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

        public static bool ValidarMail(string Mail)
        {
            bool existe = false;
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_ValidarMail";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pMail", Mail);
            SqlDataReader datareader = consulta.ExecuteReader();
            datareader.Read();
            if(Mail == (datareader["mail"].ToString()))
            {
                existe = true;
            }
            return existe;
        }

        public static bool ValidarNombre(string Nombre)
        {
            bool existe = false;
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_ValidarNom";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@pNom", Nombre);
            SqlDataReader datareader = consulta.ExecuteReader();
            datareader.Read();
            if (Nombre == (datareader["Nombre"].ToString()))
            {
                existe = true;
            }
            return existe;
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
            Consulta.Parameters.AddWithValue("@PreguntaDeSeguridad", U.Pregunta);
            Consulta.Parameters.AddWithValue("@respuesta", U.respuesta);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            return val;
        }

        public static int? InsertPersonaje(Personaje P, string pathArchivo, int[] Box)
        {
            byte[] picture = null;
            if (pathArchivo != null)
            {
                picture = ConversionIMG.ConvertirAByteArray(pathArchivo);
            }
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PersonajeAlta";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pNombre", P.Nombre);
            Consulta.Parameters.AddWithValue("@pFoto", picture);
            Consulta.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
            int i = Consulta.ExecuteNonQuery();
            int? idp = Convert.ToInt32(Consulta.Parameters["@id"].Value);
            Desconectar(Conexion);
            if (Box != null && idp != null)
            {
                foreach (int b in Box)
                {
                    BD.InsertarCategoríaP(b, idp);
                }
            }
            File.Delete(pathArchivo);
            return idp;
        }

        public static List<CategoríaP> ListarCategoriasPersonajes()
        {
            List<CategoríaP> AuxLista = new List<CategoríaP>();
   
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_TraerTodasCategoríasPersonaje";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader datareader = consulta.ExecuteReader();

            while (datareader.Read())
            {
                CategoríaP AuxC = new CategoríaP();
                AuxC.Id = Convert.ToInt32(datareader["IdCategoriaP"]);
                AuxC.Nombre = datareader["Categoría"].ToString();
                AuxLista.Add(AuxC);
            }
            Desconectar(Conexion);
            return AuxLista;
        }
        public static bool InsertPregunta(string pregunta)
        {
            bool val = new bool();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PreguntasAlta";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pTexto", pregunta);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            return val;
        }
        public static bool ModificarPregunta(Pregunta pregunta)
        {
            bool val = new bool();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PreguntasModif";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pTexto", pregunta.TextoPreg);
            Consulta.Parameters.AddWithValue("@pIdPreg", pregunta.IdPreg);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            return val;
        }
        public static bool EliminarPregunta(int idPreg)
        {
            bool val=new bool();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_PreguntasBaja";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdPreg", idPreg);
            int i = Consulta.ExecuteNonQuery();
            if (i > 0)
            {
                val = true;
            }
            Desconectar(Conexion);
            return val;
        }
        public static List<Pregunta> ListarPreguntas()
        {
            List<Pregunta> preguntas = new List<Pregunta>();
            SqlConnection Conexion = Conectar();
            SqlCommand consulta = Conexion.CreateCommand();
            consulta.CommandText = "sp_ListarTodasLasPreguntas";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader datareader = consulta.ExecuteReader();
            while (datareader.Read())
            {
                Pregunta AuxP = new Pregunta();
                AuxP.TextoPreg = datareader["Texto"].ToString();
                AuxP.IdPreg = Convert.ToInt32(datareader["IDPregunta"]);
                preguntas.Add(AuxP);
            }
            Desconectar(Conexion);
            return preguntas;
        }

        public static List<Personaje> ListarPersonajesXRespuesta(int IdPreg)
        {
            string Foto = null;
            List<Personaje> Lista = new List<Personaje>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_ListarPersonajesXRespuesta";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdPreg", IdPreg);
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int Id = Convert.ToInt32(Lector["IdPersonaje"]);
                string Nombre = Lector["Nombre"].ToString();
                byte[] AuxFoto = (byte[])Lector["Foto"];
                Foto = ConversionIMG.ConvertirAURLData(AuxFoto);
                List<CategoríaP> cat = BD.TraerCategoriaP(Id);
                Personaje P = new Personaje(Id, Nombre, Foto, cat);
                Lista.Add(P);
            }
            Desconectar(Conexion);
            return Lista;
        }

        public static bool UpdatePersonajesXPregunta(int IdPreg, int[] Box)
        {
            bool val;
            bool operar;
            int cont = 0;
            List<Personaje> Anteriores = BD.ListarPersonajesXRespuesta(IdPreg);
            if (Box == null)
            {
                foreach (Personaje P in Anteriores)
                {
                    SqlConnection Conexion = Conectar();
                    SqlCommand Consulta = Conexion.CreateCommand();
                    Consulta.CommandText = "sp_DeletePersonajesXPreguntas";
                    Consulta.CommandType = System.Data.CommandType.StoredProcedure;
                    Consulta.Parameters.AddWithValue("@pIdPreg", IdPreg);
                    Consulta.Parameters.AddWithValue("@pIdPers", P.Id);
                    int v = Consulta.ExecuteNonQuery();
                    Desconectar(Conexion);
                    cont = cont + v;
                }
            }
            else
            {
                foreach (int I in Box)
                {
                    operar = true;
                    foreach (Personaje P in Anteriores)
                    {
                        if (I == P.Id)
                        {
                            operar = false;
                        }
                    }
                    if (operar == true)
                    {
                        SqlConnection Conexion = Conectar();
                        SqlCommand Consulta = Conexion.CreateCommand();
                        Consulta.CommandText = "sp_InsertarPersonajesXPreguntas";
                        Consulta.CommandType = System.Data.CommandType.StoredProcedure;
                        Consulta.Parameters.AddWithValue("@pIdPreg", IdPreg);
                        Consulta.Parameters.AddWithValue("@pIdPers", I);
                        int v = Consulta.ExecuteNonQuery();
                        Desconectar(Conexion);
                        cont = cont + v;
                    }
                }
                foreach (Personaje P in Anteriores)
                {
                    operar = true;
                    foreach (int I in Box)
                    {
                        if (P.Id == I)
                        {
                            operar = false;
                        }
                    }
                    if (operar == true)
                    {
                        SqlConnection Conexion = Conectar();
                        SqlCommand Consulta = Conexion.CreateCommand();
                        Consulta.CommandText = "sp_DeletePersonajesXPreguntas";
                        Consulta.CommandType = System.Data.CommandType.StoredProcedure;
                        Consulta.Parameters.AddWithValue("@pIdPreg", IdPreg);
                        Consulta.Parameters.AddWithValue("@pIdPers", P.Id);
                        int v = Consulta.ExecuteNonQuery();
                        Desconectar(Conexion);
                        cont = cont + v;
                    }
                }
            }
            if(cont == 0)
            {
                val = false;
            }
            else
            {
                val = true;
            }
            return val;
        }
           
        public static void CambiarContraseña(Usuario usu)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_OlvideContraseña";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@Mail", usu.Mail);
            Consulta.Parameters.AddWithValue("@Contraseña", usu.Password);
            Consulta.ExecuteNonQuery();
            Desconectar(Conexion);
        }
        public static Pregunta GetPregunta(int Id)
        {
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerPregunta1";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@idPreg", Id);
            SqlDataReader Lector = Consulta.ExecuteReader();
            Lector.Read();
            int id = Convert.ToInt32(Lector["IDPregunta"]);
            string texto = Lector["Texto"].ToString();
            Desconectar(Conexion);
            Pregunta P = new Pregunta(id, texto);
            return P;
        }

        public static List<Pregunta> ListarPreguntaXPersonajes(int IdPers)
        {
            List<Pregunta> Lista = new List<Pregunta>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_ListarPreguntaXPersonajes";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdPers", IdPers);
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int Id = Convert.ToInt32(Lector["IdPregunta"]);
                string Texto = Lector["Texto"].ToString();
                Pregunta Pr = new Pregunta(Id, Texto);
                Lista.Add(Pr);
            }
            Desconectar(Conexion);
            return Lista;
        }

        public static bool UpdatePreguntasXPersonaje(int IdPers, int[] Box)
        {
            bool val;
            bool operar;
            int cont = 0;
            List<Pregunta> Anteriores = BD.ListarRespuestaXPersonaje(IdPers);
            if (Box == null)
            {
                foreach (Pregunta P in Anteriores)
                {
                    SqlConnection Conexion = Conectar();
                    SqlCommand Consulta = Conexion.CreateCommand();
                    Consulta.CommandText = "sp_DeletePersonajesXPreguntas";
                    Consulta.CommandType = System.Data.CommandType.StoredProcedure;
                    Consulta.Parameters.AddWithValue("@pIdPreg", P.IdPreg);
                    Consulta.Parameters.AddWithValue("@pIdPers", IdPers);
                    int v = Consulta.ExecuteNonQuery();
                    Desconectar(Conexion);
                    cont = cont + v;
                }
            }
            else
            {
                foreach (int I in Box)
                {
                    operar = true;
                    foreach (Pregunta P in Anteriores)
                    {
                        if (I == P.IdPreg)
                        {
                            operar = false;
                        }
                    }
                    if (operar == true)
                    {
                        SqlConnection Conexion = Conectar();
                        SqlCommand Consulta = Conexion.CreateCommand();
                        Consulta.CommandText = "sp_InsertarPersonajesXPreguntas";
                        Consulta.CommandType = System.Data.CommandType.StoredProcedure;
                        Consulta.Parameters.AddWithValue("@pIdPreg", I);
                        Consulta.Parameters.AddWithValue("@pIdPers", IdPers);
                        int v = Consulta.ExecuteNonQuery();
                        Desconectar(Conexion);
                        cont = cont + v;
                    }
                }
                foreach (Pregunta P in Anteriores)
                {
                    operar = true;
                    foreach (int I in Box)
                    {
                        if (P.IdPreg == I)
                        {
                            operar = false;
                        }
                    }
                    if (operar == true)
                    {
                        SqlConnection Conexion = Conectar();
                        SqlCommand Consulta = Conexion.CreateCommand();
                        Consulta.CommandText = "sp_DeletePersonajesXPreguntas";
                        Consulta.CommandType = System.Data.CommandType.StoredProcedure;
                        Consulta.Parameters.AddWithValue("@pIdPreg", P.IdPreg);
                        Consulta.Parameters.AddWithValue("@pIdPers", IdPers);
                        int v = Consulta.ExecuteNonQuery();
                        Desconectar(Conexion);
                        cont = cont + v;
                    }
                }
            }
            if (cont == 0)
            {
                val = false;
            }
            else
            {
                val = true;
            }
            return val;
        }
        public static List<Pregunta> ListarRespuestaXPersonaje(int IdPers)
        {
            List<Pregunta> Lista = new List<Pregunta>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_ListarPreguntaXPersonajes";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdPers", IdPers);
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int Id = Convert.ToInt32(Lector["IDPregunta"]);
                string Texto = Lector["Texto"].ToString();
                Pregunta P = new Pregunta (Id, Texto);
                Lista.Add(P);
            }
            Desconectar(Conexion);
            return Lista;
        }

        public static List<int> ListarRespuestas (int IdP)
        {
            List<int> Lista = new List<int>();
            SqlConnection Conexion = Conectar();
            SqlCommand Consulta = Conexion.CreateCommand();
            Consulta.CommandText = "sp_TraerRespuestas";
            Consulta.CommandType = System.Data.CommandType.StoredProcedure;
            Consulta.Parameters.AddWithValue("@pIdPersonaje", IdP);
            SqlDataReader Lector = Consulta.ExecuteReader();
            while (Lector.Read())
            {
                int Id = Convert.ToInt32(Lector["IDPregunta"]);
                Lista.Add(Id);
            }
            Desconectar(Conexion);
            return Lista;
        }
    }
}