using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2_BLL;
using C4_ENTIDAD;

namespace C1_UI
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si hay sesión activa
                if (Session["Usuario"] == null)
                {
                    Response.Redirect("LoginUI.aspx");
                    return;
                }

                // Verificar que sea administrador
                Usuarios usuarioActual = (Usuarios)Session["Usuario"];
                if (!usuarioActual.EsAdministrador())
                {
                    Response.Redirect("DashboardUI.aspx");
                    return;
                }

                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                var usuarios = usuarioBLL.ObtenerTodosLosUsuarios();
                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar usuarios: " + ex.Message, "error");
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloFormulario.Text = "Nuevo Usuario";
            lblPasswordLabel.Text = "Contraseña *";
            txtPassword.Attributes["placeholder"] = "Mínimo 10 caracteres";
            pnlFormulario.Visible = true;
            hfIdUsuario.Value = "0";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = Convert.ToInt32(hfIdUsuario.Value);

                if (idUsuario == 0)
                {
                    // Crear nuevo usuario
                    Usuarios nuevoUsuario = new Usuarios
                    {
                        Username = txtUsername.Text.Trim(),
                        NombreCompleto = txtNombreCompleto.Text.Trim(),
                        Password = txtPassword.Text,
                        Rol = ddlRol.SelectedValue,
                        Estado = true,
                        IntentosFallidos = 0,
                        FechaIngreso = DateTime.Now,
                        IdDomicilio = 0
                    };

                    usuarioBLL.RegistrarUsuario(nuevoUsuario);
                    MostrarMensaje("Usuario registrado exitosamente", "success");
                }
                else
                {
                    // Modificar usuario existente
                    Usuarios usuarioExistente = usuarioBLL.ObtenerTodosLosUsuarios()
                        .Find(u => u.IdUsuario == idUsuario);

                    if (usuarioExistente != null)
                    {
                        usuarioExistente.Username = txtUsername.Text.Trim();
                        usuarioExistente.NombreCompleto = txtNombreCompleto.Text.Trim();
                        usuarioExistente.Rol = ddlRol.SelectedValue;

                        usuarioBLL.ModificarUsuario(usuarioExistente);

                        // Cambiar contraseña si se proporcionó una nueva
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            usuarioBLL.CambiarPassword(idUsuario, txtPassword.Text);
                        }

                        MostrarMensaje("Usuario modificado exitosamente", "success");
                    }
                }

                pnlFormulario.Visible = false;
                CargarUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "error");
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                try
                {
                    var usuarios = usuarioBLL.ObtenerTodosLosUsuarios();
                    Usuarios usuario = usuarios.Find(u => u.IdUsuario == idUsuario);

                    if (usuario != null)
                    {
                        hfIdUsuario.Value = usuario.IdUsuario.ToString();
                        txtUsername.Text = usuario.Username;
                        txtNombreCompleto.Text = usuario.NombreCompleto;
                        ddlRol.SelectedValue = usuario.Rol;
                        txtPassword.Text = "";

                        lblTituloFormulario.Text = "Editar Usuario";
                        lblPasswordLabel.Text = "Nueva Contraseña (dejar vacío para mantener actual)";
                        txtPassword.Attributes["placeholder"] = "Dejar vacío para no cambiar";
                        pnlFormulario.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar usuario: " + ex.Message, "error");
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    Usuarios usuarioActual = (Usuarios)Session["Usuario"];
                    usuarioBLL.EliminarUsuario(idUsuario, usuarioActual.IdUsuario);
                    MostrarMensaje("Usuario eliminado exitosamente", "success");
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message, "error");
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
            LimpiarFormulario();
        }

        protected void btnCerrarFormulario_Click(object sender, EventArgs e)
        {
            pnlFormulario.Visible = false;
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfIdUsuario.Value = "0";
            txtUsername.Text = "";
            txtNombreCompleto.Text = "";
            txtPassword.Text = "";
            ddlRol.SelectedIndex = 0;
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;

            // Remover clases anteriores
            pnlMensaje.CssClass = "alert alert-" + tipo;
        }

        // Métodos auxiliares para el GridView
        protected string ObtenerPrimerNombre(string nombreCompleto)
        {
            if (string.IsNullOrEmpty(nombreCompleto)) return "";
            string[] partes = nombreCompleto.Split(' ');
            return partes.Length > 0 ? partes[0] : "";
        }

        protected string ObtenerApellido(string nombreCompleto)
        {
            if (string.IsNullOrEmpty(nombreCompleto)) return "";
            string[] partes = nombreCompleto.Split(' ');
            return partes.Length > 1 ? string.Join(" ", partes, 1, partes.Length - 1) : "";
        }
    }
}