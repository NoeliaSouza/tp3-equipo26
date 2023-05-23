
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Net;
using System.Drawing;
using System.Security.Cryptography;
using System.Security.Policy;

namespace ProyectoCarrito
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo{ get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listarConSP();

        }
                
    }
}