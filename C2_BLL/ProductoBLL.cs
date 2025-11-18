using C3_DAL;
using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C2_BLL
{
    public class ProductoBLL
    {
        private ProductoDAL productoDAL = new ProductoDAL();

        public bool RegistrarProducto(Producto producto)
        {
            try
            {
                ValidarCamposObligatorios(producto);

                if (ExisteCodigo(producto.Codigo))
                {
                    throw new Exception("El código de producto ya existe.");
                }

                producto.FechaCreacion = DateTime.Now;

                producto.Disponible = producto.Stock > 0;

                return productoDAL.Agregar(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el producto: " + ex.Message);
            }
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            try
            {
                return productoDAL.Leer().Where(p => p.Disponible).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de productos: " + ex.Message);
            }
        }

        public Producto ObtenerPorId(int idProducto)
        {
            try
            {
                if (idProducto <= 0)
                {
                    throw new Exception("ID de producto inválido.");
                }

                return productoDAL.BuscarPorId(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto: " + ex.Message);
            }
        }

        public bool ModificarProducto(Producto producto)
        {
            try
            {
                if (producto.Id <= 0)
                {
                    throw new Exception("ID de producto inválido para la modificación.");
                }

                ValidarCamposObligatorios(producto);

                if (ExisteCodigoExcluyendo(producto.Codigo, producto.Id))
                {
                    throw new Exception("El código de producto ya existe para otro producto.");
                }

                producto.Disponible = producto.Stock > 0;

                return productoDAL.Modificar(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el producto: " + ex.Message);
            }
        }

        public bool EliminarProducto(int idProducto)
        {
            try
            {
                if (idProducto <= 0)
                {
                    throw new Exception("ID de producto inválido.");
                }

                int cantidadDetallesVenta = productoDAL.ContarDetallesVentaPorProducto(idProducto);
                if (cantidadDetallesVenta > 0)
                {
                    throw new Exception("No se puede eliminar este producto porque está asociado a registros de venta.");
                }


                bool resultado = productoDAL.Desactivar(idProducto);

                if (!resultado)
                {
                    throw new Exception("No se pudo desactivar el producto.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }

        private void ValidarCamposObligatorios(Producto producto)
        {
            List<string> camposVacios = new List<string>();

            if (string.IsNullOrWhiteSpace(producto.Codigo))
                camposVacios.Add("Código");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                camposVacios.Add("Nombre");

            if (string.IsNullOrWhiteSpace(producto.Categoria))
                camposVacios.Add("Categoría");

            if (producto.Precio <= 0)
                camposVacios.Add("Precio (debe ser mayor a 0)");

            if (producto.Stock < 0)
                camposVacios.Add("Stock (no puede ser negativo)");

            if (camposVacios.Count > 0)
            {
                string mensaje = "Los siguientes campos son obligatorios o inválidos: " + string.Join(", ", camposVacios);
                throw new Exception(mensaje);
            }
        }

        private bool ExisteCodigo(string codigo)
        {
            Producto producto = productoDAL.BuscarPorCodigo(codigo);
            return producto != null;
        }

        private bool ExisteCodigoExcluyendo(string codigo, int idProductoActual)
        {

            Producto producto = productoDAL.BuscarPorCodigo(codigo);

            return producto != null && producto.Id != idProductoActual;
        }

        public List<Producto> ObtenerProductosDisponibles()
        {
            try
            {
                List<Producto> todosProductos = productoDAL.Leer();
                List<Producto> disponibles = new List<Producto>();

                foreach (var producto in todosProductos)
                {
                    if (producto.Disponible && producto.Stock > 0)
                    {
                        disponibles.Add(producto);
                    }
                }

                return disponibles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos disponibles: " + ex.Message);
            }
        }
    }
}