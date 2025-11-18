using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace C3_DAL
{
    public class DashboardDAL
    {
        private Conexion conexion = new Conexion();

        /// <summary>
        /// Obtiene el total de clientes activos
        /// </summary>
        public int ObtenerTotalClientes()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Cliente WHERE Activo = 1";
                    cmd.Connection = conn;

                    conn.Open();
                    int total = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener total de clientes: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de productos en el catálogo
        /// </summary>
        public int ObtenerTotalProductos()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Producto";
                    cmd.Connection = conn;

                    conn.Open();
                    int total = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener total de productos: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de ventas realizadas
        /// </summary>
        public int ObtenerTotalVentas()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Venta";
                    cmd.Connection = conn;

                    conn.Open();
                    int total = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener total de ventas: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el monto total de ventas del día actual
        /// </summary>
        public decimal ObtenerVentasDelDia()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT ISNULL(SUM(Total), 0) 
                                       FROM Venta 
                                       WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)";
                    cmd.Connection = conn;

                    conn.Open();
                    decimal total = (decimal)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas del día: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de usuarios registrados en el sistema
        /// </summary>
        public int ObtenerTotalUsuarios()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Usuario WHERE Estado = 1";
                    cmd.Connection = conn;

                    conn.Open();
                    int total = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener total de usuarios: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene las ventas del mes actual
        /// </summary>
        public decimal ObtenerVentasDelMes()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT ISNULL(SUM(Total), 0) 
                                       FROM Venta 
                                       WHERE MONTH(Fecha) = MONTH(GETDATE()) 
                                       AND YEAR(Fecha) = YEAR(GETDATE())";
                    cmd.Connection = conn;

                    conn.Open();
                    decimal total = (decimal)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas del mes: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el producto con más stock
        /// </summary>
        public string ObtenerProductoConMasStock()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT TOP 1 Nombre 
                                       FROM Producto 
                                       ORDER BY Stock DESC";
                    cmd.Connection = conn;

                    conn.Open();
                    object resultado = cmd.ExecuteScalar();
                    conn.Close();

                    return resultado != null ? resultado.ToString() : "N/A";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto con más stock: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de productos con stock bajo (menor a 10 unidades)
        /// </summary>
        public int ObtenerProductosConStockBajo()
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Producto WHERE Stock < 10";
                    cmd.Connection = conn;

                    conn.Open();
                    int total = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return total;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos con stock bajo: " + ex.Message);
            }
        }
    }
}