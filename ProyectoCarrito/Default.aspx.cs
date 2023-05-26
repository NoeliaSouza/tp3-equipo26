
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
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //version anterior
            //ArticuloNegocio negocio = new ArticuloNegocio();
            //ListaArticulo = negocio.listarConSP();

            ArticuloNegocio negocio = new ArticuloNegocio();
            Session.Add("ListaArticulo", negocio.listarConSP());
            ListaArticulo = (List<Articulo>)Session["ListaArticulo"];
            Session["inicio"] = 0;

            if (Session["listaArticulosFiltrada"] == null)
            {
                repRepetidor.DataSource = ListaArticulo;
                repRepetidor.DataBind();
            }
            else
            {
                repRepetidor.DataSource = (List<Articulo>)Session["ListaArticulosFiltrada"];
                repRepetidor.DataBind();
                Session["ListaArticulosFiltrada"] = null;
            }

        }

        //Validacion imagen
        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            }
        }


        //protected void btnDetalle_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //RECIVO LO DEL BOTON EN ESTE EVENTO
        //        Button btnDetalle = (Button)sender;
        //        // //COPIO EL ID QUE OBTUVE A TRAVES DEL EVENTO EN UNA VARIABLE ID Y LA MANDO A DetalleArticulo.aspx
        //        var id = btnDetalle.CommandArgument;
        //        Response.Redirect("DetalleArticulo.aspx?id=" + id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            Button btnAgregarCarrito = (Button)sender;
            int id = int.Parse(btnAgregarCarrito.CommandArgument);


            /* cargamos el articulo recibido */
            //ArticuloNegocio negocio= new ArticuloNegocio();
            //List<Articulo> lista = negocio.listarConSP();
            Articulo articulo = ListaArticulo.Find(x => x.Id == id);

            /* agregar funcion para agregar articulo al listado de la pagina "carrito" */

            string mensaje = articulo.Nombre + "agregado al carrito ";
            /* funcion mostrar mensaje y mandar msj por parametro*/

        }
    }
}