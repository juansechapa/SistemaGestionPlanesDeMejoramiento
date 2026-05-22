<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GestionCentroPrograma.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Admin.GestionCentroPrograma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between align-items-center mb-3">
        <h2><i class="fas fa-link"></i>Asignación de Programas a Centros</h2>
    </div>

    <div class="card shadow mb-4">
        <div class="card-body">
            <div class="row">
                <div class="col-md-6">
                    <label>Seleccionar Centro:</label>
                    <asp:DropDownList ID="ddlCentro" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged" />
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Programas asignados -->
        <div class="col-md-6">
            <div class="card shadow">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0">Programas asignados a este centro</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvAsignados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm"
                        DataKeyNames="idPrograma" OnRowCommand="gvAsignados_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codigoPrograma" HeaderText="Código" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Quitar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnQuitar" runat="server" CommandName="Quitar" CommandArgument='<%# Eval("idPrograma") %>'
                                        CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Quitar este programa del centro?');">
                                        <i class="fas fa-trash"></i> Quitar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">No hay programas asignados a este centro.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- Programas disponibles -->
        <div class="col-md-6">
            <div class="card shadow">
                <div class="card-header bg-success text-white">
                    <h5 class="mb-0">Programas disponibles para asignar</h5>
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvDisponibles" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm"
                        DataKeyNames="idPrograma" OnRowCommand="gvDisponibles_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="codigoPrograma" HeaderText="Código" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:TemplateField HeaderText="Asignar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAsignar" runat="server" CommandName="Asignar" CommandArgument='<%# Eval("idPrograma") %>'
                                        CssClass="btn btn-primary btn-sm">
                                        <i class="fas fa-plus"></i> Asignar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">No hay programas disponibles para asignar.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
