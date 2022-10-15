<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="CarritoDeCompras.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%--<div class="container text-center">--%>
  <%--<div class="row row-cols-2">--%>
    <%--<div class="col">Producto</div>--%>
    <%--<div class="col">Precio</div>--%>
  <%--</div>--%>
<%--</div>--%>
    <%--<hr />--%>
    
    <asp:GridView runat="server" ID="Articulos"> 
        <Columns>
            <asp:ImageField></asp:ImageField>
           <asp:BoundField HeaderText="Descripcion" DataField="" />
            <asp:BoundField HeaderText="Precio" DataField="" />
        </Columns>
    </asp:GridView>
</asp:Content>
