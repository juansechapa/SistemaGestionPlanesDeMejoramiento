<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="InicioAdmin.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.InicioAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-info">
        <h3>Bienvenido, <asp:Label ID="lblBienvenida" runat="server" Text=""></asp:Label></h3>
        <p>Panel de control del administrador. Desde aquí puedes gestionar centros, instructores, aprendices y planes.</p>
    </div>
    <!-- Agregar metodos y demas aqui -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>