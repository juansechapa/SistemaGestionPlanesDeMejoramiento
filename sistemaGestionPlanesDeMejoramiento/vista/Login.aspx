<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Iniciar Sesion</h2>
        <div>
            <label>Usuario:</label>
            <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
        </div>
        <div>
            <label>Contraseña:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
        </div>
        <div>
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>