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
            //ArticuloNegocio negocio=new ArticuloNegocio();
            if (!IsPostBack)
            {
                if (Session["Carrito"] != null)
                {

                    Carrito carrito = (Carrito)Session["Carrito"];

                    repCarrito.DataSource = carrito.ListaArticulo;
                    repCarrito.DataBind();
                    lblPrecioTotal.Text = carrito.PrecioTotal.ToString();
                }






            }

          
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            //var id = dgvArticulos.SelectedDataKey.Value.ToString();
           // Response.Redirect("DetalleArticulo.aspx?id=" +  id);
     
        
        }


        protected void repCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Dominio.Articulo art = (Dominio.Articulo)e.Item.DataItem;
                System.Web.UI.WebControls.Image imgImagen = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgImagen");

                /* Place holder si la imagen original falla */

              
                    string urlImagenOriginal = art.Imagenes[0].UrlImagen;
                    string urlImagenReemplazo = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png";

                    imgImagen.ImageUrl = urlImagenOriginal;
                    imgImagen.Attributes["onerror"] = "this.onerror=null;this.src='" + urlImagenReemplazo + "';";
            


                Label lblCantidad = (Label)e.Item.FindControl("lblCantidad");

                if(art!=null && lblCantidad!=null && Session["Carrito"] is Carrito carrito)
                {
                    int cantidad = carrito.ObtenerCantidadArticulo(art.Id);
                    lblCantidad.Text = cantidad.ToString();


                    decimal precio = art.Precio;
                    decimal total = precio * cantidad;
                    Label lblTotalCantXart = (Label)e.Item.FindControl("lblTotalCantXart");
                    lblTotalCantXart.Text = total.ToString();
                }
            
            
            
            }
            //updatePanelCarrito.Update();
        }
        //SUMAR Y RESTAR ART. DEL CARRITO
        protected void btnRestar_Click(object sender, EventArgs e)
        {
            Carrito carritoAct = (Carrito)Session["Carrito"];
            Button btnRestar = (Button)sender;
            int id = int.Parse(btnRestar.CommandArgument);

            Articulo art = carritoAct.ListaArticulo.FirstOrDefault(a => a.Id == id);
            carritoAct.RestarArticulo(art);
            Session["Carrito"] = carritoAct;
            updatePanelCarrito.Update();
        }

        protected void btnSumar_Click(object sender, EventArgs e)
        {
            Carrito carritoAct = (Carrito)Session["Carrito"];
            Button btnSumar = (Button)sender;
            int id = int.Parse(btnSumar.CommandArgument);

            Articulo art = carritoAct.ListaArticulo.FirstOrDefault(a => a.Id == id);
            carritoAct.AgregarArticulo(art);
            Session["Carrito"] = carritoAct;
            updatePanelCarrito.Update();
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {

            if (Session["Carrito"] != null)
            {
                Carrito carrito = (Carrito)Session["Carrito"];

                repCarrito.DataSource = carrito.ListaArticulo;
                repCarrito.DataBind();
                lblPrecioTotal.Text = carrito.PrecioTotal.ToString();
            }

        }



    }

}