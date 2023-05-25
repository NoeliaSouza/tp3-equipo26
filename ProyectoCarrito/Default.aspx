<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoCarrito.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>  
           <%if (Session["listaArticulosFiltrada"] == null || (int)Session["inicio"] == 0)
             {%>
               <h2>Bienvenido!</h2>
           <%}
             else
             {
           %> <h2> Resultado de la busqueda</h2>
           <%} %>
    </div>
   
    <%-- Css --%>
    <style>
      .card-img-top {
        height: 300px; /* Establece la altura deseada para las imágenes */
      }
    </style>
    


    <%-- Cards --%>
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <%  if (Session["listaArticulosFiltrada"] == null)
            {
                foreach (Dominio.Articulo art in ListaArticulo)
                {   /* Place holder si la imagen original falla */
                    string urlImagenOriginal = art.Imagenes[0].UrlImagen;
                    string urlImagenReemplazo = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png";

                    imgImagen.ImageUrl = urlImagenOriginal;
                    imgImagen.Attributes["onerror"] = "this.onerror=null;this.src='" + urlImagenReemplazo + "';";
        %>

                 <div class="col">
                      <div class="card">
                       <asp:Image ID="imgImagen" class="card-img-top" runat="server" CssClass="card-img-top"  style="height: 300px auto;" />
                             <div class="card-body">
                                <h5 class="card-title"><%: art.Nombre %> </h5>
                                <p class="card-text"><%: art.CodigoArticulo %></p>
                                <p class="card-text"><%:"$" + art.Precio %> </p>
                             <%-- Ver detalleeeeeeeeeeeeeeeeeeeeeeeeeeeee --%>
                                 
                               <a href="DetalleArticulo.aspx?id=<%:art.Id %>">Ver detalle</a>
                          
                                 <%--<asp:Button Text="Ver Detalle" ID="btnDetalle" CssClass="btn btn-primary" runat="server" OnClick="btnDetalle_Click" CommandArgument='<%: art.Id %>' />--%>
                                 <asp:Button Text="Agregar al carrito" ID="btnAgregarCarrito" CssClass="btn btn-success" runat="server" OnClick="btnAgregarCarrito_Click" CommandArgument='<%:art.Id %>' />
                             </div>
                          
                     </div>
                 </div>

        <%      }
            }
            else
            {
                ListaArticulo = (List<Dominio.Articulo>)Session["ListaArticulosFiltrada"];

                foreach (Dominio.Articulo art in ListaArticulo)
                {       /* Place holder si la imagen original falla */
                    string urlImagenOriginal = art.Imagenes[0].UrlImagen;
                    string urlImagenReemplazo = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png";

                    Imagen1.ImageUrl = urlImagenOriginal;
                    Imagen1.Attributes["onerror"] = "this.onerror=null;this.src='" + urlImagenReemplazo + "';";

        %>          <div class="col">
                        <div class="card">
                            <asp:Image ID="Imagen1" class="card-img-top" runat="server" CssClass="card-img-top"  style="height: 300px auto;"/>
                                <div class="card-body">
                                    <h5 class="card-title"><%: art.Nombre %> </h5>
                                    <p class="card-text"><%: art.CodigoArticulo %></p>
                                    <p class="card-text"><%:"$" + art.Precio %> </p>
                                    <%-- Ver detalle --%>
                                    <a href="DetalleArticulo.aspx?id=<%:art.Id %>">Ver detalle</a>
<%--                                    <asp:Button Text="Ver Detalle" ID="btnDetalle2" CssClass="btn btn-primary" runat="server" OnClick="btnDetalle_Click" CommandArgument='<%= art.Id %>' />--%>
                                </div>
                        </div>
                    </div>
        
        <%      }
                Session["ListaArticulosFiltrada"] = null;
            } %>
    </div>


</asp:Content>
