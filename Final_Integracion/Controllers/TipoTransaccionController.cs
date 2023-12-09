using Final_Integracion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

public class TipoTransaccionController : Controller
{
    static string cadena = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true";

    public ActionResult Index()
    {
        List<TipoTransaccion> tiposTransaccion = new List<TipoTransaccion>();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM TipoTransacciones";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoTransaccion tipoTransaccion = new TipoTransaccion();
                        tipoTransaccion.TipoTransaccionId = Convert.ToInt32(reader["TipoTransaccionId"]);
                        tipoTransaccion.Nombre = reader["Nombre"].ToString();

                        tiposTransaccion.Add(tipoTransaccion);
                    }
                }
            }
        }

        return View(tiposTransaccion);
    }

    public ActionResult Detalles(int id)
    {
        TipoTransaccion tipoTransaccion = ObtenerTipoTransaccionPorId(id);
        return View(tipoTransaccion);
    }

    public ActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Agregar(TipoTransaccion tipoTransaccion)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "INSERT INTO TipoTransacciones (Nombre) VALUES (@Nombre)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", tipoTransaccion.Nombre);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(tipoTransaccion);
    }

    public ActionResult Editar(int id)
    {
        TipoTransaccion tipoTransaccion = ObtenerTipoTransaccionPorId(id);
        return View(tipoTransaccion);
    }

    [HttpPost]
    public ActionResult Editar(TipoTransaccion tipoTransaccion)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "UPDATE TipoTransacciones SET Nombre = @Nombre WHERE TipoTransaccionId = @Id";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", tipoTransaccion.Nombre);
                    comando.Parameters.AddWithValue("@Id", tipoTransaccion.TipoTransaccionId);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(tipoTransaccion);
    }

    public ActionResult Eliminar(int id)
    {
        TipoTransaccion tipoTransaccion = ObtenerTipoTransaccionPorId(id);
        return View(tipoTransaccion);
    }

    [HttpPost]
    public ActionResult ConfirmarEliminar(int id)
    {
        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "DELETE FROM TipoTransacciones WHERE TipoTransaccionId = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    private TipoTransaccion ObtenerTipoTransaccionPorId(int id)
    {
        TipoTransaccion tipoTransaccion = new TipoTransaccion();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM TipoTransacciones WHERE TipoTransaccionId = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoTransaccion.TipoTransaccionId = Convert.ToInt32(reader["TipoTransaccionId"]);
                        tipoTransaccion.Nombre = reader["Nombre"].ToString();
                    }
                }
            }
        }
        return tipoTransaccion;
    }
}
