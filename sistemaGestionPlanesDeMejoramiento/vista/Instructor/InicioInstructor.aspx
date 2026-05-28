<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Instructor/InstructorMaster.Master" AutoEventWireup="true" CodeBehind="InicioInstructor.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Instructor.InicioInstructor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-info">
        <i class="fas fa-circle-info"></i>
        <div>
        <h2>Bienvenido,
            <asp:Label ID="lblBienvenida" runat="server"></asp:Label></h2>
        <p>Aquí podrás gestionar los planes de mejoramiento de tus aprendices.</p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 mb-3">
            <div class="card shadow dashboard-card">
                <div class="card-body">
                    <h5 class="card-title"><i class="fas fa-users"></i>Mis Aprendices</h5>
                    <p class="card-text">Consulta los aprendices asignados a tus fichas.</p>
                    <a href="MisAprendices.aspx" class="btn btn-primary">Ir</a>
                </div>
            </div>
        </div>
        <div class="col-md-6 mb-3">
            <div class="card shadow dashboard-card">
                <div class="card-body">
                    <h5 class="card-title"><i class="fas fa-clipboard-list"></i>Planes Pendientes</h5>
                    <p class="card-text">Revisa y evalúa los planes de mejoramiento.</p>
                    <a href="MisPlanes.aspx" class="btn btn-primary">Ir</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
