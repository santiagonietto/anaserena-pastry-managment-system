<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductosUI.aspx.cs" Inherits="C1_UI.ProductosUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/estilos.css" rel="stylesheet" type="text/css" />
    <link href="CSS/usuarios.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="productos-container">
        <div class="page-header">
            <div>
                <h2>Gestión de Productos</h2>
                <p>Administre el catálogo de productos</p>
            </div>

            <asp:Button ID="btnNuevoProducto" runat="server" Text="+ Nuevo Producto" CssClass="btn-primary" OnClick="btnNuevoProducto_Click" />
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="pnlFormulario" runat="server" Visible="false" CssClass="form-panel">
            <div class="form-header">
                <h3><asp:Label ID="lblTituloFormulario" runat="server" Text="Nuevo Producto"></asp:Label></h3>
                <asp:LinkButton ID="btnCerrarFormulario" runat="server" CssClass="close-btn" OnClick="btnCerrarFormulario_Click" CausesValidation="false">
                    <i class="fas fa-times"></i>
                </asp:LinkButton>
            </div>

            <div class="form-body">
                <asp:HiddenField ID="hfIdProducto" runat="server" Value="0" />
                
                <div class="form-row">
                    <div class="form-group">
                        <label>Código *</label>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" placeholder="Ej: P001"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Nombre *</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Torta de Chocolate"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label>Descripción</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Descripción detallada del producto"></asp:TextBox>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label>Precio *</label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej: 25.00" step="0.01"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Categoría *</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccione Categoría --</asp:ListItem>
                            <asp:ListItem Value="Tortas">Tortas</asp:ListItem>
                            <asp:ListItem Value="Cupcakes">Cupcakes</asp:ListItem>
                            <asp:ListItem Value="Galletas">Galletas</asp:ListItem>
                            <asp:ListItem Value="Pies">Pies</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Stock</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" placeholder="Cantidad en stock"></asp:TextBox>
                    </div>
                </div>

                <div class="form-actions">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-secondary" OnClick="btnCerrarFormulario_Click" CausesValidation="false" />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Producto" CssClass="btn-success" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </asp:Panel>

        <div class="users-section">
            <h3 class="section-title">Lista de Productos</h3>
            
            <div class="table-container">
                <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="users-table" 
                    OnRowCommand="gvProductos_RowCommand" DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                        CommandArgument='<%# Eval("Id") %>' CssClass="btn-action btn-edit" ToolTip="Editar">
                                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" 
                                        CommandArgument='<%# Eval("Id") %>' CssClass="btn-action btn-delete" 
                                        OnClientClick="return confirm('¿Está seguro de eliminar este producto? Esto lo ocultará del catálogo.');" ToolTip="Eliminar">
                                        <i class="fas fa-trash"></i>
                                    </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="empty-state">
                            <i class="fas fa-box fa-3x"></i>
                            <p>No hay productos registrados en el catálogo.</p>
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