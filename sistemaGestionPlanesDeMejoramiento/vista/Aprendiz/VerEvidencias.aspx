<%@ Page Title="" Language="C#" MasterPageFile="~/vista/Aprendiz/AprendizMaster.Master" AutoEventWireup="true" CodeBehind="VerEvidencias.aspx.cs" Inherits="sistemaGestionPlanesDeMejoramiento.vista.Aprendiz.VerEvidencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex justify-content-between mb-3">
        <h2><i class="fas fa-paperclip"></i>Evidencias del Plan</h2>
        <asp:LinkButton ID="btnVolver" runat="server" PostBackUrl="MisPlanes.aspx" CssClass="btn btn-secondary" CausesValidation="false">Volver</asp:LinkButton>
    </div>
    <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger message-label" />
    <div class="card shadow">
        <div class="card-body">
            <asp:GridView ID="gvEvidencias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                OnRowCommand="gvEvidencias_RowCommand"
                AllowPaging="True" PageSize="15" OnPageIndexChanging="gvEvidencias_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="nombreArchivo" HeaderText="Nombre Archivo" />
                    <asp:BoundField DataField="fechaSubida" HeaderText="Fecha Subida" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="observaciones" HeaderText="Observaciones" />
                    <asp:TemplateField HeaderText="Descargar">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDescargar" runat="server" CommandName="Descargar" CommandArgument='<%# Eval("idEvidencia") %>'
                                CssClass="btn btn-sm btn-success" CausesValidation="false">
                                Descargar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="alert alert-info">No hay evidencias subidas para este plan.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
