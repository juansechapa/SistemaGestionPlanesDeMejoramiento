<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReestablecerClave.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.ReestablecerClave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Restablecer Contraseña</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../vista/CSS/recuperacion.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-12 col-sm-10 col-md-7 col-lg-5">
                    <div class="card shadow">
                        <div class="card-header bg-success text-white">
                            <h3 class="mb-0">Restablecer Contraseña</h3>
                        </div>
                        <div class="card-body">
                            <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger w-100" Visible="false" />
                            <div class="mb-3">
                                <label class="form-label">Código de verificación</label>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="15" placeholder="Ej: A1B2C3D4" />
                                <asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ControlToValidate="txtCodigo"
                                    ErrorMessage="* Campo obligatorio" CssClass="text-danger" />
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Nueva contraseña</label>
                                <asp:TextBox ID="txtNuevaPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Mínimo 6 caracteres" />
                                <asp:RequiredFieldValidator ID="rfvNueva" runat="server" ControlToValidate="txtNuevaPassword"
                                    ErrorMessage="* Campo obligatorio" CssClass="text-danger" />
                            </div>
                            <div class="mb-3">
                                <label class="form-label">Confirmar contraseña</label>
                                <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Repite la contraseña" />
                                <asp:RequiredFieldValidator ID="rfvConfirmar" runat="server" ControlToValidate="txtConfirmarPassword"
                                    ErrorMessage="* Campo obligatorio" CssClass="text-danger" />
                                <asp:CompareValidator ID="cvPasswords" runat="server" ControlToCompare="txtNuevaPassword"
                                    ControlToValidate="txtConfirmarPassword" ErrorMessage="Las contraseñas no coinciden"
                                    CssClass="text-danger" />
                            </div>
                            <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer" CssClass="btn btn-success w-100" OnClick="btnRestablecer_Click" />
                            <div class="text-center mt-3">
                                <a href="Login.aspx">Volver al inicio de sesión</a>
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