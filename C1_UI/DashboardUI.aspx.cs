using C1_UI;
using C2_BLL;
using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace C1_UI
{
    public partial class DashboardUI : System.Web.UI.Page
    {
        private DashboardBLL dashboardBLL = new DashboardBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                CargarEstadisticas();
                ConfigurarAccesosPorRol();
            }
        }

        /// <summary>
        /// Valida que exista una sesión activa
        /// </summary>
        private void ValidarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("LoginUI.aspx");
            }
        }

        /// <summary>
        /// Carga todas las estadísticas del dashboard desde la capa de negocio
        /// </summary>
        private void CargarEstadisticas()
        {
            try
            {
                // Total de clientes
                lblTotalClientes.Text = dashboardBLL.ObtenerTotalClientes().ToString();

                // Total de productos
                lblTotalProductos.Text = dashboardBLL.ObtenerTotalProductos().ToString();

                // Total de ventas
                lblTotalVentas.Text = dashboardBLL.ObtenerTotalVentas().ToString();

                // Ventas del día de hoy
                lblVentasHoy.Text = "$" + dashboardBLL.ObtenerVentasDelDia().ToString("N2");

                // Última actualización (fecha actual)
                lblUltimaActualizacion.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                // En caso de error, mostrar valores por defecto
                lblTotalClientes.Text = "0";
                lblTotalProductos.Text = "0";
                lblTotalVentas.Text = "0";
                lblVentasHoy.Text = "$0.00";
                lblUltimaActualizacion.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // Opcional: Mostrar mensaje de error al usuario
                // lblMensajeError.Text = "Error al cargar estadísticas: " + ex.Message;
            }
        }

        /// <summary>
        /// Configura la visibilidad de accesos rápidos según el rol del usuario
        /// </summary>
        private void ConfigurarAccesosPorRol()
        {
            try
            {
                if (Session["Usuario"] != null)
                {
                    Usuarios usuario = (Usuarios)Session["Usuario"];

                    // Solo administradores ven el acceso a Usuarios
                    if (!usuario.EsAdministrador())
                    {
                        liUsuarios.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                // Por seguridad, ocultar el acceso si hay algún error
                liUsuarios.Visible = false;
            }
        }
    }
}