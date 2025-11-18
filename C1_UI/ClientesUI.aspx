<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientesUI.aspx.cs" Inherits="C1_UI.ClientesUI" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Clientes</title>
    

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" 
          rel="stylesheet" />
    

    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css" 
          rel="stylesheet" />
    
    <style>
        body {
            padding: 20px;
            background-color: #f5f5f5;
        }
        .container-custom {
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .btn-accion {
            margin: 0 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-custom">

            <div class="mb-4">
                <asp:Label ID="GestionClientesLBL" runat="server" 
                           Text="Gestión de Clientes" 
                           CssClass="h2 d-block mb-2"></asp:Label>
                <asp:Label ID="AdministrarClienteLBL" runat="server" 
                           Text="Administre los clientes del sistema" 
                           CssClass="text-muted"></asp:Label>
            </div>


            <div class="mb-3">
                <asp:Button ID="NuevoClienteBTN" runat="server" 
                            Text="+ Nuevo Cliente" 
                            CssClass="btn btn-primary" 
                            OnClick="NuevoClienteBTN_Click" />
            </div>


            <asp:Label ID="lblMensaje" runat="server" 
                       CssClass="alert alert-success d-block" 
                       Visible="false"></asp:Label>
            <asp:Label ID="lblError" runat="server" 
                       CssClass="alert alert-danger d-block" 
                       Visible="false"></asp:Label>


            <div class="card">
                <div class="card-header bg-light">
                    <asp:Label ID="ListaClientesLBL" runat="server" 
                               Text="Lista de Clientes" 
                               CssClass="h5 mb-0"></asp:Label>
                </div>
                <div class="card-body">

                    <asp:GridView ID="ClientesGV" runat="server" 
                                  CssClass="table table-striped table-hover" 
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
                                    <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandName="Editar" 
                                                    CommandArgument='<%# Eval("IdCliente") %>'
                                                    CssClass="btn btn-sm btn-warning btn-accion"
                                                    ToolTip="Editar cliente">
                                        <i class="bi bi-pencil-square"></i> Editar
                                    </asp:LinkButton>
                                    
                                    <asp:LinkButton ID="btnEliminar" runat="server" 
                                                    CommandName="Eliminar" 
                                                    CommandArgument='<%# Eval("IdCliente") %>'
                                                    CssClass="btn btn-sm btn-danger btn-accion"
                                                    ToolTip="Eliminar cliente"
                                                    OnClientClick="return confirm('¿Está seguro de eliminar este cliente?');">
                                        <i class="bi bi-trash"></i> Eliminar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                        <EmptyDataTemplate>
                            <div class="alert alert-info">
                                No hay clientes registrados. Haga clic en "Nuevo Cliente" para agregar uno.
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>