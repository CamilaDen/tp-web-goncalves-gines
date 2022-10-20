using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace CarritoDeCompras
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        public List<Categoria> listaCat { get; set; }
        public List<Marca> listaMar { get; set; }
        public int cantArticulos { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listarConSP();
            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaArticulo;
                repRepetidor.DataBind();
                DropDownCategoria.Items.Insert(0, "Filtrar Categoria");
                DropDownMarca.Items.Insert(0, "Filtrar Marca");
                cargarDropDowns();
            }
            else
            {
                if (DropDownMarca.SelectedIndex <= 0 && DropDownCategoria.SelectedIndex <= 0)
                {
                    limpiarDropDowns();
                    cargarDropDowns();
                }


            }
            /*if (btnFiltrar())
            {
                limpiarDropDowns();
                cargarDropDowns();
            }*/

        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            List<ItemCarrito> ListaItemCarrito;

            if (valor != null)
            {
                int Id = int.Parse(valor);

                if (Session["ListaItemCarrito"] == null)
                {
                    ListaItemCarrito = new List<ItemCarrito>();
                    Session.Add("ListaItemCarrito", ListaItemCarrito);
                }
                int posItem = ((List<ItemCarrito>)(Session["ListaItemCarrito"])).FindIndex(x => x.articulo.Id == Id);

                if (posItem == -1)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo articulo = new Articulo();
                    articulo = negocio.buscarPorId(Id);
                    ItemCarrito item = new ItemCarrito
                    {
                        articulo = articulo,
                        cantidad = 1,
                        precioTotal = articulo.Precio
                    };
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"])).Add(item);
                }
                else
                {
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad++;
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].precioTotal *= ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad;
                }
            }
        }

        protected void btnFiltrar_Click1(object sender, EventArgs e)
        {

            Articulo lista = new Articulo();
            try
            {
                filtrarBusqueda();
                
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected List<Articulo> filtrarBusqueda()
        {

            var listaFiltrada = new List<Articulo>();
            var conexion = new negocio.AccesoDatos();

            //string opcionProducto = DropDownProducto.ToString();
            string opcionMarca = DropDownMarca.SelectedItem.ToString();
            string opcionCategoria = DropDownCategoria.SelectedItem.ToString();
            if (opcionCategoria != null && opcionMarca != null)
            {
                if (opcionCategoria != "Filtrar Categoria" && opcionMarca != "Filtrar Marca")
                {

                    string consulta = "Select  * FROM ARTICULOS A Inner join Categorias C on C.Id = A.IdCategoria Inner join MARCAS M on M.Id = A.IdMarca Where C.Descripcion Like '%" + opcionCategoria + "%' AND M.Descripcion Like '%" + opcionMarca + "%'";
                    conexion.setearConsulta(consulta);
                    conexion.ejecutarLectura();

                    while (conexion.Lector.Read())
                    {
                        Articulo a = new Articulo();
                        a.Id = (int)conexion.Lector["Id"];
                        a.Nombre = (string)conexion.Lector["Nombre"];
                        a.Descripcion = (string)conexion.Lector["Descripcion"];
                        if (!(conexion.Lector["ImagenUrl"] is DBNull))
                            a.ImagenUrl = (string)conexion.Lector["ImagenUrl"];

                        listaFiltrada.Add(a);
                    }
                    if (Page.IsPostBack)
                    {
                        repRepetidor.DataSource = listaFiltrada;
                        repRepetidor.DataBind();
                    }


                }
                else
                {
                    //mensaje para validar seleccion
                }


            }

            return listaFiltrada;
        }

        private void cargarDropDowns()
        {

            CategoriaNegocio nC = new CategoriaNegocio();
            listaCat = nC.listar();

            foreach (var item in listaCat)
            {
                DropDownCategoria.Items.Add(item.Descripcion);
            }

            MarcaNegocio nM = new MarcaNegocio();
            listaMar = nM.listar();
            foreach (var item in listaMar)
            {
                DropDownMarca.Items.Add(item.Descripcion);
            }

        }
        private void limpiarDropDowns()
        {
            DropDownCategoria.Items.Clear();
            DropDownMarca.Items.Clear();
        }



    }
}