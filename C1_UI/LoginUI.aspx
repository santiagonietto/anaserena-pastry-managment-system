<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginUI.aspx.cs" Inherits="C1_UI.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Login - Pastelería Casera</title>
    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" 
          rel="stylesheet" />

    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css" 
          rel="stylesheet" />
    <link href="/CSS/estilos.css" rel="stylesheet" />
    <link href="/CSS/login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <!-- Logo y Branding -->
            <div class="logo-container">
                <div class="logo-icon">
                    <i class="bi bi-cake2"></i>
                </div>
                <div class="brand-name">Pastelería Casera</div>
                <div class="brand-subtitle">Ana Serena</div>
                <div class="system-name">Sistema de Gestión de Ventas</div>
            </div>

            <!-- Mensajes de Error/Éxito -->
            <asp:Label ID="lblError" runat="server" 
                       CssClass="alert alert-danger" 
                       Visible="false"></asp:Label>

            <!-- Formulario de Login -->
            <div class="mb-3">
                <label class="form-label">Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" 
                             CssClass="form-control" 
                             placeholder="Ingrese su usuario"
                             autocomplete="username"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" 
                             TextMode="Password" 
                             CssClass="form-control" 
                             placeholder="Ingrese su contraseña"
                             autocomplete="current-password"></asp:TextBox>
            </div>

            <!-- Botón de Login -->
            <asp:Button ID="btnIniciarSesion" runat="server" 
                        Text="Iniciar Sesión" 
                        CssClass="btn btn-login" 
                        OnClick="btnIniciarSesion_Click" />

            <!-- Olvidaste tu contraseña -->
            <div class="forgot-password">
                <asp:HyperLink ID="lnkOlvidePassword" runat="server" 
                               NavigateUrl="~/RecuperarPassword.aspx">
                    ¿Olvidaste tu contraseña?
                </asp:HyperLink>
            </div>

        </div>
    </form>
</body>
</html>