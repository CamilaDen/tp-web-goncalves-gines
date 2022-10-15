using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.Drawing;

namespace CarritoDeCompras
{
   
    public partial class Carrito : System.Web.UI.Page
    {
        public List<ItemCarrito> ListaItemCarrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                if (Session["ArticuloId"] != null)
                {
                    int Id = int.Parse(Session["ArticuloId"].ToString());
                    Session.Remove("ArticuloId");


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
                if (!IsPostBack)
                {
                    dgvArticulos.DataSource = ((List<ItemCarrito>)(Session["ListaItemCarrito"]));
                    dgvArticulos.DataBind();
                }
            }
            catch (Exception ex) { 

            }
            
        }
    }
}