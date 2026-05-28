<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Instructor/InstructorMaster.Master" AutoEventWireup="true" CodeBehind="MisPlanes.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Instructor.MisPlanes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between mb-3">
        <h2><i class="fas fa-clipboard-list"></i>Mis Planes de Mejoramiento</h2>
    </div>
    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvPlanes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                OnRowCommand="gvPlanes_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvPlanes_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="idPlanMejoramiento" HeaderText="ID" />
                    <asp:BoundField DataField="NombreAprendiz" HeaderText="Aprendiz" />
                    <asp:BoundField DataField="TipoPlan" HeaderText="Tipo" />
                    <asp:BoundField DataField="actividadesPropuestas" HeaderText="Actividades" />
                    <asp:BoundField DataField="fechaEntrega" HeaderText="Entrega" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="estadoPlan" HeaderText="Estado" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay planes registrados.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
