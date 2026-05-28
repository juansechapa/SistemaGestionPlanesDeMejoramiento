<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Iniciar sesión - SENA Planes de Mejoramiento</title>
    <link href="<%= ResolveUrl("~/vista/CSS/loginEstilos.css") %>" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrapper">
            <div class="login-card">
                <div class="login-header">
                    <div class="logo" aria-hidden="true">
                        <svg width="40" height="40" viewBox="0 0 40 40" fill="none">
                            <rect width="40" height="40" rx="10" fill="#2d8a4e" />
                            <path d="M11 27V15L20 9L29 15V27L20 33L11 27Z" stroke="white" stroke-width="1.8" stroke-linejoin="round" />
                            <circle cx="20" cy="21" r="3.5" fill="white" />
                        </svg>
                    </div>
                    <h1>Planes de Mejoramiento</h1>
                    <p class="subtitle">Ingrese sus credenciales institucionales para acceder al sistema</p>
                </div>

                <div class="form-group">
                    <label for="<%= txtUsuario.ClientID %>">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" autocomplete="username" placeholder="Ej. juan.perez"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="<%= txtPassword.ClientID %>">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" autocomplete="current-password" placeholder="Ingrese su contraseña"></asp:TextBox>
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Iniciar sesión" CssClass="btn-primary" OnClick="btnIngresar_Click" />

                <asp:Label ID="lblMensaje" runat="server" CssClass="message-error"></asp:Label>
            </div>

            <p class="footer-text">SENA &copy; <%= DateTime.Now.Year %> - Todos los derechos reservados</p>
        </div>
    </form>
</body>
</html>