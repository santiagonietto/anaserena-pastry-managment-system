<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardUI.aspx.cs" Inherits="C1_UI.DashboardUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/estilos.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/dashboard.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dashboard-container">
        <!-- Encabezado -->
        <div class="dashboard-header">
            <h2>Dashboard</h2>
            <p>Bienvenido al sistema de gestión de ventas</p>
        </div>

        <!-- Tarjetas de estadísticas -->
        <div class="stats-grid">
            <!-- Total Clientes -->
            <div class="stat-card">
                <div class="stat-card-header">
                    <span class="stat-card-title">Total Clientes</span>
                    <div class="stat-card-icon clientes">
                        <i class="bi bi-people"></i>
                    </div>
                </div>
                <div class="stat-card-value">
                    <asp:Label ID="lblTotalClientes" runat="server" Text="0"></asp:Label>
                </div>
                <div class="stat-card-label">Clientes registrados</div>
            </div>

            <!-- Total Productos -->
            <div class="stat-card">
                <div class="stat-card-header">
                    <span class="stat-card-title">Total Productos</span>
                    <div class="stat-card-icon productos">
                        <i class="bi bi-box"></i>
                    </div>
                </div>
                <div class="stat-card-value">
                    <asp:Label ID="lblTotalProductos" runat="server" Text="0"></asp:Label>
                </div>
                <div class="stat-card-label">Productos en catálogo</div>
            </div>

            <!-- Total Ventas -->
            <div class="stat-card">
                <div class="stat-card-header">
                    <span class="stat-card-title">Total Ventas</span>
                    <div class="stat-card-icon ventas">
                        <i class="bi bi-cart"></i>
                    </div>
                </div>
                <div class="stat-card-value">
                    <asp:Label ID="lblTotalVentas" runat="server" Text="0"></asp:Label>
                </div>
                <div class="stat-card-label">Ventas realizadas</div>
            </div>

            <!-- Ventas Hoy -->
            <div class="stat-card">
                <div class="stat-card-header">
                    <span class="stat-card-title">Ventas Hoy</span>
                    <div class="stat-card-icon hoy">
                        <i class="bi bi-currency-dollar"></i>
                    </div>
                </div>
                <div class="stat-card-value">
                    <asp:Label ID="lblVentasHoy" runat="server" Text="$0.00"></asp:Label>
                </div>
                <div class="stat-card-label">Ventas del día</div>
            </div>
        </div>

        <!-- Información del Sistema -->
        <div class="info-section">
            <h3>Información del Sistema</h3>
            <div class="info-item">
                <span class="info-label">Versión:</span>
                <span class="info-value">1.0.0</span>
            </div>
            <div class="info-item">
                <span class="info-label">Última actualización:</span>
                <span class="info-value">
                    <asp:Label ID="lblUltimaActualizacion" runat="server"></asp:Label>
                </span>
            </div>
            <div class="info-item">
                <span class="info-label">Estado:</span>
                <span class="info-value success">Operativo</span>
            </div>
        </div>

        <!-- Accesos Rápidos -->
        <div class="quick-access">
            <h3>Accesos Rápidos</h3>
            <p style="color: #7f8c8d; font-size: 14px; margin-bottom: 15px;">
                Utilice el menú lateral para navegar entre las diferentes secciones del sistema.
            </p>
            <ul>
                <li>
                    <a href="ClientesUI.aspx">
                        <i class="bi bi-people"></i>
                        <span>Gestionar clientes</span>
                    </a>
                </li>
                <li>
                    <a href="ProductosUI.aspx">
                        <i class="bi bi-box"></i>
                        <span>Administrar productos</span>
                    </a>
                </li>
                <li>
                    <a href="VentasUI.aspx">
                        <i class="bi bi-cart"></i>
                        <span>Registrar ventas</span>
                    </a>
                </li>
                <li runat="server" id="liUsuarios">
                    <a href="UsuariosUI.aspx">
                        <i class="bi bi-person-gear"></i>
                        <span>Control de usuarios</span>
                    </a>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>