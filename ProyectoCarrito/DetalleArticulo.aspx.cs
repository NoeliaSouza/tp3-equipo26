﻿using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ProyectoCarrito
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Articulo articulo = new Articulo();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {

                    //TRAIGO DESDE DEFAULT EL ID DEL ARTICULO QUE QUIERO MOSTRAR A TRAVES DEL CLICK EN EL BUTTON
                    int id = int.Parse(Request.QueryString["id"]);

                    //GENERO UNA LISTA DE ARTICULOS CON LO QUE HAY EN EL SESSION DE ListaArticulo
                    List<Articulo> ListaArticulo = (List<Articulo>)Session["ListaArticulo"];


                    //ENCUENTRO EL ID DEL ARTICULO EN LA LISTA Y COPIO EL OBJETO EN EL NUEVO OBJETO ARTICULO
                    articulo = ListaArticulo.Find(x => x.Id == id);
                    Session["UltimoIdArticulo"] = id;

                }
                
            }
            else
            {   //validacion recarga de pag.
                int ultimoIdArticulo = (int)Session["UltimoIdArticulo"];
                List<Articulo> ListaArticulo = (List<Articulo>)Session["ListaArticulo"];
                articulo = ListaArticulo.Find(x => x.Id == ultimoIdArticulo);

            }



        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {



                
                Carrito carrito = (Carrito)Session["Carrito"];


                
                carrito.AgregarArticulo(articulo);
                Session["Carrito"] = carrito;


            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}