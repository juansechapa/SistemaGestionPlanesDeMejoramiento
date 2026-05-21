<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Ingreso - Planes de Mejoramiento SENA</title>
    <link href="<%= ResolveUrl("~/vista/CSS/loginEstilos.css") %>" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <main class="login-page">
            <section class="login-hero" aria-label="Sistema de gestión de planes de mejoramiento">
                <div class="brand-panel">
                    <div class="brand-mark" aria-hidden="true">S</div>
                    <p class="brand-kicker">SENA</p>
                    <h1>Planes de Mejoramiento</h1>
                    <p class="brand-copy">Ingreso institucional para aprendices, instructores y administradores.</p>
                </div>

                <section class="login-card" aria-labelledby="login-title">
                    <div class="login-card-header">
                        <span class="login-badge">Acceso seguro</span>
                        <h2 id="login-title">Iniciar sesión</h2>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtUsuario.ClientID %>">Usuario</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" autocomplete="username"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtPassword.ClientID %>">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" autocomplete="current-password"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="login-button" OnClick="btnIngresar_Click" />

                    <asp:Label ID="lblMensaje" runat="server" CssClass="login-message"></asp:Label>
                </section>
            </section>
        </main>
    </form>
</body>
</html>
