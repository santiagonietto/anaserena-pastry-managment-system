using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C2_BLL;
using C4_ENTIDAD;

namespace C1_UI
{
    public partial class Login : System.Web.UI.Page
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                Session.Abandon();

                if (Session["Usuario"] != null)
                {
                    Response.Redirect("DashboardUI.aspx");
                }
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
 
                string username = txtUsuario.Text.Trim();
                string password = txtPassword.Text;


                Usuarios usuario = usuarioBLL.IniciarSesion(username, password);

                if (usuario != null)
                {
                    // Guardar usuario en sesión
                    Session["Usuario"] = usuario;
                    Session["UsuarioId"] = usuario.IdUsuario;
                    Session["UsuarioNombre"] = usuario.NombreCompleto;
                    Session["UsuarioRol"] = usuario.Rol;
                    Session["Username"] = usuario.Username;

                    if (Application["UsuariosConectados"] == null)
                    {
                        Application["UsuariosConectados"] = 0;
                    }
                    Application["UsuariosConectados"] = (int)Application["UsuariosConectados"] + 1;

                    if (usuario.EsAdministrador())
                    {
                        Response.Redirect("DashboardUI.aspx");
                    }
                    else
                    {
                        Response.Redirect("VentasUI.aspx"); 
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}