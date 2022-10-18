<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoDeCompras.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    
    <div class="top-0 " style="text-align: right">
        <i></i>
        <button type="button" class="btn btn-primary position-relative">
         <span class="material-symbols-outlined">shopping_cart</span>
         <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
           <%
               if (Session["ListaItemCarrito"] != null)
                   ((List<dominio.ItemCarrito>)(Session["ListaItemCarrito"])).Count().ToString();
                
           %>
         </span>
        </button>
    </div>
    <hr />
    <div class="row g-3">
          <div class="col-sm-7">
            <asp:TextBox ID="txtFiltrarProducto" class="form-control" runat="server" placeholder="Producto" aria-label="Producto"></asp:TextBox>
          </div>
          <div class="col-sm">
           <asp:DropDownList ID="DropDownCategoria" class="form-select" aria-labelledby="Categoria " runat="server" ></asp:DropDownList>
          </div>
         <div class="col-sm">
            <asp:DropDownList ID="DropDownMarca" class="form-select" runat="server"></asp:DropDownList>
          </div>
           <div class="col-sm">
                <asp:Button Text="Buscar" CssClass="btn btn-success  " runat="server" ID="btnFiltrar"/>
           </div>
    </div>

    <h1>Listado de Artículos</h1>
    <div class="row row-cols-1 row-cols-md-3 g-4" >
        <%--     <%
            foreach (dominio.Articulo articulo in ListaArticulo)
            {
        %>
        <div class="col">
            <div class="card">
                <img src="<%:articulo.ImagenUrl %>" class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title"><%: articulo.Nombre %></h5>
                    <p class="card-text"><%: articulo.Descripcion %></p>
                    <button type="button" class="btn btn-success" onclick="">Comprar</button>
                </div>
            </div>
        </div>
        <%  } %>--%>
        <asp:Repeater runat="server" ID="repRepetidor" >
            <ItemTemplate >
    
                <div class="col">
                    <div class="card" id="cardArticulo" style="width: 20rem; height: 30rem;">
                        <img src="<%#Eval("ImagenUrl") %>" class="card-img-top" alt="imagen de producto" style="width: 20rem; height: 19rem; object-fit: contain;">
                        <div class="card-body" style="width: 20rem; height: 5rem;">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <div>
                            <asp:Button Text="Comprar" CssClass="btn btn-success" runat="server" ID="btnComprar" CommandArgument='<%#Eval("Id") %>' CommandName="ArticuloId" OnClick="btnComprar_Click" />
                            </div>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
