using Final_Integracion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

public class TransaccionController : Controller
{
    static string cadena = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true";

    public ActionResult Index()
    {
        List<Transaccion> transacciones = new List<Transaccion>();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM Transacciones";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaccion transaccion = new Transaccion();
                        transaccion.TransaccionId = Convert.ToInt32(reader["TransaccionId"]);
                        transaccion.TipoTransaccionId = Convert.ToInt32(reader["TipoTransaccionId"]);
                        transaccion.IdArticulo = Convert.ToInt32(reader["IdArticulo"]);
                        transaccion.Fecha = Convert.ToDateTime(reader["Fecha"]);
                        transaccion.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                        transaccion.Monto = Convert.ToDecimal(reader["Monto"]);

                        transacciones.Add(transaccion);
                    }
                }
            }
        }

        return View(transacciones);
    }

    public ActionResult Detalles(int id)
    {
        Transaccion transaccion = ObtenerTransaccionPorId(id);
        return View(transaccion);
    }

    public ActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Agregar(Transaccion transaccion)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "INSERT INTO Transacciones (TipoTransaccionId, IdArticulo, Fecha, Cantidad, Monto) " +
                                  "VALUES (@TipoTransaccionId, @IdArticulo, @Fecha, @Cantidad, @Monto)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@TipoTransaccionId", transaccion.TipoTransaccionId);
                    comando.Parameters.AddWithValue("@IdArticulo", transaccion.IdArticulo);
                    comando.Parameters.AddWithValue("@Fecha", transaccion.Fecha);
                    comando.Parameters.AddWithValue("@Cantidad", transaccion.Cantidad);
                    comando.Parameters.AddWithValue("@Monto", transaccion.Monto);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(transaccion);
    }

    public ActionResult Editar(int id)
    {
        Transaccion transaccion = ObtenerTransaccionPorId(id);
        return View(transaccion);
    }

    [HttpPost]
    public ActionResult Editar(Transaccion transaccion)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "UPDATE Transacciones SET TipoTransaccionId = @TipoTransaccionId, " +
                                  "IdArticulo = @IdArticulo, Fecha = @Fecha, Cantidad = @Cantidad, " +
                                  "Monto = @Monto WHERE TransaccionId = @Id";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@TipoTransaccionId", transaccion.TipoTransaccionId);
                    comando.Parameters.AddWithValue("@IdArticulo", transaccion.IdArticulo);
                    comando.Parameters.AddWithValue("@Fecha", transaccion.Fecha);
                    comando.Parameters.AddWithValue("@Cantidad", transaccion.Cantidad);
                    comando.Parameters.AddWithValue("@Monto", transaccion.Monto);
                    comando.Parameters.AddWithValue("@Id", transaccion.TransaccionId);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(transaccion);
    }

    public ActionResult Eliminar(int id)
    {
        Transaccion transaccion = ObtenerTransaccionPorId(id);
        return View(transaccion);
    }

    [HttpPost]
    public ActionResult ConfirmarEliminar(int id)
    {
        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "DELETE FROM Transacciones WHERE TransaccionId = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    private Transaccion ObtenerTransaccionPorId(int id)
    {
        Transaccion transaccion = new Transaccion();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM Transacciones WHERE TransaccionId = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transaccion.TransaccionId = Convert.ToInt32(reader["TransaccionId"]);
                        transaccion.TipoTransaccionId = Convert.ToInt32(reader["TipoTransaccionId"]);
                        transaccion.IdArticulo = Convert.ToInt32(reader["IdArticulo"]);
                        transaccion.Fecha = Convert.ToDateTime(reader["Fecha"]);
                        transaccion.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                        transaccion.Monto = Convert.ToDecimal(reader["Monto"]);
                    }
                }
            }
        }
        return transaccion;
    }
}
