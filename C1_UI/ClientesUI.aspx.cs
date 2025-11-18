using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2_BLL;
using C4_ENTIDAD;

namespace C1_UI
{
    public partial class ClientesUI : System.Web.UI.Page
    {
        private ClienteBLL clienteBLL = new ClienteBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
        }

        private void CargarClientes()
        {
            try
            {
                List<Cliente> listaClientes = clienteBLL.ObtenerTodosLosClientes();
                ClientesGV.DataSource = listaClientes;
                ClientesGV.DataBind();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar clientes: " + ex.Message);
            }
        }

        protected void ClientesGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Editar")
                {
                    CargarClienteParaEditar(idCliente);
                }
                else if (e.CommandName == "Eliminar")
                {
                    EliminarCliente(idCliente);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al procesar acción: " + ex.Message);
            }
        }

        // ✅ NUEVO: Cargar cliente para editar
        private void CargarClienteParaEditar(int idCliente)
        {
            try
            {
                List<Cliente> clientes = clienteBLL.ObtenerTodosLosClientes();
                Cliente cliente = clientes.Find(c => c.IdCliente == idCliente);

                if (cliente != null)
                {
                    // Guardar el ID en el campo oculto
                    hdnIdCliente.Value = cliente.IdCliente.ToString();

                    // Llenar los campos con los datos del cliente
                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtTelefono.Text = cliente.Telefono;
                    txtEmail.Text = cliente.Email;

                    // Cambiar el título del modal
                    lblTituloModal.Text = "Editar Cliente";
                    btnGuardar.Text = "Actualizar";

                    // Mostrar el modal
                    PanelNuevoCliente.Visible = true;
                    lblMensaje.Visible = false;
                    lblError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar cliente: " + ex.Message);
            }
        }

        private void EliminarCliente(int idCliente)
        {
            try
            {
                if (clienteBLL.EliminarCliente(idCliente))
                {
                    MostrarMensaje("Cliente eliminado correctamente");
                    CargarClientes();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        // ✅ Mostrar modal para nuevo cliente
        protected void NuevoClienteBTN_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloModal.Text = "Nuevo Cliente";
            btnGuardar.Text = "Guardar";
            hdnIdCliente.Value = "0";
            PanelNuevoCliente.Visible = true;
            lblMensaje.Visible = false;
            lblError.Visible = false;
        }

        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            PanelNuevoCliente.Visible = false;
            LimpiarFormulario();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            PanelNuevoCliente.Visible = false;
            LimpiarFormulario();
        }

        // ✅ MODIFICADO: Guardar o Actualizar según el ID
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(hdnIdCliente.Value);

                Cliente cliente = new Cliente
                {
                    IdCliente = idCliente,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                if (idCliente == 0)
                {
                    // Nuevo cliente
                    cliente.Activo = true;
                    cliente.FechaIngreso = DateTime.Now;

                    if (clienteBLL.RegistrarCliente(cliente))
                    {
                        MostrarMensaje($"Cliente {cliente.Nombre} {cliente.Apellido} guardado exitosamente");
                    }
                }
                else
                {
                    // Editar cliente existente
                    // Obtener datos completos del cliente para no perder Activo y FechaIngreso
                    List<Cliente> clientes = clienteBLL.ObtenerTodosLosClientes();
                    Cliente clienteExistente = clientes.Find(c => c.IdCliente == idCliente);

                    cliente.Activo = clienteExistente.Activo;
                    cliente.FechaIngreso = clienteExistente.FechaIngreso;

                    if (clienteBLL.ModificarCliente(cliente))
                    {
                        MostrarMensaje($"Cliente {cliente.Nombre} {cliente.Apellido} actualizado exitosamente");
                    }
                }

                PanelNuevoCliente.Visible = false;
                LimpiarFormulario();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MostrarError("Error al guardar: " + ex.Message);
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            hdnIdCliente.Value = "0";
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
            lblError.Visible = false;
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
            lblMensaje.Visible = false;
        }
    }
}