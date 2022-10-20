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
        public decimal totalCarrito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try {               
                if (!IsPostBack)
                {
                    dgvArticulos.DataSource = ((List<ItemCarrito>)(Session["ListaItemCarrito"]));
                    dgvArticulos.DataBind();
                }              
            }
            catch (Exception ex) { 

            }           
            
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            if (valor != null)
            {
                int Id = int.Parse(valor);
                int posItem = ((List<ItemCarrito>)(Session["ListaItemCarrito"])).FindIndex(x => x.articulo.Id == Id);

                if (posItem > -1)
                {
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"])).RemoveAt(posItem);
                    dgvArticulos.DataSource = ((List<ItemCarrito>)(Session["ListaItemCarrito"]));
                    dgvArticulos.DataBind();
                }
                if (((List<ItemCarrito>)(Session["ListaItemCarrito"])).Count()==0)
                {
                    Session.Remove("ListaItemCarrito");
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            if (valor != null)
            {
                int Id = int.Parse(valor);
                int posItem = ((List<ItemCarrito>)(Session["ListaItemCarrito"])).FindIndex(x => x.articulo.Id == Id);

                if (posItem > -1)
                {
                    decimal precioUnitario = ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].precioTotal / ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad;
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad++;
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].precioTotal = precioUnitario * ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad;
                    dgvArticulos.DataSource = ((List<ItemCarrito>)(Session["ListaItemCarrito"]));
                    dgvArticulos.DataBind();
                }
            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
            if (valor != null)
            {
                int Id = int.Parse(valor);
                int posItem = ((List<ItemCarrito>)(Session["ListaItemCarrito"])).FindIndex(x => x.articulo.Id == Id);

                if (posItem > -1)
                {
                    decimal precioUnitario = ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].precioTotal / ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad;
                    ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad--;
                    if(((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad == 0)
                    {
                        ((List<ItemCarrito>)(Session["ListaItemCarrito"])).RemoveAt(posItem);
                    }
                    else
                    {
                        ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].precioTotal = precioUnitario * ((List<ItemCarrito>)(Session["ListaItemCarrito"]))[posItem].cantidad;
                    }
                    dgvArticulos.DataSource = ((List<ItemCarrito>)(Session["ListaItemCarrito"]));
                    dgvArticulos.DataBind();
                }
                if (((List<ItemCarrito>)(Session["ListaItemCarrito"])).Count() == 0)
                {
                    Session.Remove("ListaItemCarrito");
                }
            }
        }
    }
}