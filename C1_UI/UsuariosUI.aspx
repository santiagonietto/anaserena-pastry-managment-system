<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="C1_UI.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/estilos.css" rel="stylesheet" type="text/css" />
    <link href="CSS/usuarios.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="usuarios-container">
        <div class="page-header">
            <div>
                <h2>Gestión de Usuarios</h2>
                <p>Administre los usuarios del sistema</p>
            </div>

            <asp:Button ID="btnNuevoUsuario" runat="server" Text="+ Nuevo Usuario" CssClass="btn-primary" OnClick="btnNuevoUsuario_Click" />
        </div>

        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="form-panel">
            <div class="form-header">
                <h3><asp:Label ID="lblTituloFormulario" runat="server" Text="Nuevo Usuario"></asp:Label></h3>
                <asp:LinkButton ID="btnCerrarFormulario" runat="server" CssClass="close-btn" OnClick="btnCerrarFormulario_Click">
                    <i class="fas fa-times"></i>
                </asp:LinkButton>
            </div>

            <div class="form-body">
                <asp:HiddenField ID="hfIdUsuario" runat="server" Value="0" />
                
                <div class="form-row">
                    <div class="form-group">
                        <label>Usuario *</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Ingrese nombre de usuario"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Nombre Completo *</label>
                        <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control" placeholder="Ingrese nombre completo"></asp:TextBox>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label>Rol *</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione Rol --</asp:ListItem>
                            <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                            <asp:ListItem Value="Empleado">Empleado</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label ID="lblPasswordLabel" runat="server" Text="Contraseña *"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Mínimo 10 caracteres"></asp:TextBox>
                    </div>
                </div>

                <div class="form-actions">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="btn-success" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </asp:Panel>

        <div class="users-section">
            <h3 class="section-title">Lista de Usuarios</h3>
            
            <div class="table-container">
                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="users-table" 
                    OnRowCommand="gvUsuarios_RowCommand" DataKeyNames="IdUsuario">
                    <Columns>
                        <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                        <asp:BoundField DataField="Username" HeaderText="Usuario" />
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <%# ObtenerPrimerNombre(Eval("NombreCompleto").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido">
                            <ItemTemplate>
                                <%# ObtenerApellido(Eval("NombreCompleto").ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rol">
                            <ItemTemplate>
                                <span class='<%# "badge badge-" + (Eval("Rol").ToString() == "Administrador" ? "admin" : "employee") %>'>
                                    <%# Eval("Rol") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# "status-badge status-" + (Convert.ToBoolean(Eval("Estado")) ? "active" : "inactive") %>'>
                                    <%# Convert.ToBoolean(Eval("Estado")) ? "Activo" : "Inactivo" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                        CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn-action btn-edit" ToolTip="Editar">
                                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" 
                                        CommandArgument='<%# Eval("IdUsuario") %>' CssClass="btn-action btn-delete" 
                                        OnClientClick="return confirm('¿Está seguro de eliminar este usuario?');" ToolTip="Eliminar">
                                        <i class="fas fa-trash"></i>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="empty-state">
                            <i class="fas fa-users fa-3x"></i>
                            <p>No hay usuarios registrados</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

    <script>
        // Ocultar mensajes automáticamente después de 5 segundos
        window.onload = function () {
            var alert = document.querySelector('.alert');
            if (alert) {
                setTimeout(function () {
                    alert.style.display = 'none';
                }, 5000);
            }
        };
    </script>
</asp:Content>