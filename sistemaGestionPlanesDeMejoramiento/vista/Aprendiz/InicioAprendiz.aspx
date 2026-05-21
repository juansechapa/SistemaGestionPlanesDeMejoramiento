<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Aprendiz/AprendizMaster.Master" AutoEventWireup="true" CodeBehind="InicioAprendiz.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Aprendiz.InicioAprendiz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div calss="alert alert-info">
        <h3>Bienvenido aprendiz.<asp:Label ID="lblBienvenida" runat="server"></asp:Label></h3>
        <p>Revisa el estado de tus planes de mejoramiento y sube las evidencias requeridas.</p>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
