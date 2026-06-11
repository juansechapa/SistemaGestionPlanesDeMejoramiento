<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.RecuperarContrasena" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Recuperar Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../vista/CSS/recuperacion.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-12 col-sm-10 col-md-7 col-lg-5">
                    <div class="card shadow">
                        <div class="card-header bg-primary text-white">
                            <h3 class="mb-0">Recuperar Contrasena</h3>
                        </div>
                        <div class="card-body">
                            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-info w-100" Visible="false" />
                            <div class="mb-3">
                                <label class="form-label">Correo electronico</label>
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" placeholder="ejemplo@correo.com" />
                                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo"
                                    ErrorMessage="* Campo obligatorio" CssClass="text-danger" />
                                <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ErrorMessage="Correo inválido" CssClass="text-danger" />
                            </div>
                            <asp:Button ID="btnEnviar" runat="server" Text="Enviar codigo" CssClass="btn btn-primary w-100" OnClick="btnEnviar_Click" />
                            <div class="text-center mt-3">
                                <a href="ReestablecerClave.aspx">Ya recivi el codigo</a>
                                <span class="text-muted mx-2">|</span>
                                <a href="Login.aspx">Volver al inicio de sesion</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>