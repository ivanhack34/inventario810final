using Final_Integracion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

public class AlmacenController : Controller
{
    static string cadena = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true";

    public ActionResult Index()
    {
        List<Almacen> almacenes = new List<Almacen>();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM Almacenes";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Almacen almacen = new Almacen();
                        almacen.AlmacenID = Convert.ToInt32(reader["AlmacenID"]);
                        almacen.Descripcion = reader["Descripcion"].ToString();
                        almacen.Estado = Convert.ToBoolean(reader["Estado"]);

                        almacenes.Add(almacen);
                    }
                }
            }
        }

        return View(almacenes);
    }

    public ActionResult Detalles(int id)
    {
        Almacen almacen = ObtenerAlmacenPorId(id);
        return View(almacen);
    }

    public ActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Agregar(Almacen almacen)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "INSERT INTO Almacenes (Descripcion, Estado) VALUES (@Descripcion, @Estado)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Descripcion", almacen.Descripcion);
                    comando.Parameters.AddWithValue("@Estado", almacen.Estado);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(almacen);
    }

    public ActionResult Editar(int id)
    {
        Almacen almacen = ObtenerAlmacenPorId(id);
        return View(almacen);
    }

    [HttpPost]
    public ActionResult Editar(Almacen almacen)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "UPDATE Almacenes SET Descripcion = @Descripcion, Estado = @Estado WHERE AlmacenID = @Id";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Descripcion", almacen.Descripcion);
                    comando.Parameters.AddWithValue("@Estado", almacen.Estado);
                    comando.Parameters.AddWithValue("@Id", almacen.AlmacenID);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
        return View(almacen);
    }

    public ActionResult Eliminar(int id)
    {
        Almacen almacen = ObtenerAlmacenPorId(id);
        return View(almacen);
    }

    [HttpPost]
    public ActionResult ConfirmarEliminar(int id)
    {
        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "DELETE FROM Almacenes WHERE AlmacenID = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    private Almacen ObtenerAlmacenPorId(int id)
    {
        Almacen almacen = new Almacen();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM Almacenes WHERE AlmacenID = @Id";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        almacen.AlmacenID = Convert.ToInt32(reader["AlmacenID"]);
                        almacen.Descripcion = reader["Descripcion"].ToString();
                        almacen.Estado = Convert.ToBoolean(reader["Estado"]);
                    }
                }
            }
        }
        return almacen;
    }
}
