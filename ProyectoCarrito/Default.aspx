<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoCarrito.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Bienvenido! </h2>

    <%-- Cards --%>
    <div class="row row-cols-1 row-cols-md-3 g-4">

        <% 
            foreach(Dominio.Articulo art in ListaArticulo)
            {%>

                 <div class="col">
                     <div class="card">
                         <img src="<%:art.imagenes[0] %>" class="card-img-top" alt="...">
                         <div class="card-body">
                             <h5 class="card-title"> <%: art.CodigoArticulo %> </h5>
                             <p class="card-text"> <%: art.Descripcion %></p>
                             <%-- Ver detalle --%>
                             <a href="#">Ver detalle</a>
                         </div>
                     </div>
                 </div>

        <%  }  %>
    </div>



</asp:Content>
