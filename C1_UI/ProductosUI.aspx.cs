// File: C1_UI/ProductosUI.aspx.cs

using C2_BLL;
using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace C1_UI
{
    // *** CLASE RENOMBRADA ***
    public partial class ProductosUI : System.Web.UI.Page
    {
        private ProductoBLL productoBLL = new ProductoBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            try
            {
                gvProductos.DataSource = productoBLL.ObtenerTodosLosProductos();
                gvProductos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, "error");
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
    
            int idProducto = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                CargarFormularioParaEdicion(idProducto);
            }
            else if (e.CommandName == "Eliminar")
            {
                EliminarProducto(idProducto);
            }
        }

        protected void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloFormulario.Text = "Nuevo Producto";
            pnlFormulario.Visible = true;
            pnlMensaje.Visible = false;
        }

        protected void btnCerrarFormulario_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
            LimpiarFormulario();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                producto.Id = Convert.ToInt32(hfIdProducto.Value);
                producto.Codigo = txtCodigo.Text.Trim();
                producto.Nombre = txtNombre.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();

                if (decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    producto.Precio = precio;
                }
                else
                {
                    throw new Exception("El precio debe ser un número válido.");
                }

                producto.Categoria = ddlCategoria.SelectedValue;


                if (decimal.TryParse(txtStock.Text, out decimal stock))
                {
                    producto.Stock = stock;
                }
                else
                {
                    throw new Exception("El stock debe ser un número válido.");
                }

                if (producto.Id == 0)
                {
     
                    if (productoBLL.RegistrarProducto(producto))
                    {
                        MostrarMensaje("Producto registrado exitosamente.", "success");
                    }
                }
                else
                {

                    if (productoBLL.ModificarProducto(producto))
                    {
                        MostrarMensaje("Producto modificado exitosamente.", "success");
                    }
                }

                pnlFormulario.Visible = false;
                LimpiarFormulario();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "error");
            }
        }

        private void LimpiarFormulario()
        {
            hfIdProducto.Value = "0";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            ddlCategoria.SelectedIndex = 0;
            txtStock.Text = "0";
        }

        private void CargarFormularioParaEdicion(int idProducto)
        {
            try
            {
                Producto producto = productoBLL.ObtenerPorId(idProducto);
                if (producto != null)
                {
                    hfIdProducto.Value = producto.Id.ToString();
                    txtCodigo.Text = producto.Codigo;
                    txtNombre.Text = producto.Nombre;
                    txtDescripcion.Text = producto.Descripcion;
                    txtPrecio.Text = producto.Precio.ToString("0.00");
                    ddlCategoria.SelectedValue = producto.Categoria;
                    txtStock.Text = producto.Stock.ToString();

                    lblTituloFormulario.Text = "Editar Producto: " + producto.Nombre;
                    pnlFormulario.Visible = true;
                    pnlMensaje.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar datos para edición: " + ex.Message, "error");
            }
        }

        private void EliminarProducto(int idProducto)
        {
            try
            {
                if (productoBLL.EliminarProducto(idProducto))
                {
                    MostrarMensaje("Producto eliminado (ocultado del catálogo) exitosamente.", "success");
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar producto: " + ex.Message, "error");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;

            // Define la clase CSS basada en el tipo (ej: success, error)
            string cssClass = "alert alert-info";
            if (tipo.ToLower() == "success")
                cssClass = "alert alert-success";
            else if (tipo.ToLower() == "error")
                cssClass = "alert alert-danger";

            pnlMensaje.CssClass = cssClass;
        }
    }
}