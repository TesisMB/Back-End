﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Back_End.Helpers
{
    public class Encrypt
    {
        //Esta funcion se encargara de la encriptacion de la Contrseña y devolverla hasheada para mayor seguridad.
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }



        
    }
}
