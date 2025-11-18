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

        public List<Cliente> ObtenerTodosLosClientes()
        {
            try
            {
                return clienteDAL.Leer();
            } catch (Exception err)
            {
                throw new Exception($"Error al obtener la lista de clientes {err.Message}");
            }
        }

        public List<Cliente> BuscarClientes(string textoBusqueda)
        {
            try
            {
                if(string.IsNullOrEmpty(textoBusqueda))
                {
                    throw new Exception("Debe ingresar un criterio de busqueda");
                }

                List<Cliente> resultados = clienteDAL.Buscar(textoBusqueda);

                if(resultados.Count == 0)
                {
                    throw new Exception("Error, no se econtrar conincidencias!");
                }

                return resultados;
            }catch (Exception err)
            {
                throw new Exception($"Error, no se encontrar coincidencias! {err.Message}");
            }
        }

        public List<Domicilio> ObtenerDomicilioCLiente(int idDomicilio)
        {
            try
            {
                if(idDomicilio <= 0)
                {
                    throw new Exception("ID de domicilio invalido!");
                }

                return clienteDAL.ObtenerDomicilio(idDomicilio);
            } catch (Exception err)
            {
                throw new Exception($"Error al obtener domicilio: {err.Message}");
            }
        }

        public bool ModificarCliente(Cliente cliente)
        {
            try
            {
                if(cliente.IdCliente <= 0)
                {
                    throw new Exception("ID de  cliente invalido!");
                }

                ValidarCamposObligatorios(cliente);

                ValidarFormatoEmail(cliente.Email);
                ValidarFormatoTelefono(cliente.Telefono);

                if (ExisteClientePorEmailOTelefonoExcluyendo(cliente.Email, cliente.Telefono, cliente.IdCliente))
                {
                    throw new Exception("Ya existe un cliente con esa informacion!");
                }

                clienteDAL.Modificar(cliente);
                return true;
            } catch (Exception err)
            {
                throw new Exception($"Error al modificar el cliente: {err.Message}");
            }
        }

        public bool EliminarCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                {
                    throw new Exception("ID de cliente inválido.");
                }

                int cantidadVentas = clienteDAL.ConocerVenta(idCliente);
                if (cantidadVentas > 0)
                {
                    throw new Exception("No se puede eliminar este cliente porque está asociado a registros de venta.");
                }

                // Eliminar cliente
                clienteDAL.Eliminar(idCliente);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
        }

        public bool DesactivarCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                {
                    throw new Exception("ID de cliente inválido.");
                }

                bool resultado = clienteDAL.Desactivar(idCliente);

                if (!resultado)
                {
                    throw new Exception("No se pudo desactivar el cliente.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar cliente: " + ex.Message);
            }
        }

        public bool ReactivarCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                {
                    throw new Exception("ID de cliente inválido.");
                }

                bool resultado = clienteDAL.Reactivar(idCliente);

                if (!resultado)
                {
                    throw new Exception("No se pudo reactivar el cliente.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reactivar cliente: " + ex.Message);
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
