
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
            

        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                //RECIVO LO DEL BOTON EN ESTE EVENTO
                Button btnDetalle = (Button)sender;

                // //COPIO EL ID QUE OBTUVE A TRAVES DEL EVENTO EN UNA VARIABLE ID Y LA MANDO A DetalleArticulo.aspx
                var id = btnDetalle.CommandArgument;
               
                Response.Redirect("DetalleArticulo.aspx?id=" + id);
            }
            catch (Exception ex)
            {
                
                throw ;
            }



        }



    }
}