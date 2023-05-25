using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace ProyectoCarrito
{
    public partial class Listado : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio=new ArticuloNegocio();
            dgvArticulos.DataSource = negocio.listarConSP();
            dgvArticulos.DataBind(); /* enlaza datos*/
            
            Session.Add("ListaArticulo", negocio.listarConSP());
            ListaArticulo = (List<Articulo>)Session["ListaArticulo"];
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvArticulos.SelectedRow.Cells[0].Text;
            
            var id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("DetalleArticulo.aspx?id=" +  id);
        }
    }
}