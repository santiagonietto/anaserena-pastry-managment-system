//using System;
//using System.Collections.Generic;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using C2_BLL;
//using C4_ENTIDAD;

//namespace C1_UI
//{
//    public partial class ClientesUI : System.Web.UI.Page
//    {
//        private ClienteBLL clienteBLL = new ClienteBLL();

//        /// <summary>
//        /// Se ejecuta al cargar la página
//        /// </summary>
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                CargarClientes();
//            }
//        }

//        /// <summary>
//        /// Carga la lista de clientes en el GridView
//        /// </summary>
//        private void CargarClientes()
//        {
//            try
//            {
//                //List<Cliente> listaClientes = clienteBLL.ObtenerTodosLosClientes();
//                //ClientesGV.DataSource = listaClientes;
//                ClientesGV.DataBind();
//            }
//            catch (Exception ex)
//            {
//                MostrarError("Error al cargar clientes: " + ex.Message);
//            }
//        }

//        /// <summary>
//        /// ✅ MÉTODO QUE FALTABA - Maneja los comandos Editar/Eliminar
//        /// </summary>
//        protected void ClientesGV_RowCommand(object sender, GridViewCommandEventArgs e)
//        {
//            try
//            {
//                int idCliente = Convert.ToInt32(e.CommandArgument);

//                if (e.CommandName == "Editar")
//                {
//                    // Redirigir a página de edición (crear después)
//                    Response.Redirect($"ClienteEditar.aspx?id={idCliente}");
//                }
//                else if (e.CommandName == "Eliminar")
//                {
//                    EliminarCliente(idCliente);
//                }
//            }
//            catch (Exception ex)
//            {
//                MostrarError("Error al procesar acción: " + ex.Message);
//            }
//        }

//        /// <summary>
//        /// Elimina un cliente
//        /// </summary>
//        private void EliminarCliente(int idCliente)
//        {
//            try
//            {
//               // if (clienteBLL.EliminarCliente(idCliente))
//                {
//                    MostrarMensaje("Cliente eliminado correctamente");
//                    CargarClientes(); // Recargar la lista
//                }
//            }
//            catch (Exception ex)
//            {
//                MostrarError(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Botón Nuevo Cliente
//        /// </summary>
//        protected void NuevoClienteBTN_Click(object sender, EventArgs e)
//        {
//            // Redirigir a página de nuevo cliente (crear después)
//            Response.Redirect("ClienteNuevo.aspx");
//        }

//        /// <summary>
//        /// Muestra mensaje de éxito
//        /// </summary>
//        private void MostrarMensaje(string mensaje)
//        {
//            lblMensaje.Text = mensaje;
//            lblMensaje.Visible = true;
//            lblError.Visible = false;
//        }

//        /// <summary>
//        /// Muestra mensaje de error
//        /// </summary>
//        private void MostrarError(string mensaje)
//        {
//            lblError.Text = mensaje;
//            lblError.Visible = true;
//            lblMensaje.Visible = false;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using C4_ENTIDAD;

namespace C1_UI
{
    public partial class ClientesUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientesMock();
            }
        }

        private void CargarClientesMock()
        {
            List<Cliente> clientesPrueba = new List<Cliente>
            {
                new Cliente
                {
                    IdCliente = 1,
                    Nombre = "María",
                    Apellido = "González",
                    Telefono = "555-0101",
                    Email = "maria.gonzalez@email.com"
                },
                new Cliente
                {
                    IdCliente = 2,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Telefono = "555-0102",
                    Email = "juan.perez@email.com"
                },
                new Cliente
                {
                    IdCliente = 3,
                    Nombre = "Ana",
                    Apellido = "Martínez",
                    Telefono = "555-0103",
                    Email = "ana.martinez@email.com"
                }
            };
            ClientesGV.DataSource = clientesPrueba;
            ClientesGV.DataBind();
        }

        protected void ClientesGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Editar")
                {
                    MostrarMensaje($"Editar cliente ID: {idCliente}");
                }
                else if (e.CommandName == "Eliminar")
                {
                    MostrarMensaje($"Cliente {idCliente} eliminado (simulación)");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error: " + ex.Message);
            }
        }

        protected void NuevoClienteBTN_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
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


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                Cliente nuevoCliente = new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                MostrarMensaje($"Cliente {nuevoCliente.Nombre} {nuevoCliente.Apellido} guardado exitosamente");


                PanelNuevoCliente.Visible = false;
                LimpiarFormulario();
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