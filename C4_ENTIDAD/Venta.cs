using System;
using System.Collections.Generic;

namespace C4_ENTIDAD
{
    public class Venta
    {
        private int _idVenta;
        private DateTime _fecha;
        private decimal _total;
        private int _idUsuario;
        private int _idCliente;


        public int IdVenta { get => _idVenta; set => _idVenta = value; }
        public DateTime Fecha { get => _fecha; set => _fecha = value; }
        public decimal Total { get => _total; set => _total = value; }
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }


        public string NombreCliente { get; set; }
        public string NombreUsuario { get; set; }

   
        public List<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<DetalleVenta>();
            Fecha = DateTime.Now;
            Total = 0;
        }

        public Venta(int idVenta, DateTime fecha, decimal total, int idUsuario, int idCliente)
        {
            _idVenta = idVenta;
            _fecha = fecha;
            _total = total;
            _idUsuario = idUsuario;
            _idCliente = idCliente;
            Detalles = new List<DetalleVenta>();
        }

  
        public void CalcularTotal()
        {
            Total = 0;
            foreach (var detalle in Detalles)
            {
                Total += detalle.Subtotal;
            }
        }

        public void AgregarDetalle(DetalleVenta detalle)
        {
            Detalles.Add(detalle);
            CalcularTotal();
        }

        public void EliminarDetalle(int idProducto)
        {
            Detalles.RemoveAll(d => d.IdProducto == idProducto);
            CalcularTotal();
        }
    }
}