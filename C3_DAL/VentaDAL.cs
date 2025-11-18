using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using C4_ENTIDAD;

namespace C3_DAL
{
    public class VentaDAL
    {
        private Conexion conexion = new Conexion();

        public int Agregar(Venta venta)
        {
            int idVentaGenerado = 0;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = @"INSERT INTO Venta (FECHA, TOTAL, IDUSUARIO, IDCLIENTE) 
                                          VALUES (@fecha, @total, @idusuario, @idcliente);
                                          SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    comando.Parameters.AddWithValue("@fecha", venta.Fecha);
                    comando.Parameters.AddWithValue("@total", venta.Total);
                    comando.Parameters.AddWithValue("@idusuario", venta.IdUsuario);
                    comando.Parameters.AddWithValue("@idcliente", venta.IdCliente > 0 ? (object)venta.IdCliente : DBNull.Value);

                    comando.Connection = conn;
                    conn.Open();

                    idVentaGenerado = (int)comando.ExecuteScalar();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar venta: " + ex.Message);
            }

            return idVentaGenerado;
        }

        public List<Venta> Leer()
        {
            List<Venta> listaVentas = new List<Venta>();

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = @"SELECT v.ID, v.FECHA, v.TOTAL, v.IDUSUARIO, v.IDCLIENTE,
                                                  u.NOMBRECOMPLETO as NombreUsuario,
                                                  c.NOMBRE + ' ' + c.APELLIDO as NombreCliente
                                          FROM Venta v
                                          INNER JOIN Usuario u ON v.IDUSUARIO = u.ID
                                          LEFT JOIN Cliente c ON v.IDCLIENTE = c.ID
                                          ORDER BY v.FECHA DESC";

                    comando.Connection = conn;
                    conn.Open();

                    SqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        Venta venta = new Venta();
                        venta.IdVenta = lector.GetInt32(0);
                        venta.Fecha = lector.GetDateTime(1);
                        venta.Total = lector.GetDecimal(2);
                        venta.IdUsuario = lector.GetInt32(3);
                        venta.IdCliente = lector.IsDBNull(4) ? 0 : lector.GetInt32(4);
                        venta.NombreUsuario = lector.GetString(5);
                        venta.NombreCliente = lector.IsDBNull(6) ? "Sin cliente" : lector.GetString(6);

                        listaVentas.Add(venta);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer ventas: " + ex.Message);
            }

            return listaVentas;
        }

        public Venta ObtenerPorId(int idVenta)
        {
            Venta venta = null;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = @"SELECT v.ID, v.FECHA, v.TOTAL, v.IDUSUARIO, v.IDCLIENTE,
                                                  u.NOMBRECOMPLETO as NombreUsuario,
                                                  c.NOMBRE + ' ' + c.APELLIDO as NombreCliente
                                          FROM Venta v
                                          INNER JOIN Usuario u ON v.IDUSUARIO = u.ID
                                          LEFT JOIN Cliente c ON v.IDCLIENTE = c.ID
                                          WHERE v.ID = @idventa";

                    comando.Parameters.AddWithValue("@idventa", idVenta);
                    comando.Connection = conn;
                    conn.Open();

                    SqlDataReader lector = comando.ExecuteReader();
                    if (lector.Read())
                    {
                        venta = new Venta();
                        venta.IdVenta = lector.GetInt32(0);
                        venta.Fecha = lector.GetDateTime(1);
                        venta.Total = lector.GetDecimal(2);
                        venta.IdUsuario = lector.GetInt32(3);
                        venta.IdCliente = lector.IsDBNull(4) ? 0 : lector.GetInt32(4);
                        venta.NombreUsuario = lector.GetString(5);
                        venta.NombreCliente = lector.IsDBNull(6) ? "Sin cliente" : lector.GetString(6);
                    }

                    conn.Close();
                }

                // Cargar detalles
                if (venta != null)
                {
                    DetalleVentaDAL detalleDAL = new DetalleVentaDAL();
                    venta.Detalles = detalleDAL.ObtenerPorVenta(idVenta);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener venta: " + ex.Message);
            }

            return venta;
        }

        public bool ActualizarTotal(int idVenta, decimal nuevoTotal)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "UPDATE Venta SET TOTAL = @total WHERE ID = @idventa";

                    comando.Parameters.AddWithValue("@total", nuevoTotal);
                    comando.Parameters.AddWithValue("@idventa", idVenta);

                    comando.Connection = conn;
                    conn.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
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