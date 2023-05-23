<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoCarrito.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Bienvenido! </h2>

    <%-- Cards --%>
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <% 
            foreach (Dominio.Articulo art in ListaArticulo)
            {   /* Place holder si la imagen original falla */
                string urlImagenOriginal = art.Imagenes[0].UrlImagen;
                string urlImagenReemplazo = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png";

                imgImagen.ImageUrl = urlImagenOriginal;
                imgImagen.Attributes["onerror"] = "this.onerror=null;this.src='" + urlImagenReemplazo + "';";

        %>

        <div class="col">
            <div class="card">
                <asp:Image ID="imgImagen" class="card-img-top" runat="server" CssClass="card-img-top"  style="height: 300px;"/>
                             <div class="card-body">
                             <h5 class="card-title"><%: art.Nombre %> </h5>
                             <p class="card-text"><%: art.CodigoArticulo %></p>
                             <p class="card-text"><%:"$" + art.Precio %> </p>
                             <%-- Ver detalle --%>
                             <a href="#">Ver detalle</a>
                         </div>
            </div>
        </div>

        <%  }  %>
    </div>


</asp:Content>
