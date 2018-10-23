using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace QEQB05.Models
{
    public static class ConversionIMG
    {
        public static Byte[] ConvertirAByteArray (string pathArchivo)
        {
            FileStream fs = new FileStream(pathArchivo, FileMode.Open);
            FileInfo fi = new FileInfo(pathArchivo);
            long temp = fi.Length;
            int lung = Convert.ToInt32(temp);
            byte[] picture = new byte[lung];
            fs.Read(picture, 0, lung);
            fs.Close();
            return picture;
        }
        public static string ConvertirAURLData (byte[] img)
        {
            string aux;
            aux = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(img));
            return aux;
        }
    }
}