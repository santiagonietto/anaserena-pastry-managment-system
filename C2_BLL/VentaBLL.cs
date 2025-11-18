using System;
using System.Collections.Generic;
using C3_DAL;
using C4_ENTIDAD;

namespace C2_BLL
{
    public class VentaBLL
    {
        private VentaDAL ventaDAL = new VentaDAL();
        private DetalleVentaDAL detalleVentaDAL = new DetalleVentaDAL();
        private ProductoDAL productoDAL = new ProductoDAL();

        public bool RegistrarVenta(Venta venta)
        {
            try
            {

                if (venta.Detalles == null || venta.Detalles.Count == 0)
                {
                    throw new Exception("Debe agregar al menos un producto a la venta");
                }

                foreach (var detalle in venta.Detalles)
                {
                    Producto producto = productoDAL.BuscarPorId(detalle.IdProducto);

                    if (producto == null)
                    {
                        throw new Exception($"El producto con ID {detalle.IdProducto} no existe");
                    }

                    if (!producto.Disponible)
                    {
                        throw new Exception($"El producto {producto.Nombre} no está disponible");
                    }

                    if (!producto.TieneStock(detalle.Cantidad))
                    {
                        throw new Exception($"Stock insuficiente para {producto.Nombre}. Disponible: {producto.Stock}");
                    }
                }

                venta.CalcularTotal();

                if (venta.Total <= 0)
                {
                    throw new Exception("El total de la venta debe ser mayor a cero");
                }


                int idVenta = ventaDAL.Agregar(venta);

                if (idVenta <= 0)
                {
                    throw new Exception("Error al registrar la venta");
                }

                foreach (var detalle in venta.Detalles)
                {
                    detalle.IdVenta = idVenta;
                    detalle.CalcularSubtotal();

                    if (!detalleVentaDAL.Agregar(detalle))
                    {
                        throw new Exception("Error al agregar detalle de venta");
                    }


                    Producto producto = productoDAL.BuscarPorId(detalle.IdProducto);
                    producto.DescontarStock(detalle.Cantidad);
                    productoDAL.ActualizarStock(producto.Id, producto.Stock);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar venta: " + ex.Message);
            }
        }


        public List<Venta> ObtenerTodasLasVentas()
        {
            try
            {
                return ventaDAL.Leer();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas: " + ex.Message);
            }
        }

        public Venta ObtenerVentaPorId(int idVenta)
        {
            try
            {
                if (idVenta <= 0)
                {
                    throw new Exception("ID de venta inválido");
                }

                Venta venta = ventaDAL.ObtenerPorId(idVenta);

                if (venta == null)
                {
                    throw new Exception("Venta no encontrada");
                }

                return venta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener venta: " + ex.Message);
            }
        }

        public void ValidarAgregarProducto(int idProducto, int cantidad)
        {
            try
            {
                if (idProducto <= 0)
                {
                    throw new Exception("Debe seleccionar un producto");
                }

                if (cantidad <= 0)
                {
                    throw new Exception("La cantidad debe ser mayor a cero");
                }

                Producto producto = productoDAL.BuscarPorId(idProducto);

                if (producto == null)
                {
                    throw new Exception("Producto no encontrado");
                }

                if (!producto.Disponible)
                {
                    throw new Exception($"El producto {producto.Nombre} no está disponible");
                }

                if (!producto.TieneStock(cantidad))
                {
                    throw new Exception($"Stock insuficiente. Disponible: {producto.Stock}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}