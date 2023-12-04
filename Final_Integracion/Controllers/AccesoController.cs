using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Final_Integracion.Models; //Uso del modelo Usuario del proyecto
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;

namespace Final_Integracion.Controllers
{
    public class AccesoController : Controller
    {
        //En el Data Source le cambian el nombre del servidor dependiendo del nombre del suyo
        static string cadena = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true"; 
        

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registro;
            string mensaje;

            if (oUsuario.Clave == oUsuario.ConfirmarClave)
            {
                //Actualizamos la contraseña encriprada con el metodo ConvertirSha256
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            //Para conectar con nuestra base de datos la parte del registro

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.Parameters.Add("Registro", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registro = Convert.ToBoolean(cmd.Parameters["Registro"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registro)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }

        }


        //Vamos hacer ahora el de validar el usuario
        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {

            oUsuario.Clave = ConvertirSha256(oUsuario.Clave);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.IdUsuario= Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

            if(oUsuario.IdUsuario != 0)
            {
                Session["usuario"] = oUsuario;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewData["Mensaje"] = "usuario no encontrado";
                return View();
            }

        }

        public static string ConvertirSha256(string texto)
        {
            //Usaremos un metodo para usar el convertir la clave encriptada en SHA256 a un string
            //Using System.Text;
            //Usar la referencia de "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}