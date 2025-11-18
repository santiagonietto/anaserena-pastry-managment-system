using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_ENTIDAD
{
    public class Producto
    {
        private int id;
        private string codigo;
        private string nombre;
        private string descripcion;
        private decimal precio;
        private string categoria;
        private bool disponible;
        private decimal stock;
        private DateTime fechaCreacion;

        public int Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public bool Disponible { get => disponible; set => disponible = value; }

        public decimal Stock { get => stock; set => stock = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }

        public List<Ingredientes> Ingredientes { get; set; }
       

        public Producto()
        {
            Ingredientes = new List<Ingredientes>();
        }

        public Producto(int id, string codigo, string nombre, string descripcion, decimal precio, string categoria, bool disponible, decimal stock, DateTime fechaCreacion) // El constructor debe usar decimal stock
        {
            this.id = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            this.categoria = categoria;
            this.disponible = disponible;
            this.stock = stock;
            this.fechaCreacion = fechaCreacion;
        }

        public int ActualizarPrecio(decimal nuevoPrecio)
        {
            Precio = nuevoPrecio;
            return (int)Precio;
        }

        public bool Desactivar()
        {
            Disponible = false;
            return Disponible;
        }

        public bool Reactivar()
        {
            Disponible = true;
            return Disponible;
        }
        public string EstaDisponible()
        {
            return Disponible ? "Disponible" : "No Disponible";
        }

        public List<Ingredientes> ConocerIngredientes()
        {
            return Ingredientes;
        }

        public bool TieneStock(int cantidad)
        {
            return Stock >= cantidad;
        }

        public void DescontarStock(int cantidad)
        {
            if (TieneStock(cantidad))
            {
                Stock -= cantidad;
            }
            else
            {
                throw new Exception($"Stock insuficiente. Disponible: {Stock}");
            }
        }
    }
}

