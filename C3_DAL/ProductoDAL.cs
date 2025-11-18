using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace C3_DAL
{
    public class ProductoDAL
    {
        private Conexion conexion = new Conexion();


        private Producto MapearProducto(SqlDataReader lector)
        {
            Producto producto = new Producto();
            producto.Id = lector.GetInt32(0);
            producto.Codigo = lector.GetString(1);
            producto.Nombre = lector.GetString(2);

            producto.Descripcion = lector.IsDBNull(3) ? string.Empty : lector.GetString(3);
            producto.Precio = lector.GetDecimal(4);
            producto.Categoria = lector.GetString(5);
            producto.Disponible = lector.GetBoolean(6);

            producto.Stock = lector.GetDecimal(7);

            producto.FechaCreacion = lector.GetDateTime(8);

            return producto;
        }


        public bool Agregar(Producto producto)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Producto 
                                       (CODIGO, NOMBRE, DESCRIPCION, PRECIO, CATEGORIA, DISPONIBLE, STOCK, FECHACREACION)
                                       VALUES 
                                       (@codigo, @nombre, @descripcion, @precio, @categoria, @disponible, @stock, @fechaCreacion)";

                    cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(producto.Descripcion) ? (object)DBNull.Value : producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@categoria", producto.Categoria);
                    cmd.Parameters.AddWithValue("@disponible", producto.Disponible);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@fechaCreacion", producto.FechaCreacion);

                    cmd.Connection = conn;
                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar producto: " + ex.Message);
            }
        }

        public List<Producto> Leer()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT ID, CODIGO, NOMBRE, DESCRIPCION, PRECIO, CATEGORIA, 
                                              DISPONIBLE, STOCK, FECHACREACION 
                                       FROM Producto 
                                       ORDER BY NOMBRE ASC";
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    while (lector.Read())
                    {
                        lista.Add(MapearProducto(lector));
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer productos: " + ex.Message);
            }

            return lista;
        }
        public Producto BuscarPorId(int idProducto)
        {
            Producto producto = null;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT ID, CODIGO, NOMBRE, DESCRIPCION, PRECIO, CATEGORIA, 
                                              DISPONIBLE, STOCK, FECHACREACION 
                                       FROM Producto 
                                       WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    if (lector.Read())
                    {
                        producto = MapearProducto(lector);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar producto por ID: " + ex.Message);
            }

            return producto;
        }

        public bool Modificar(Producto producto)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE Producto SET 
                                       CODIGO = @codigo,
                                       NOMBRE = @nombre,
                                       DESCRIPCION = @descripcion,
                                       PRECIO = @precio,
                                       CATEGORIA = @categoria,
                                       DISPONIBLE = @disponible,
                                       STOCK = @stock
                                       WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@id", producto.Id);
                    cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
 
                    cmd.Parameters.AddWithValue("@descripcion", string.IsNullOrEmpty(producto.Descripcion) ? (object)DBNull.Value : producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@categoria", producto.Categoria);
                    cmd.Parameters.AddWithValue("@disponible", producto.Disponible);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);

                    cmd.Connection = conn;
                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar producto: " + ex.Message);
            }
        }

        public bool Desactivar(int idProducto)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE Producto SET DISPONIBLE = 0 WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Producto BuscarPorCodigo(string codigo)
        {
            Producto producto = null;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT ID, CODIGO, NOMBRE, DESCRIPCION, PRECIO, CATEGORIA, 
                                              DISPONIBLE, STOCK, FECHACREACION 
                                       FROM Producto 
                                       WHERE CODIGO = @codigo";
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    if (lector.Read())
                    {
                        producto = MapearProducto(lector);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar producto por código: " + ex.Message);
            }

            return producto;
        }

        public int ContarDetallesVentaPorProducto(int idProducto)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM DetalleVenta WHERE IDPRODUCTO = @id";
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    cmd.Connection = conn;

                    conn.Open();
                    int cantidad = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return cantidad;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool ActualizarStock(int idProducto, decimal nuevoStock)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Producto SET STOCK = @stock WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@stock", nuevoStock);
                    cmd.Parameters.AddWithValue("@id", idProducto);

                    cmd.Connection = conn;
                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}