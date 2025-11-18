using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_ENTIDAD
{
    public class Usuarios
    {
        private int _idUsuario;
        private string _username;
        private string _nombreCompleto;
        private string _password;
        private string _rol;
        private bool _estado;
        private int _intentosFallidos;
        private DateTime _fechaIngreso;
        private int _idDomicilio;

        // Propiedades
        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string Username { get => _username; set => _username = value; }
        public string NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
        public string Password { get => _password; set => _password = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public int IntentosFallidos { get => _intentosFallidos; set => _intentosFallidos = value; }
        public DateTime FechaIngreso { get => _fechaIngreso; set => _fechaIngreso = value; }
        public int IdDomicilio { get => _idDomicilio; set => _idDomicilio = value; }

        // Constructores
        public Usuarios()
        {
        }

        public Usuarios(int idUsuario, string username, string nombreCompleto, string password,
                      string rol, bool estado, int intentosFallidos, DateTime fechaIngreso, int idDomicilio)
        {
            _idUsuario = idUsuario;
            _username = username;
            _nombreCompleto = nombreCompleto;
            _password = password;
            _rol = rol;
            _estado = estado;
            _intentosFallidos = intentosFallidos;
            _fechaIngreso = fechaIngreso;
            _idDomicilio = idDomicilio;
        }

        // Métodos adicionales
        public bool EsAdministrador()
        {
            return Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
                   Rol.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        public bool EstaBloqueado()
        {
            return IntentosFallidos >= 3 || !Estado;
        }
    }
}
