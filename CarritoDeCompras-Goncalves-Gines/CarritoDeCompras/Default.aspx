<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CarritoDeCompras.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    
    <div class="top-0 " style="text-align: right">
        <button type="button" class="btn btn-primary position-relative">
         Articulos Seleccionados
         <span class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
          4
         <span class="visually-hidden">unread messages</span>
         </span>
        </button>
    </div>
    <hr />
    <div class="row g-3">
          <div class="col-sm-7">
            <input type="text" class="form-control" placeholder="Producto" aria-label="Producto">
          </div>
          <div class="col-sm">
            <div class="btn-group">
                  <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Categoria
                  </button>
                  <ul class="dropdown-menu">
                    <lu>Algo1</lu>
                  </ul>
            </div>
          </div>
         <div class="col-sm">
            <div class="btn-group">
                  <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Categoria
                  </button>
                  <ul class="dropdown-menu">
                    <lu>Algo1</lu>
                  </ul>
            </div>
          </div>
           <div class="col-sm">
                <asp:Button Text="Buscar" CssClass="btn btn-success  " runat="server" ID="btnFiltrar"/>
           </div>
    </div>

    <h1>Listado de Artículos</h1>
    <%-- <asp:GridView ID="dgvArticulos" runat="server" CssClass="table" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descripcion" />
        </Columns>
    </asp:GridView>--%>
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
                    <div class="card" id="cardArticulo" >
                        <img src="<%#Eval("ImagenUrl") %>" class="card-img-top" alt="imagen de producto">
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion") %></p>
                            <div>
                            <%--<asp:Button Text="Comprar" CssClass="btn btn-success  " runat="server" ID="btnComprar" CommandArgument='<%#Eval("Id") %>' CommandName="ArticuloId" OnClick="btnComprar_Click" />--%>
                            <asp:CommandField ShowSelectButton ="true" SelectText="Comprar" HeaderText="Accion" />
                            </div>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>
    </div>
   <%-- 
       TOAST (Ver bien como funciona con el Js y el tema de que hace getID)
       <div class="toast-container position-fixed bottom-0 end-0 p-3">
        <div id="liveToast" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header">
                <img src="..." class="rounded me-2" alt="...">
                <strong class="me-auto">Bootstrap</strong>
                <small>11 mins ago</small>
                <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
            </div>
            <div class="toast-body">
                Hello, world! This is a toast message.
            </div>
        </div>
    </div>--%>
</asp:Content>
