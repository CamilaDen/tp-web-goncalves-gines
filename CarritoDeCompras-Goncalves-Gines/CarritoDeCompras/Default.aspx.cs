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
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listarConSP(); 
            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaArticulo;
                repRepetidor.DataBind();
            }
            //dgvArticulos.DataSource = negocio.listarConSP();
            //dgvArticulos.DataBind(); 
               DropDownCategoria.Items.Add("Celulares");
               DropDownCategoria.Items.Add("Televisores");
               DropDownCategoria.Items.Add("Tablets");
               DropDownCategoria.Items.Add("Monitores");
               DropDownMarca.Items.Add("Samsung");
               DropDownMarca.Items.Add("Apple");
               DropDownMarca.Items.Add("Motorola");
               DropDownMarca.Items.Add("Sony");
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            Session.Add("ArticuloId",valor);  
            Response.Redirect("Carrito.aspx", false);
        }

        protected void btnFiltrar_Click(object sender, EventArgs e) {

            ArticuloNegocio lista = new ArticuloNegocio();
            try
            {
                filtrarBusqueda();
            }
            catch (Exception ex)
            {

                throw;
            }

            limpiarFiltros();
        }

        protected List<Articulo> filtrarBusqueda(){

            var listaFiltrada = new List<Articulo>();
            var conexion = new negocio.AccesoDatos();
           
            string descripcionProducto = txtFiltrarProducto.Text;
            string opcionMarca = DropDownMarca.SelectedItem.ToString(); 
            string opcionCategoria = DropDownCategoria.SelectedItem.ToString();
            if ( descripcionProducto != null && opcionCategoria != null && opcionMarca != null )
            {
                string consulta = "Select * FROM Articulos A inner join CATEGORIAS C ON C.Id = A.IdCategoria inner join MARCAS M ON M.Id = A.IdMarca Where A.Descripcion Like '%"+descripcionProducto+"%' AND C.Descripcion Like '%"+ opcionCategoria +"%' AND M.Descripcion Like '%"+ opcionMarca +"%'";
                conexion.setearConsulta(consulta);
                conexion.ejecutarLectura();
                
            }
            else {
                //Faltaria Mostrar Cartel de alerta por falta de seleccion mediante label.
            }
            while (conexion.Lector.Read()) { 

                       
            }
            return listaFiltrada;
        }

        protected void limpiarFiltros()
        {
            DropDownCategoria.SelectedIndex = -1;
            DropDownMarca.SelectedIndex = -1;
            btnFiltrar.Enabled = true;
            txtFiltrarProducto.Text = null;
        }
    }
}