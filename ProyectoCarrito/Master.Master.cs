using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace ProyectoCarrito
{
    public partial class Master : System.Web.UI.MasterPage
    {
        public bool MostrarFiltros { get; set; }

        public bool MostrarCarrito { get; set; }
        
        public bool NavContentVisible
        {
            get { return NavContent.Visible; }
            set { NavContent.Visible = value; }
        }

        public void ToggleNavContent(bool visible)
        {
            NavContent.Visible = visible;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            panelContador.Update();
            try
            {


                Carrito carrito2 = (Carrito)Session["Carrito"];
                int cantidadTotalArticulos = carrito2.ObtenerCantidadTotalArticulos();

                //if (lblTotalCantCarrito != null)
                if (cantidadTotalArticulos > 0 && lblTotalCantCarrito != null)
                {
                    //Carrito carrito = (Carrito)Session["Carrito"];
                    //int cantidadTotalArticulos = carrito.ObtenerCantidadTotalArticulos();
                    lblTotalCantCarrito.Text = " (" + cantidadTotalArticulos.ToString() + ")";
                    panelContador.Update();
                }


                else
                {
                    lblTotalCantCarrito.Text = " (0)";
                }
            }


            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Session["Carrito"] != null)
            {
                Carrito carrito2 = (Carrito)Session["Carrito"];
                int cantidadTotalArticulos = carrito2.ObtenerCantidadTotalArticulos();
                if (lblTotalCantCarrito != null)
                {
                    lblTotalCantCarrito.Text = " (" + cantidadTotalArticulos.ToString() + ")";
                    panelContador.Update();
                }


                //lblMensaje.Text = Session["Carrito"].ToString();
            }
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                List<Articulo> lista = (List<Articulo>)Session["ListaArticulo"];
                List<Articulo> listaFiltrada = lista.Where(x => x.Nombre.ToUpper().Contains(txtBuscador.Text.ToUpper())).ToList();

                Session["ListaArticulosFiltrada"] = listaFiltrada;
                Session["ListaActiva"]=listaFiltrada;
                Session["inicio"] = string.IsNullOrEmpty(txtBuscador.Text) ? 0 : 1;

                // Redireccion
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        //version anterior con foreach
        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<Articulo> lista = (List<Articulo>)Session["ListaArticulo"];
        //        List<Articulo> listaFiltrada = lista.Where(x => x.Nombre.ToUpper().Contains(txtBuscador.Text.ToUpper())).ToList();
        //        Session.Add("ListaArticulosFiltrada", listaFiltrada);
        //        if (txtBuscador.Text == "")
        //        {
        //            Session["inicio"] = 0;
        //        }
        //        else { Session["inicio"] = 1; }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}