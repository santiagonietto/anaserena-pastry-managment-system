<%@ Page Title="Gestión de Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VentasUI.aspx.cs" Inherits="C1_UI.VentasUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/estilos.css" rel="stylesheet" type="text/css" />    
    <link href="CSS/ventas.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 class="page-title">Gestión de Ventas</h1>
        <p class="page-subtitle">Registre nuevas ventas y consulte el historial</p>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>

    <div class="ventas-container">
        <div class="card">
            <h2 class="card-title">
                <i class="bi bi-cart-plus"></i>
                Nueva Venta
            </h2>

            <div class="form-group">
                <label class="form-label">Cliente</label>
                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select">
                    <asp:ListItem Value="0" Text="Seleccione un cliente"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label class="form-label">Agregar Productos</label>
                <div class="input-group">
                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select">
                        <asp:ListItem Value="0" Text="Seleccione un producto"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" 
                                 CssClass="form-control" placeholder="Cant." 
                                 min="1" Text="1"></asp:TextBox>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                CssClass="btn-primary" OnClick="btnAgregar_Click" />
                </div>
            </div>

            <div class="form-group">
                <label class="form-label">Detalles de la Venta</label>
                
                <asp:GridView ID="gvDetallesVenta" runat="server" 
                              CssClass="detalle-venta-table"
                              AutoGenerateColumns="False"
                              OnRowCommand="gvDetallesVenta_RowCommand"
                              ShowHeaderWhenEmpty="True"
                              EmptyDataText="No hay productos agregados">
                    <Columns>
                        <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." 
                                        DataFormatString="${0:N2}" />
                        <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" 
                                        DataFormatString="${0:N2}" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEliminar" runat="server" 
                                                CommandName="EliminarDetalle" 
                                                CommandArgument='<%# Eval("IdProducto") %>'
                                                CssClass="btn-danger"
                                                OnClientClick="return confirm('¿Eliminar este producto?');">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="total-container">
                <div class="total-label">Total:</div>
                <div class="total-amount">
                    $<asp:Label ID="lblTotal" runat="server" Text="0.00"></asp:Label>
                </div>
            </div>

            <div style="margin-top: 20px;">
                <asp:Button ID="btnRegistrarVenta" runat="server" Text="Registrar Venta" 
                            CssClass="btn-success" OnClick="btnRegistrarVenta_Click" />
            </div>
        </div>

        <div class="card">
            <h2 class="card-title">
                <i class="bi bi-clock-history"></i>
                Historial de Ventas
            </h2>

            <div class="table-responsive">
                <asp:GridView ID="gvHistorialVentas" runat="server" 
                              CssClass="table table-hover"
                              AutoGenerateColumns="False"
                              ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField DataField="IdVenta" HeaderText="ID" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" 
                                        DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="NombreCliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="Total" HeaderText="Total" 
                                        DataFormatString="${0:N2}" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="empty-state">
                            <i class="bi bi-inbox"></i>
                            <p>No hay ventas registradas</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>