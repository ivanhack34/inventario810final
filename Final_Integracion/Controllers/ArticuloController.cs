using Final_Integracion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

public class ArticuloController : Controller
{
    static string cadena = "Data Source=DESKTOP-UA2E864\\SQLEXPRESS; Initial Catalog=DB_ACCESO;Integrated Security=true";

    public ActionResult Index()
    {
        List<Articulo> articulos = new List<Articulo>();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM Articulos";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Articulo articulo = new Articulo();
                        articulo.IdArticulo = Convert.ToInt32(reader["IdArticulo"]);
                        articulo.Descripcion = reader["Descripcion"].ToString();
                        articulo.Existencia = Convert.ToInt32(reader["Existencia"]);
                        articulo.TipoInventarioId = Convert.ToInt32(reader["TipoInventarioId"]);
                        articulo.AlmacenID = Convert.ToInt32(reader["AlmacenID"]);
                        articulo.TransaccionId = Convert.ToInt32(reader["TransaccionId"]);
                        articulo.AsientoId = Convert.ToInt32(reader["AsientoId"]);
                        articulo.CostoUnitario = Convert.ToDecimal(reader["CostoUnitario"]);
                        articulo.Estado = Convert.ToBoolean(reader["Estado"]);

                        articulos.Add(articulo);
                    }
                }
            }
        }

        return View(articulos);
    }

    public ActionResult Detalles(int id)
    {
        Articulo articulo = ObtenerArticuloPorId(id);
        return View(articulo);
    }

    [HttpGet]
    public ActionResult Agregar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Agregar(Articulo articulo)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "INSERT INTO Articulos (Descripcion, Existencia, TipoInventarioId, AlmacenID, TransaccionId, AsientoId, CostoUnitario, Estado) " +
                    "VALUES (@Descripcion, @Existencia, @TipoInventarioId, @AlmacenID, @TransaccionId, @AsientoId, @CostoUnitario, @Estado)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
                    comando.Parameters.AddWithValue("@Existencia", articulo.Existencia);
                    comando.Parameters.AddWithValue("@TipoInventarioId", articulo.TipoInventarioId);
                    comando.Parameters.AddWithValue("@AlmacenID", articulo.AlmacenID);
                    comando.Parameters.AddWithValue("@TransaccionId", articulo.TransaccionId);
                    comando.Parameters.AddWithValue("@AsientoId", articulo.AsientoId);
                    comando.Parameters.AddWithValue("@CostoUnitario", articulo.CostoUnitario);
                    comando.Parameters.AddWithValue("@Estado", articulo.Estado);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        return View(articulo);
    }

    public ActionResult Editar(int id)
    {
        Articulo articulo = ObtenerArticuloPorId(id);
        return View(articulo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Editar(Articulo articulo)
    {
        if (ModelState.IsValid)
        {
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string consulta = "UPDATE Articulos SET Descripcion = @Descripcion, Existencia = @Existencia, TipoInventarioId = @TipoInventarioId, " +
                    "AlmacenID = @AlmacenID, TransaccionId = @TransaccionId, AsientoId = @AsientoId, CostoUnitario = @CostoUnitario, Estado = @Estado " +
                    "WHERE IdArticulo = @IdArticulo";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Descripcion", articulo.Descripcion);
                    comando.Parameters.AddWithValue("@Existencia", articulo.Existencia);
                    comando.Parameters.AddWithValue("@TipoInventarioId", articulo.TipoInventarioId);
                    comando.Parameters.AddWithValue("@AlmacenID", articulo.AlmacenID);
                    comando.Parameters.AddWithValue("@TransaccionId", articulo.TransaccionId);
                    comando.Parameters.AddWithValue("@AsientoId", articulo.AsientoId);
                    comando.Parameters.AddWithValue("@CostoUnitario", articulo.CostoUnitario);
                    comando.Parameters.AddWithValue("@Estado", articulo.Estado);
                    comando.Parameters.AddWithValue("@IdArticulo", articulo.IdArticulo);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        return View(articulo);
    }

    public ActionResult Eliminar(int id)
    {
        Articulo articulo = ObtenerArticuloPorId(id);
        return View(articulo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult ConfirmarEliminar(int id)
    {
        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "DELETE FROM Articulos WHERE IdArticulo = @IdArticulo";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@IdArticulo", id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Index");
    }

    private Articulo ObtenerArticuloPorId(int id)
    {
        Articulo articulo = new Articulo();

        using (SqlConnection conexion = new SqlConnection(cadena))
        {
            string consulta = "SELECT * FROM Articulos WHERE IdArticulo = @IdArticulo";

            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@IdArticulo", id);

                conexion.Open();

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        articulo.IdArticulo = Convert.ToInt32(reader["IdArticulo"]);
                        articulo.Descripcion = reader["Descripcion"].ToString();
                        articulo.Existencia = Convert.ToInt32(reader["Existencia"]);
                        articulo.TipoInventarioId = Convert.ToInt32(reader["TipoInventarioId"]);
                        articulo.AlmacenID = Convert.ToInt32(reader["AlmacenID"]);
                        articulo.TransaccionId = Convert.ToInt32(reader["TransaccionId"]);
                        articulo.AsientoId = Convert.ToInt32(reader["AsientoId"]);
                        articulo.CostoUnitario = Convert.ToDecimal(reader["CostoUnitario"]);
                        articulo.Estado = Convert.ToBoolean(reader["Estado"]);
                    }
                }
            }
        }

        return articulo;
    }
}
