<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="ClientesUI.aspx.cs" Inherits="C1_UI.ClientesUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css" rel="stylesheet" />
    <link href="/CSS/estilos.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container-custom">

    <!-- Encabezado con botón -->
    <div class="header-wrapper mb-4">
        <div>
            <asp:Label ID="GestionClientesLBL" runat="server" Text="Gestión de Clientes" CssClass="h2 d-block mb-2" />
            <asp:Label ID="AdministrarClienteLBL" runat="server" Text="Administre los clientes del sistema" CssClass="text-muted" />
        </div>
        <div>
            <asp:Button ID="NuevoClienteBTN" runat="server" Text="+ Nuevo Cliente" CssClass="btn btn-primary" OnClick="NuevoClienteBTN_Click" />
        </div>
    </div>

    <!-- Mensajes -->
    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-success d-block" Visible="false" />
    <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block" Visible="false" />

    <!-- GridView -->
    <div class="card">
        <div class="card-header bg-light">
            <asp:Label ID="ListaClientesLBL" runat="server" Text="Lista de Clientes" CssClass="h5 mb-0" />
        </div>
        <div class="card-body">
            <asp:GridView ID="ClientesGV" runat="server" CssClass="table table-striped table-hover"
                AutoGenerateColumns="False"
                OnRowCommand="ClientesGV_RowCommand"
                DataKeyNames="IdCliente">

                <Columns>
                    <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="action-buttons">
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    CommandName="Editar"
                                    CommandArgument='<%# Eval("IdCliente") %>'
                                    CssClass="btn-action btn-edit">
                                    <i class="bi bi-pencil-square"></i> <%-- Icono cuadrado y lleno --%>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CommandName="Eliminar"
                                    CommandArgument='<%# Eval("IdCliente") %>'
                                    CssClass="btn-action btn-delete"
                                    OnClientClick="return confirm('¿Está seguro de eliminar este cliente?');">
                                    <i class="bi bi-trash"></i> <%-- Icono de bote de basura --%>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay clientes registrados.</div>
                </EmptyDataTemplate>

            </asp:GridView>
        </div>
    </div>

</div>

<!-- Modal Nuevo Cliente -->
<asp:Panel ID="PanelNuevoCliente" runat="server" Visible="false" CssClass="modal-overlay">
    <div class="modal-content-custom">

        <div class="modal-header-custom">
            <h4 class="mb-0">
                <asp:Label ID="lblTituloModal" runat="server" Text="Nuevo Cliente"></asp:Label>
            </h4>
            <asp:Button ID="btnCerrarModal" runat="server" Text="✕" CssClass="btn-close"
                OnClick="btnCerrarModal_Click" CausesValidation="false" />
        </div>

        <div class="modal-body-custom">
            <asp:HiddenField ID="hdnIdCliente" runat="server" Value="0" />

            <div class="form-row-custom">
                <div class="form-field-custom">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                        ErrorMessage="Requerido" CssClass="text-danger small" Display="Dynamic" />
                </div>

                <div class="form-field-custom">
                    <label>Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido"
                        ErrorMessage="Requerido" CssClass="text-danger small" Display="Dynamic" />
                </div>
            </div>

            <div class="form-row-custom">
                <div class="form-field-custom">
                    <label>Teléfono</label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono"
                        ErrorMessage="Requerido" CssClass="text-danger small" Display="Dynamic" />
                </div>

                <div class="form-field-custom">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Requerido" CssClass="text-danger small" Display="Dynamic" />
                </div>
            </div>
        </div>

        <div class="modal-footer-custom">
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary"
                OnClick="btnCancelar_Click" CausesValidation="false" />
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary"
                OnClick="btnGuardar_Click" />
        </div>

    </div>
</asp:Panel>

</asp:Content>
