using Dominio;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoCarrito
{
    public partial class MiniCarrito : System.Web.UI.Page
    {
        public bool MostrarNav { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["Carrito"] != null)
                {

                    Master masterPage = this.Master as Master;
                    if (masterPage != null)
                    {
                        masterPage.NavContentVisible = MostrarNav;
                    }
                    MostrarNav = false;


                    Carrito carrito = (Carrito)Session["Carrito"];


                    repCarrito.DataSource = carrito.ListaArticulo;
                    repCarrito.DataBind();
                    lblPrecioTotal.Text = carrito.PrecioTotal.ToString();

                }

            }



        }

        protected void repCarrito_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Dominio.Articulo art = (Dominio.Articulo)e.Item.DataItem;
                System.Web.UI.WebControls.Image imgImagen = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImagenCarrito");



                /* Place holder si la imagen original falla */
                string urlImagenOriginal = art.Imagenes[0].UrlImagen;
                string urlImagenReemplazo = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png";

                imgImagen.ImageUrl = urlImagenOriginal;
                imgImagen.Attributes["onerror"] = "this.onerror=null;this.src='" + urlImagenReemplazo + "';";

                //ESTO SE ENCARGA DE AJUSTAR LA CANTIDAD DE ARTICULOS
                Label lblCantidad = (Label)e.Item.FindControl("lblCantidad");
                if (art != null && lblCantidad != null && Session["Carrito"] is Carrito carrito)
                {
                    // Obtener la cantidad correspondiente del diccionario utilizando el nuevo método ObtenerCantidadArticulo
                    int cantidad = carrito.ObtenerCantidadArticulo(art.Id);
                    lblCantidad.Text = cantidad.ToString();
                }
                updatePanelCarrito.Update();

            }

        }

        //protected void panelCarrito_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "AsignarEventos", "asignarEventos();", true);
        //    }
        //}

        protected void restarArticulo_Click(object sender, EventArgs e)
        {   
            Carrito carrito = (Carrito)Session["Carrito"];
            //RECIVO LO DEL BOTON EN ESTE EVENTO
            Button btnRestar = (Button)sender;
            // //COPIO EL ID QUE OBTUVE A TRAVES DEL EVENTO EN UNA VARIABLE ID Y LA MANDO A DetalleArticulo.aspx
            int id = int.Parse(btnRestar.CommandArgument);

            Articulo articulo = carrito.ListaArticulo.FirstOrDefault(a => a.Id == id);

            carrito.RestarArticulo(articulo);

            Session["Carrito"] = carrito;

            updatePanelCarrito.Update();

           

        }


        protected void sumarArticulo_Click(object sender, EventArgs e)
        {
            Carrito carrito = (Carrito)Session["Carrito"];
            //RECIVO LO DEL BOTON EN ESTE EVENTO
            Button btnSumar = (Button)sender;
            // //COPIO EL ID QUE OBTUVE A TRAVES DEL EVENTO EN UNA VARIABLE ID Y LA MANDO A DetalleArticulo.aspx
            int id = int.Parse(btnSumar.CommandArgument);

            Articulo articulo = carrito.ListaArticulo.FirstOrDefault(a => a.Id == id);

            carrito.AgregarArticulo(articulo);

            Session["Carrito"] = carrito;

            updatePanelCarrito.Update();

           
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {


            Carrito carrito = (Carrito)Session["Carrito"];


            repCarrito.DataSource = carrito.ListaArticulo;
            repCarrito.DataBind();
            lblPrecioTotal.Text = carrito.PrecioTotal.ToString();

        }

      
    }
}