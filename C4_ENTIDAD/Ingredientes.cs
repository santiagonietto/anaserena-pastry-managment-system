using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_ENTIDAD
{
    public class Ingredientes
    {
        private int id;
        private string descripcion;
        private string codigo;
        private string nombre;
        private decimal stock;
        private string unidadMedida;
        private int precioUnitario;
        private bool activo;

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public decimal Stock { get => stock; set => stock = value; }
        public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
        public int PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public bool Activo { get => activo; set => activo = value; }
        public Ingredientes()
        {

        }
        public Ingredientes(int id, string descripcion, string codigo, string nombre, decimal stock, string unidadMedida, int precioUnitario, bool activo)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.codigo = codigo;
            this.nombre = nombre;
            this.stock = stock;
            this.unidadMedida = unidadMedida;
            this.precioUnitario = precioUnitario;
            this.activo = activo;
        }

        public int ActualizarStock(decimal cantidad)
        {
            Stock += cantidad;
            return (int)Stock;
        }

        public int VerificarStockMinimo(decimal minimo = 1)
        {
            return Stock < minimo ? 1 : 0;
        }

        public string ConocerUnidadMedida()
        {
            return UnidadMedida;
        }

        public string ObtenerDescripcion()
        {
            return Descripcion;
        }
    }
}
