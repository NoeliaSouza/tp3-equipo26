
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
using System.Web.UI.HtmlControls;

namespace ProyectoCarrito
{
    public partial class Home : System.Web.UI.Page
    {
        //public bool FiltroAvanzado { get; set; }
        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //version anterior
            //ArticuloNegocio negocio = new ArticuloNegocio();
            //ListaArticulo = negocio.listarConSP();
            //FiltroAvanzado = false;
            //FiltroAvanzado = chkAvanzado.Checked;

            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("ListaArticulo", negocio.listarConSP());
                ListaArticulo = (List<Articulo>)Session["ListaArticulo"];

                if (Session["inicio"] == null || (int)Session["inicio"] == 0)
                { Session["listaActiva"] = ListaArticulo; }
                Session["inicio"] = 0;

                if (Session["Carrito"] == null)
                {
                    Carrito carrito = new Carrito();
                    Session["Carrito"] = carrito;
                }

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

                cargarCboCriterio("Mayor a", "Menor a", "Igual a");
                limpiarRb();





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

        //VER DETALLE
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
                throw ex;
            }
        }

        //AGREGAMOS ARTICULOS A LA LISTA DE ARTICULOS DE LA CLASE CARRITO
        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(((Button)sender).CommandArgument);
            Carrito carrito = (Carrito)Session["Carrito"];
            Articulo articulo = new Articulo();
            ListaArticulo = (List<Articulo>)Session["ListaArticulo"];
            articulo = ListaArticulo.Find(a => a.Id == id);
            carrito.AgregarArticulo(articulo);
            Session["Carrito"] = carrito;


        }

        //protected void btnEjemplo_Click(object sender, EventArgs e)
        //{
        //    string valor = ((Button)sender).CommandArgument;
        //}

        // Funcion para actualizar y llamar a la funcion despues del updatePanel para que vuelva a asignar los eventos 
        protected void panel1_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "AsignarEventos", "asignarEventos();", true);
            }
        }


        /* Filtro comun busca por txtFiltro*/
        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["ListaArticulo"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.CodigoArticulo.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            repRepetidor.DataSource = listaFiltrada;
            repRepetidor.DataBind();
        }

        /* Filtro avanzado. Cada vez que chekeamos filtro avanzado, cambiamos el valor del booleano */
        /* El txtFiltro va a estar activado o desactivado depende del valor del booleano Filtro Avanzado */
        //protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        //{
        //    FiltroAvanzado = chkAvanzado.Checked;
        //    txtFiltro.Enabled = !FiltroAvanzado;//txtFiltro comun
        //}

        /* cada vez que cambie, cargue el campo de criterio */
        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlCriterio.Items.Clear();
            txtFiltroAvanzado.Enabled = true;
            string opcion = ddlCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cargarCboCriterio("Mayor a", "Menor a", "Igual a");
            }
            else if (opcion == "Categorias")
            {
                txtFiltroAvanzado.Enabled = false;
                cargarCboCategorias();
            }
            else if (opcion == "Marcas")
            {
                txtFiltroAvanzado.Enabled = false;
                cargarCboMarcas();
            }
            else
            {
                cargarCboCriterio("Comienza con", "Termina con", "Contiene");
            }



        }

        /* btn buscar filtro avanzado */
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();



                if (validarFiltro())
                {
                    string campo = ddlCampo.SelectedItem.ToString();
                    string criterio = ddlCriterio.SelectedItem.ToString();
                    string filtro = txtFiltroAvanzado.Text;


                    if (ddlCampo.SelectedItem.ToString() == "Marcas" || ddlCampo.SelectedItem.ToString() == "Categorias")
                    {
                        filtro = criterio;
                    }

                    //Verificamoss si el filtro esta vacio
                    if (string.IsNullOrWhiteSpace(filtro))
                    {  // Cargar la lista completa de arts


                        repRepetidor.DataSource = Session["ListaArticulo"];
                        repRepetidor.DataBind();
                    }
                    else
                    {   // Si el filtro no esta vacio, filtra

                        Session["ListaActiva"] = negocio.filtrar(campo, criterio, filtro);
                        repRepetidor.DataSource = Session["ListaActiva"];
                        //repRepetidor.DataSource = negocio.filtrar(campo, criterio, filtro);
                        repRepetidor.DataBind();


                    }
                }

                txtFiltroAvanzado.Text = string.Empty;
                limpiarRb();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        private bool validarFiltro()
        {
            if (ddlCampo.SelectedIndex < 0 || ddlCampo.SelectedIndex < 0)
            {
                lblMensajeError.Text = "Debe seleccionar un campo y un criterio de búsqueda.";
                lblMensajeError.Visible = true;
                updatePanelMensajeError.Update();
                timerMensajeError.Enabled = true;
                //("Debe seleccionar un campo y un criterio de búsqueda.", "Error de selección", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    lblMensajeError.Text = "Ingrese un filtro para la búsqueda.";
                    lblMensajeError.Visible = true;
                    updatePanelMensajeError.Update();
                    timerMensajeError.Enabled = true;
                    // ("Por favor ingresa un filtro para numéricos");
                    return false;
                }
                if (!soloNumeros(txtFiltroAvanzado.Text))
                {
                    lblMensajeError.Text = "Por favor ingrese un filtro para numéricos";
                    lblMensajeError.Visible = true;
                    updatePanelMensajeError.Update();
                    timerMensajeError.Enabled = true;
                    //("Solo se aceptan números para filtrar un campo numerico");
                    return false;
                }
                int filtroAvanzado;
                if (int.TryParse(txtFiltroAvanzado.Text, out filtroAvanzado))
                {
                    if (filtroAvanzado < 0)
                    {
                        lblMensajeError.Text = "El filtro para números no puede ser menor que 0";
                        lblMensajeError.Visible = true;
                        updatePanelMensajeError.Update();
                        timerMensajeError.Enabled = true;
                        return false;
                    }
                }
                if(ddlCriterio.SelectedItem.ToString()=="Menor a")
                {
                    if(int.TryParse(txtFiltroAvanzado.Text, out filtroAvanzado))
                    {
                        if (filtroAvanzado == 0)
                        {
                            lblMensajeError.Text = "El filtro para números no puede ser menor que 0";
                            lblMensajeError.Visible = true;
                            updatePanelMensajeError.Update();
                            timerMensajeError.Enabled = true;
                            return false;
                        }
                        
                    }
                }


            }

            if (ddlCampo.SelectedItem.ToString() == "Nombre")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    lblMensajeError.Text = "Ingrese un filtro para la búsqueda.";
                    lblMensajeError.Visible = true;
                    updatePanelMensajeError.Update();
                    timerMensajeError.Enabled = true;
                    return false;
                }
                
            }


            return true;
        }


        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if ((char.IsNumber(caracter)))
                    return true;
            }
            return false;
        }

        private void cargarCboCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> categorias = new List<Categoria>();
            categorias = negocio.listar();

            foreach (Categoria categoria in categorias)
            {
                ddlCriterio.Items.Add(categoria.NombreCategoria);
            }

        }

        private void cargarCboMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> marcas = new List<Marca>();
            marcas = negocio.listar();

            foreach (Marca marca in marcas)
            {
                ddlCriterio.Items.Add(marca.NombreMarca);
            }
        }


        private void cargarCboCriterio(string criterio1, string criterio2, string criterio3)
        {
            ddlCriterio.Items.Add(criterio1);
            ddlCriterio.Items.Add(criterio2);
            ddlCriterio.Items.Add(criterio3);
        }


        //FALTAN MUCHAS VALIDACIONES, Y GENERA ERROR POR EXCEPCIONES EN VARIAS PARTES.


        //protected void btnRango_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<Articulo> lista = (List<Articulo>)Session["ListaActiva"];
        //        List<Articulo> filtrada = new List<Articulo>();

        //        //Session["ListaArticulosFiltradaxRango"]="No se encontraron resultados";



        //        if (txtRangoInicial.Text != "" && txtRangoFinal.Text != "")
        //        {
        //            foreach (Articulo a in lista)
        //            {
        //                if (a.Precio >= int.Parse(txtRangoInicial.Text)
        //                    && a.Precio <= int.Parse(txtRangoFinal.Text))
        //                {
        //                    filtrada.Add(a);
        //                }
        //            }
        //        }
        //        else if (txtRangoInicial.Text != "" && txtRangoFinal.Text == "")
        //        {
        //            foreach (Articulo a in lista)
        //            {
        //                if (a.Precio >= int.Parse(txtRangoInicial.Text))
        //                {
        //                    filtrada.Add(a);
        //                }
        //            }
        //        }
        //        else if (txtRangoFinal.Text != "" && txtRangoInicial.Text == "")
        //        {
        //            foreach (Articulo a in lista)
        //            {
        //                if (a.Precio <= int.Parse(txtRangoFinal.Text))
        //                {
        //                    filtrada.Add(a);
        //                }
        //            }
        //        }

        //        else {

        //            txtRangoFinal.Attributes["placeholder"] = "Carga 1 o ambos campos obligatoriamente";
        //            txtRangoInicial.Attributes["placeholder"] = "Carga 1 o ambos campos obligatoriamente";


        //        }

        //        // Session["ListaArticulo"] = filtrada;
        //        // Session["inicio"] = 1;
        //        Session["ListaActiva"]= filtrada;
        //        repRepetidor.DataSource = filtrada;
        //        repRepetidor.DataBind();
        //        limpiarRb();

        //        // Redireccion
        //        //Response.Redirect("Default.aspx");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void limpiarRb()
        {
            if (rbRelevancia.Checked)
            {
                rbRelevancia.Checked = false;
            }
            else if (rbAscendente.Checked)
            {
                rbAscendente.Checked = false;
            }
            else if (rbDescendente.Checked)
            {
                rbDescendente.Checked = false;
            }
        }

        protected void rbRelevancia_CheckedChanged(object sender, EventArgs e)
        {   List<Articulo> listaArticulos = new List<Articulo>();
            if (Session["ListaActiva"] == null)
            {
               listaArticulos = (List<Articulo>)Session["ListaArticulo"];
            }
            else
            {
               listaArticulos = (List<Articulo>)Session["ListaActiva"];

            }
            if (rbRelevancia.Checked) {
                listaArticulos = listaArticulos.OrderBy(a => a.Nombre).ToList();
                Session["ListaActiva"] = listaArticulos;
                repRepetidor.DataSource = listaArticulos;
                repRepetidor.DataBind();
                
            }
        }

        protected void rbAscendente_CheckedChanged(object sender, EventArgs e)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            if (Session["ListaActiva"] == null)
            {
                listaArticulos = (List<Articulo>)Session["ListaArticulo"];
            }
            else
            {
                listaArticulos = (List<Articulo>)Session["ListaActiva"];

            }

            if (rbAscendente.Checked)
            {
                listaArticulos = listaArticulos.OrderBy(a => a.Precio).ToList();
                Session["ListaActiva"] = listaArticulos;
                repRepetidor.DataSource = listaArticulos;
                repRepetidor.DataBind();
            }
        }

        protected void rbDescendente_CheckedChanged(object sender, EventArgs e)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            if (Session["ListaActiva"] == null)
            {
                listaArticulos = (List<Articulo>)Session["ListaArticulo"];
            }
            else
            {
                listaArticulos = (List<Articulo>)Session["ListaActiva"];

            }
            if (rbDescendente.Checked)
            {
                listaArticulos = listaArticulos.OrderByDescending(a => a.Precio).ToList();
                Session["ListaActiva"] = listaArticulos;
                repRepetidor.DataSource = listaArticulos;
                repRepetidor.DataBind();
            }
        }

        /*msj */
        protected void timerMensajeError_Tick(object sender, EventArgs e)
        {
            lblMensajeError.Visible = false;
            timerMensajeError.Enabled = false;
            updatePanelMensajeError.Update();
        }

    }
}