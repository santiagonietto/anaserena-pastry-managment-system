using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C4_ENTIDAD;

namespace C1_UI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                CargarDatosUsuario();
                ConfigurarMenuPorRol();
            }
        }



        private void ValidarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("LoginUI.aspx");
            }
        }
        private void CargarDatosUsuario()
        {
            try
            {
                if (Session["UsuarioNombre"] != null && Session["UsuarioRol"] != null)
                {
                    string nombreCompleto = Session["UsuarioNombre"].ToString();
                    string rol = Session["UsuarioRol"].ToString();

                    // Mostrar nombre y rol
                    lblNombreUsuario.Text = nombreCompleto;
                    lblRolUsuario.Text = rol;

                    // Obtener inicial del nombre
                    string inicial = nombreCompleto.Substring(0, 1).ToUpper();
                    lblInicialUsuario.Text = inicial;
                }
            }
            catch (Exception ex)
            {
                // Log error si es necesario
                lblNombreUsuario.Text = "Usuario";
                lblRolUsuario.Text = "Sin rol";
                lblInicialUsuario.Text = "U";
            }
        }

        private void ConfigurarMenuPorRol()
        {
            try
            {
                if (Session["Usuario"] != null)
                {
                    Usuarios usuario = (Usuarios)Session["Usuario"];

                    // Solo administradores pueden ver el módulo de Usuarios
                    if (usuario.EsAdministrador())
                    {
                        navUsuariosItem.Visible = true;
                    }
                    else
                    {
                        navUsuariosItem.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                // Por defecto ocultar usuarios si hay error
                navUsuariosItem.Visible = false;
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Decrementar contador de usuarios conectados
                if (Application["UsuariosConectados"] != null)
                {
                    int usuariosConectados = (int)Application["UsuariosConectados"];
                    if (usuariosConectados > 0)
                    {
                        Application["UsuariosConectados"] = usuariosConectados - 1;
                    }
                }

                // Limpiar sesión
                Session.Clear();
                Session.Abandon();

                // Redirigir al login
                Response.Redirect("LoginUI.aspx");
            }
            catch (Exception ex)
            {
                // Si hay error, forzar la salida
                Session.Clear();
                Response.Redirect("LoginUI.aspx");
            }
        }
    }
}