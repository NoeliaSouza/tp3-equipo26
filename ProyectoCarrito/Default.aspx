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
        %>
        <h2>Resultado de la busqueda</h2>
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
        <asp:Repeater ID="repRepetidor" runat="server" OnItemDataBound="repRepetidor_ItemDataBound">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <asp:Image ID="imgImagen" class="card-img-top" runat="server" CssClass="card-img-top" Style="height: 300px auto;" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %> </h5>
                            <p class="card-text"><%# Eval("CodigoArticulo")%></p>
                            <p class="card-text"><%# Eval("Precio")%> </p>
                            <%-- Ver detalleeeeeeeeeeeeeeeeeeeeeeeeeeeee --%>
                            <a href="DetalleArticulo.aspx?id=<%# Eval("Id") %>" class="btn btn-primary">Ver detalle</a>
                            <%-- Agregar al carrito --%>
                            <%--<a href="#" class="btn btn-success">Agregar al carrito</a>--%>
                            <%--<asp:Button Text="Ver Detalle" ID="btnDetalle" CssClass="btn btn-primary" runat="server" OnClick="btnDetalle_Click" CommandArgument='<%: art.Id %>' />--%>
                            <asp:Button Text="Agregar al carrito" ID="btnAgregarCarrito" CssClass="btn btn-success" runat="server" OnClick="btnAgregarCarrito_Click" CommandArgument='<%:art.Id %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
