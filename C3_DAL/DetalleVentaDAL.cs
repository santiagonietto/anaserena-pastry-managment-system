using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using C4_ENTIDAD;

namespace C3_DAL
{
    public class DetalleVentaDAL
    {
        private Conexion conexion = new Conexion();

        public bool Agregar(DetalleVenta detalle)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = @"INSERT INTO DetalleVenta (CANTIDAD, PRECIOUNITARIO, SUBTOTAL, IDVENTA, IDPRODUCTO)
                                          VALUES (@cantidad, @preciounitario, @subtotal, @idventa, @idproducto)";

                    comando.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                    comando.Parameters.AddWithValue("@preciounitario", detalle.PrecioUnitario);
                    comando.Parameters.AddWithValue("@subtotal", detalle.Subtotal);
                    comando.Parameters.AddWithValue("@idventa", detalle.IdVenta);
                    comando.Parameters.AddWithValue("@idproducto", detalle.IdProducto);

                    comando.Connection = conn;
                    conn.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar detalle de venta: " + ex.Message);
            }
        }

        public List<DetalleVenta> ObtenerPorVenta(int idVenta)
        {
            List<DetalleVenta> listaDetalles = new List<DetalleVenta>();

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = @"SELECT dv.ID, dv.CANTIDAD, dv.PRECIOUNITARIO, dv.SUBTOTAL, 
                                                  dv.IDVENTA, dv.IDPRODUCTO,
                                                  p.NOMBRE as NombreProducto, p.CODIGO as CodigoProducto
                                          FROM DetalleVenta dv
                                          INNER JOIN Producto p ON dv.IDPRODUCTO = p.ID
                                          WHERE dv.IDVENTA = @idventa";

                    comando.Parameters.AddWithValue("@idventa", idVenta);
                    comando.Connection = conn;
                    conn.Open();

                    SqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        DetalleVenta detalle = new DetalleVenta();
                        detalle.IdDetalleVenta = lector.GetInt32(0);
                        detalle.Cantidad = lector.GetInt32(1);
                        detalle.PrecioUnitario = lector.GetDecimal(2);
                        detalle.Subtotal = lector.GetDecimal(3);
                        detalle.IdVenta = lector.GetInt32(4);
                        detalle.IdProducto = lector.GetInt32(5);
                        detalle.NombreProducto = lector.GetString(6);
                        detalle.CodigoProducto = lector.GetString(7);

                        listaDetalles.Add(detalle);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalles de venta: " + ex.Message);
            }

            return listaDetalles;
        }
    }
}