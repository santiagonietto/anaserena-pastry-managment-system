using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2_BLL;
using C4_ENTIDAD;

namespace C1_UI
{
    public partial class VentasUI : System.Web.UI.Page
    {
        private VentaBLL ventaBLL = new VentaBLL();
        private ClienteBLL clienteBLL = new ClienteBLL();
        private ProductoBLL productoBLL = new ProductoBLL();


        private List<DetalleVenta> DetallesVenta
        {
            get
            {
                if (Session["DetallesVenta"] == null)
                {
                    Session["DetallesVenta"] = new List<DetalleVenta>();
                }
                return (List<DetalleVenta>)Session["DetallesVenta"];
            }
            set
            {
                Session["DetallesVenta"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarClientes();
                CargarProductos();
                CargarHistorialVentas();
                ActualizarDetallesVenta();
            }
        }

        private void CargarClientes()
        {
            try
            {
                List<Cliente> clientes = clienteBLL.ObtenerTodosLosClientes();

                ddlCliente.Items.Clear();
                ddlCliente.Items.Add(new ListItem("Seleccione un cliente", "0"));

                foreach (var cliente in clientes)
                {
                    if (cliente.Activo)
                    {
                        string texto = $"{cliente.Nombre} {cliente.Apellido} - {cliente.Email}";
                        ddlCliente.Items.Add(new ListItem(texto, cliente.IdCliente.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar clientes: " + ex.Message);
            }
        }

        private void CargarProductos()
        {
            try
            {
                List<Producto> productos = productoBLL.ObtenerProductosDisponibles();

                ddlProducto.Items.Clear();
                ddlProducto.Items.Add(new ListItem("Seleccione un producto", "0"));

                foreach (var producto in productos)
                {
                    string texto = $"{producto.Nombre} - ${producto.Precio:N2} (Stock: {producto.Stock})";
                    ddlProducto.Items.Add(new ListItem(texto, producto.Id.ToString()));
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar productos: " + ex.Message);
            }
        }

        private void CargarHistorialVentas()
        {
            try
            {
                List<Venta> ventas = ventaBLL.ObtenerTodasLasVentas();
                gvHistorialVentas.DataSource = ventas;
                gvHistorialVentas.DataBind();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar historial: " + ex.Message);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = Convert.ToInt32(ddlProducto.SelectedValue);
                int cantidad = Convert.ToInt32(txtCantidad.Text);

                if (idProducto == 0)
                {
                    MostrarError("Debe seleccionar un producto");
                    return;
                }

                if (cantidad <= 0)
                {
                    MostrarError("La cantidad debe ser mayor a cero");
                    return;
                }


                ventaBLL.ValidarAgregarProducto(idProducto, cantidad);


                Producto producto = productoBLL.ObtenerPorId(idProducto);


                DetalleVenta detalleExistente = DetallesVenta.Find(d => d.IdProducto == idProducto);

                if (detalleExistente != null)
                {

                    detalleExistente.Cantidad += cantidad;
                    detalleExistente.CalcularSubtotal();
                }
                else
                {

                    DetalleVenta nuevoDetalle = new DetalleVenta
                    {
                        IdProducto = producto.Id,
                        NombreProducto = producto.Nombre,
                        CodigoProducto = producto.Codigo,
                        Cantidad = cantidad,
                        PrecioUnitario = producto.Precio
                    };
                    nuevoDetalle.CalcularSubtotal();

                    DetallesVenta.Add(nuevoDetalle);
                }


                ActualizarDetallesVenta();


                ddlProducto.SelectedIndex = 0;
                txtCantidad.Text = "1";

                MostrarMensaje("Producto agregado correctamente");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }


        protected void gvDetallesVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EliminarDetalle")
                {
                    int idProducto = Convert.ToInt32(e.CommandArgument);
                    DetallesVenta.RemoveAll(d => d.IdProducto == idProducto);
                    ActualizarDetallesVenta();
                    MostrarMensaje("Producto eliminado del detalle");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al eliminar producto: " + ex.Message);
            }
        }

        private void ActualizarDetallesVenta()
        {
            gvDetallesVenta.DataSource = DetallesVenta;
            gvDetallesVenta.DataBind();

            // Calcular total
            decimal total = 0;
            foreach (var detalle in DetallesVenta)
            {
                total += detalle.Subtotal;
            }
            lblTotal.Text = total.ToString("N2");
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {

                if (DetallesVenta.Count == 0)
                {
                    MostrarError("Debe agregar al menos un producto a la venta");
                    return;
                }

                Venta nuevaVenta = new Venta
                {
                    Fecha = DateTime.Now,
                    IdUsuario = Convert.ToInt32(Session["UsuarioId"]),
                    IdCliente = Convert.ToInt32(ddlCliente.SelectedValue),
                    Detalles = new List<DetalleVenta>(DetallesVenta)
                };

                nuevaVenta.CalcularTotal();


                if (ventaBLL.RegistrarVenta(nuevaVenta))
                {
                    MostrarMensaje("¡Venta registrada correctamente!");


                    LimpiarFormulario();

                    CargarHistorialVentas();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
        private void LimpiarFormulario()
        {
            ddlCliente.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
            txtCantidad.Text = "1";
            DetallesVenta.Clear();
            ActualizarDetallesVenta();
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