using System;

namespace C4_ENTIDAD
{
    public class DetalleVenta
    {
        private int _idDetalleVenta;
        private int _cantidad;
        private decimal _precioUnitario;
        private decimal _subtotal;
        private int _idVenta;
        private int _idProducto;


        public int IdDetalleVenta { get => _idDetalleVenta; set => _idDetalleVenta = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public decimal PrecioUnitario { get => _precioUnitario; set => _precioUnitario = value; }
        public decimal Subtotal { get => _subtotal; set => _subtotal = value; }
        public int IdVenta { get => _idVenta; set => _idVenta = value; }
        public int IdProducto { get => _idProducto; set => _idProducto = value; }


        public string NombreProducto { get; set; }
        public string CodigoProducto { get; set; }

        public DetalleVenta()
        {
        }

        public DetalleVenta(int idDetalleVenta, int cantidad, decimal precioUnitario,
                           decimal subtotal, int idVenta, int idProducto)
        {
            _idDetalleVenta = idDetalleVenta;
            _cantidad = cantidad;
            _precioUnitario = precioUnitario;
            _subtotal = subtotal;
            _idVenta = idVenta;
            _idProducto = idProducto;
        }

        public void CalcularSubtotal()
        {
            Subtotal = Cantidad * PrecioUnitario;
        }

        public override string ToString()
        {
            return $"{NombreProducto} - Cant: {Cantidad} - Precio: ${PrecioUnitario} - Subtotal: ${Subtotal}";
        }
    }
}