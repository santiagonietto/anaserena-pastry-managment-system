using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_ENTIDAD
{
    public class Cliente
    {
        private int _idCliente;
        private string _nombre;
        private string _apellido;
        private string _telefono;
        private string _email;
        private bool _activo;
        private DateTime _fechaIngreso;

        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public string Email { get => _email; set => _email = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public DateTime FechaIngreso { get => _fechaIngreso; set => _fechaIngreso = value; }

        public Cliente()
        {
        }

        public Cliente(int idCliente, string nombre, string apellido, string telefono, string email, bool activo, DateTime fechaIngreso)
        {
            _idCliente = idCliente;
            _nombre = nombre;
            _apellido = apellido;
            _telefono = telefono;
            _email = email;
            _activo = activo;
            _fechaIngreso = fechaIngreso;
        }

        public string ObtenerNombreCompleto()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
