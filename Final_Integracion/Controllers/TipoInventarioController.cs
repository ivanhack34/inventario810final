using Final_Integracion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

public class TipoInventarioController : Controller
{
    static string connectionString = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true";

    public ActionResult Index()
    {
        List<TipoInventario> tiposInventario = new List<TipoInventario>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM TiposInventarios";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoInventario tipoInventario = new TipoInventario();
                        tipoInventario.TipoInventarioId = Convert.ToInt32(reader["TipoInventarioId"]);
                        tipoInventario.Descripcion = reader["Descripcion"].ToString();
                        tipoInventario.CuentaContable = reader["CuentaContable"].ToString();
                        tipoInventario.Estado = Convert.ToBoolean(reader["Estado"]);

                        tiposInventario.Add(tipoInventario);
                    }
                }
            }
        }

        return View(tiposInventario);
    }

    public ActionResult Detalles(int id)
    {
        TipoInventario tipoInventario = ObtenerTipoInventarioPorId(id);
        return View(tipoInventario);
    }

    public ActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Agregar(TipoInventario tipoInventario)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO TiposInventarios (Descripcion, CuentaContable, Estado) VALUES (@Descripcion, @CuentaContable, @Estado)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", tipoInventario.Descripcion);
                    command.Parameters.AddWithValue("@CuentaContable", tipoInventario.CuentaContable);
                    command.Parameters.AddWithValue("@Estado", tipoInventario.Estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(tipoInventario);
    }

    public ActionResult Editar(int id)
    {
        TipoInventario tipoInventario = ObtenerTipoInventarioPorId(id);
        return View(tipoInventario);
    }

    [HttpPost]
    public ActionResult Editar(TipoInventario tipoInventario)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE TiposInventarios SET Descripcion = @Descripcion, CuentaContable = @CuentaContable, Estado = @Estado WHERE TipoInventarioId = @TipoInventarioId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TipoInventarioId", tipoInventario.TipoInventarioId);
                    command.Parameters.AddWithValue("@Descripcion", tipoInventario.Descripcion);
                    command.Parameters.AddWithValue("@CuentaContable", tipoInventario.CuentaContable);
                    command.Parameters.AddWithValue("@Estado", tipoInventario.Estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(tipoInventario);
    }

    public ActionResult Eliminar(int id)
    {
        TipoInventario tipoInventario = ObtenerTipoInventarioPorId(id);
        return View(tipoInventario);
    }

    [HttpPost]
    public ActionResult ConfirmarEliminar(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM TiposInventarios WHERE TipoInventarioId = @TipoInventarioId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TipoInventarioId", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    private TipoInventario ObtenerTipoInventarioPorId(int id)
    {
        TipoInventario tipoInventario = new TipoInventario();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM TiposInventarios WHERE TipoInventarioId = @TipoInventarioId";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TipoInventarioId", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tipoInventario.TipoInventarioId = Convert.ToInt32(reader["TipoInventarioId"]);
                        tipoInventario.Descripcion = reader["Descripcion"].ToString();
                        tipoInventario.CuentaContable = reader["CuentaContable"].ToString();
                        tipoInventario.Estado = Convert.ToBoolean(reader["Estado"]);
                    }
                }
            }
        }
        return tipoInventario;
    }
}
