using Final_Integracion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

public class TipoMovimientoController : Controller
{
    static string cadena = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true";

    public ActionResult Index()
    {
        List<TipoMovimiento> tiposMovimiento = new List<TipoMovimiento>();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM TipoMovimientos";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoMovimiento tipoMovimiento = new TipoMovimiento();
                        tipoMovimiento.TipoMovimientoId = Convert.ToInt32(reader["TipoMovimientoId"]);
                        tipoMovimiento.Nombre = reader["Nombre"].ToString();

                        tiposMovimiento.Add(tipoMovimiento);
                    }
                }
            }
        }

        return View(tiposMovimiento);
    }

    public ActionResult Detalles(int id)
    {
        TipoMovimiento tipoMovimiento = ObtenerTipoMovimientoPorId(id);
        return View(tipoMovimiento);
    }

    public ActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Agregar(TipoMovimiento tipoMovimiento)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "INSERT INTO TipoMovimientos (Nombre) VALUES (@Nombre)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", tipoMovimiento.Nombre);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(tipoMovimiento);
    }

    public ActionResult Editar(int id)
    {
        TipoMovimiento tipoMovimiento = ObtenerTipoMovimientoPorId(id);
        return View(tipoMovimiento);
    }

    [HttpPost]
    public ActionResult Editar(TipoMovimiento tipoMovimiento)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "UPDATE TipoMovimientos SET Nombre = @Nombre WHERE TipoMovimientoId = @Id";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", tipoMovimiento.Nombre);
                    comando.Parameters.AddWithValue("@Id", tipoMovimiento.TipoMovimientoId);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(tipoMovimiento);
    }

    public ActionResult Eliminar(int id)
    {
        TipoMovimiento tipoMovimiento = ObtenerTipoMovimientoPorId(id);
        return View(tipoMovimiento);
    }

    [HttpPost]
    public ActionResult ConfirmarEliminar(int id)
    {
        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "DELETE FROM TipoMovimientos WHERE TipoMovimientoId = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    private TipoMovimiento ObtenerTipoMovimientoPorId(int id)
    {
        TipoMovimiento tipoMovimiento = new TipoMovimiento();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM TipoMovimientos WHERE TipoMovimientoId = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoMovimiento.TipoMovimientoId = Convert.ToInt32(reader["TipoMovimientoId"]);
                        tipoMovimiento.Nombre = reader["Nombre"].ToString();
                    }
                }
            }
        }
        return tipoMovimiento;
    }
}
