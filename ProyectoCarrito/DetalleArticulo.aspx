<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="ProyectoCarrito.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        body {
            background-color: #333;
        }

        .card {
            background-color: #222;
            max-width: 800px;
            margin: 0 auto;
            margin-top: 20px;
            border-radius: 10px;
            color: #fff;
            margin-bottom: 20px;
        }

        .card-img-top {
            width: 100%;
            height: auto;
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
        }


        .card-body {
            padding: 20px;
            text-align: center;
        }

        .details {
            margin-bottom: 10px;
        }

        .label {
            font-weight: bold;
        }

        .price {
            font-size: 24px;
        }

        .card-title {
            text-decoration: underline;
        }

        .add-to-cart {
            margin-top: 20px;
        }

        .carousel-control-prev-icon {
            color: black;
        }
        carousel-control-next {
            color:black;
        }
    </style>
    <%-- Carga de imagen --%>
    <%
        if (articulo.Id != 0)
        {

            //string urlImagenOriginal = articulo.Imagenes[0].UrlImagen;
            //string urlImagenReemplazo = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png";

            //Imagen1.ImageUrl = urlImagenOriginal;
            //Imagen1.Attributes["onerror"] = "this.onerror=null;this.src='" + urlImagenReemplazo + "';";
        }


    %>
    <%
        if (articulo.Id != 0)
        {
                %>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <%--<img src="imagen.jpg" class="card-img-top" alt="Imagen del artículo">--%>

                    <div id="carouselExample" class="carousel slide">
                        <div class="carousel-inner">
                            <% for (int i = 0; i < articulo.Imagenes.Count; i++)
                                { %>
                            <div class="carousel-item <%= i == 0 ? "active" : "" %>">
                                <img src="<%= articulo.Imagenes[i].UrlImagen %>" class="d-block w-100" alt="Imagen <%= i %>" onerror="this.src='https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/681px-Placeholder_view_vector.svg.png';">
                            </div>
                            <% } %>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExample" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#carouselExample" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>

                    <div class="card-body">
                        <h5 class="card-title"><%: articulo.Nombre %></h5>
                        <div class="details">
                            <p><span class="label">Código del articulo:</span> <%: articulo.CodigoArticulo %></p>
                        </div>
                        <div class="details">
                            <p><span class="label">Marca:</span> <%: articulo.Marcas.NombreMarca %></p>
                        </div>
                        <div class="details">
                            <p><span class="label">Categoría:</span> <%:articulo.Categorias.NombreCategoria %></p>
                        </div>
                        <div class="details">
                            <p><span class="label">Precio:</span> <%:"$" + articulo.Precio %></p>
                        </div>
                        <div class="add-to-cart">
                            <button class="btn btn-primary">Agregar a carrito</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%  }
    %>

    <%else
        {
            %>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <%--<img src="imagen.jpg" class="card-img-top" alt="Imagen del artículo">--%>
                    <asp:Image ID="Image1" class="card-img-top" runat="server" CssClass="card-img-top" />
                    <div class="card-body">
                        <h5 class="card-title">No hay articulo seleccionado</h5>
                        <div class="details">
                            <p><span class="label">Código del articulo:</span> </p>
                        </div>
                        <div class="details">
                            <p><span class="label">Marca:</span> ></p>
                        </div>
                        <div class="details">
                            <p><span class="label">Categoría:</span> ></p>
                        </div>
                        <div class="details">
                            <p><span class="label">Precio:</span></p>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <%  } %>
</asp:Content>
