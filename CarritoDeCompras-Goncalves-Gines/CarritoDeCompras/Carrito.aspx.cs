using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace CarritoDeCompras
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {

               // ArticuloNegocio art = Request.QueryString["valor"] != null ? Request.QueryString["valor"];           
                
            }
            catch(Exception ex) { 

            }
            
        }
    }
}