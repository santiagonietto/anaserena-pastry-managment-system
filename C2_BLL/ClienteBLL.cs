using C3_DAL;
using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2_BLL
{
    public class ClienteBLL
    {
        private ClienteDAL clienteDAL = new ClienteDAL();

        public ClienteBLL()
        {
            clienteDAL = new ClienteDAL();
        }

        // Registrar cliente
        public bool RegistrarCliente(Cliente cliente)
        {
            try
            {
                ValidarCamposObligatorios(cliente);

                ValidarFormatoEmail(cliente.Email);
                ValidarFormatoTelefono(cliente.Telefono);

                if (ExisteClientePorEmailOTelefono(cliente.Email, cliente.Telefono))
                {
                    throw new Exception("Ya existe un cliente con el mismo email o teléfono.");
                }

                cliente.Activo = true;
                cliente.FechaIngreso = DateTime.Now;

                clienteDAL.Agregar(cliente);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el cliente: " + ex.Message);
            }
        }

        // CAMPOS DE VALIDACION PRIVADA

        private void ValidarCamposObligatorios(Cliente cliente)
        {
            List<string> camposVacios = new List<string>();

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                camposVacios.Add("Nombre");

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
                camposVacios.Add("Apellido");

            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                camposVacios.Add("Teléfono");

            if (string.IsNullOrWhiteSpace(cliente.Email))
                camposVacios.Add("Email");

            if (camposVacios.Count > 0)
            {
                string mensaje = "Los siguientes campos son obligatorios: " + string.Join(", ", camposVacios);
                throw new Exception(mensaje);
            }
        }

        private void ValidarFormatoEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return; 

            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, patron))
            {
                throw new Exception("Ingrese un formato válido de correo electrónico.");
            }
        }

        private void ValidarFormatoTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return; 


            string patron = @"^[\d\s\-\(\)]+$";
            if (!Regex.IsMatch(telefono, patron))
            {
                throw new Exception("Ingrese un formato válido de teléfono.");
            }


            string soloNumeros = Regex.Replace(telefono, @"[^\d]", "");
            if (soloNumeros.Length < 8)
            {
                throw new Exception("El teléfono debe tener al menos 8 dígitos.");
            }
        }

        private bool ExisteClientePorEmailOTelefono(string email, string telefono)
        {
            List<Cliente> todosClientes = clienteDAL.Leer();

            return todosClientes.Any(c =>
                c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                c.Telefono.Equals(telefono, StringComparison.OrdinalIgnoreCase)
            );
        }

        private bool ExisteClientePorEmailOTelefonoExcluyendo(string email, string telefono, int idClienteActual)
        {
            List<Cliente> todosClientes = clienteDAL.Leer();

            return todosClientes.Any(c =>
                c.IdCliente != idClienteActual &&
                (c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                 c.Telefono.Equals(telefono, StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
