<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Instructor/InstructorMaster.Master" AutoEventWireup="true" CodeBehind="InicioInstructor.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Instructor.InicioInstructor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-info">
        <h2>Bienvenido. <asp:Label ID="lblBienvenida" runat="server"></asp:Label></h2>
        <p>>Aquí podrás gestionar los planes de mejoramiento asignados a tus aprendices.</p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
