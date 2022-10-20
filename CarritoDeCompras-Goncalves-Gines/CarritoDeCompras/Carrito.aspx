<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="CarritoDeCompras.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        if (Session["ListaItemCarrito"] == null)
        {
    %>
    <h2>No existen productos agregados al carrito...</h2>
    <%
        }
        else
        {
            totalCarrito = ((List<dominio.ItemCarrito>)(Session["ListaItemCarrito"])).Sum( item => item.precioTotal );
    %>
    <div>
        <asp:GridView ID="dgvArticulos" runat="server" CssClass="table" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="Producto" DataField="articulo.Nombre" />
                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                <asp:BoundField HeaderText="Precio" DataField="precioTotal" DataFormatString="{0:c}" HtmlEncode="False" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button Text=" + " CssClass="btn btn-success" runat="server" ID="btnAgregar" CommandArgument='<%#Eval("articulo.Id") %>' CommandName="ArticuloId" OnClick="btnAgregar_Click" />
                        <asp:Button Text="  - " CssClass="btn btn-dark" runat="server" ID="btnQuitar" CommandArgument='<%#Eval("articulo.Id") %>' CommandName="ArticuloId" OnClick="btnQuitar_Click" />
                        <asp:Button Text="Eliminar" CssClass="btn btn-danger" runat="server" ID="btnEliminar" CommandArgument='<%#Eval("articulo.Id") %>' CommandName="ArticuloId" OnClick="btnEliminar_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h3 class="text-end">Total  <%: totalCarrito.ToString("C") %> </h3>
    </div>
    <%
        }
    %>
</asp:Content>
