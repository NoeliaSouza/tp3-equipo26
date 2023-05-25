using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace ProyectoCarrito
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                
                List<Articulo> lista = (List<Articulo>)Session["ListaArticulo"];
                List<Articulo> listaFiltrada = lista.Where(x => x.Nombre.ToUpper().Contains(txtBuscador.Text.ToUpper())).ToList();
                Session.Add("ListaArticulosFiltrada", listaFiltrada);

                if (txtBuscador.Text == "")
                {
                    Session["inicio"] = 0;
                }
                else { Session["inicio"] = 1; }
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
    }
}