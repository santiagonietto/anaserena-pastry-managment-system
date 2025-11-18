using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_DAL;

namespace C2_BLL
{
    public class DashboardBLL
    {
        private DashboardDAL dashboardDAL = new DashboardDAL();

        /// <summary>
        /// Obtiene el total de clientes activos del sistema
        /// </summary>
        public int ObtenerTotalClientes()
        {
            try
            {
                int total = dashboardDAL.ObtenerTotalClientes();

                if (total < 0)
                {
                    throw new Exception("El total de clientes no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener clientes: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de productos en el catálogo
        /// </summary>
        public int ObtenerTotalProductos()
        {
            try
            {
                int total = dashboardDAL.ObtenerTotalProductos();

                if (total < 0)
                {
                    throw new Exception("El total de productos no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener productos: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de ventas realizadas
        /// </summary>
        public int ObtenerTotalVentas()
        {
            try
            {
                int total = dashboardDAL.ObtenerTotalVentas();

                if (total < 0)
                {
                    throw new Exception("El total de ventas no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener ventas: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el monto total de ventas del día actual
        /// </summary>
        public decimal ObtenerVentasDelDia()
        {
            try
            {
                decimal total = dashboardDAL.ObtenerVentasDelDia();

                if (total < 0)
                {
                    throw new Exception("El monto de ventas no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener ventas del día: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el total de usuarios activos del sistema
        /// </summary>
        public int ObtenerTotalUsuarios()
        {
            try
            {
                int total = dashboardDAL.ObtenerTotalUsuarios();

                if (total < 0)
                {
                    throw new Exception("El total de usuarios no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener usuarios: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el monto total de ventas del mes actual
        /// </summary>
        public decimal ObtenerVentasDelMes()
        {
            try
            {
                decimal total = dashboardDAL.ObtenerVentasDelMes();

                if (total < 0)
                {
                    throw new Exception("El monto de ventas del mes no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener ventas del mes: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene el nombre del producto con mayor stock
        /// </summary>
        public string ObtenerProductoConMasStock()
        {
            try
            {
                string producto = dashboardDAL.ObtenerProductoConMasStock();

                if (string.IsNullOrWhiteSpace(producto))
                {
                    return "Sin productos";
                }

                return producto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener producto con más stock: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la cantidad de productos con stock bajo (menor a 10 unidades)
        /// </summary>
        public int ObtenerProductosConStockBajo()
        {
            try
            {
                int total = dashboardDAL.ObtenerProductosConStockBajo();

                if (total < 0)
                {
                    throw new Exception("El total de productos con stock bajo no puede ser negativo");
                }

                return total;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica de negocio al obtener productos con stock bajo: " + ex.Message);
            }
        }

        /// <summary>
        /// Verifica si hay alertas de stock bajo
        /// </summary>
        public bool HayAlertasDeStock()
        {
            try
            {
                int productosStockBajo = dashboardDAL.ObtenerProductosConStockBajo();
                return productosStockBajo > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar alertas de stock: " + ex.Message);
            }
        }

        /// <summary>
        /// Calcula el promedio de ventas diarias del mes actual
        /// </summary>
        public decimal CalcularPromedioVentasDiarias()
        {
            try
            {
                decimal ventasMes = dashboardDAL.ObtenerVentasDelMes();
                int diaActual = DateTime.Now.Day;

                if (diaActual == 0)
                {
                    return 0;
                }

                decimal promedio = ventasMes / diaActual;
                return Math.Round(promedio, 2);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular promedio de ventas diarias: " + ex.Message);
            }
        }
    }
}