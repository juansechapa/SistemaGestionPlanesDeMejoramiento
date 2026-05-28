<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Aprendiz/AprendizMaster.Master" AutoEventWireup="true" CodeBehind="InicioAprendiz.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Aprendiz.InicioAprendiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-info">
        <i class="fas fa-circle-info"></i>
        <div>
        <h2>¡Hola,
            <asp:Label ID="lblBienvenida" runat="server"></asp:Label>!</h2>
        <p>Bienvenido a tu panel de control. Aquí puedes consultar tus datos y el estado de tus planes de mejoramiento.</p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-6 mb-3">
            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0">Mis datos personales</h5>
                </div>
                <div class="card-body">
                    <p><strong>Nombres:</strong>
                        <asp:Label ID="lblNombres" runat="server" /></p>
                    <p><strong>Apellidos:</strong>
                        <asp:Label ID="lblApellidos" runat="server" /></p>
                    <p><strong>Tipo documento:</strong>
                        <asp:Label ID="lblTipoDoc" runat="server" /></p>
                    <p><strong>Número documento:</strong>
                        <asp:Label ID="lblNumDoc" runat="server" /></p>
                    <p><strong>Correo:</strong>
                        <asp:Label ID="lblCorreo" runat="server" /></p>
                    <p><strong>Teléfono:</strong>
                        <asp:Label ID="lblTelefono" runat="server" /></p>
                    <p><strong>Fecha nacimiento:</strong>
                        <asp:Label ID="lblFechaNac" runat="server" /></p>
                </div>
            </div>
        </div>
        <div class="col-md-6 mb-3">
            <div class="card shadow">
                <div class="card-header bg-success text-white">
                    <h5 class="mb-0">Información académica</h5>
                </div>
                <div class="card-body">
                    <p>
                        <strong>Estado académico:</strong>
                        <asp:Label ID="lblEstado" runat="server" CssClass="badge bg-secondary" />
                    </p>
                    <p><strong>Ficha:</strong>
                        <asp:Label ID="lblFicha" runat="server" /></p>
                    <p><strong>Programa:</strong>
                        <asp:Label ID="lblPrograma" runat="server" /></p>
                    <p><strong>Centro:</strong>
                        <asp:Label ID="lblCentro" runat="server" /></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
