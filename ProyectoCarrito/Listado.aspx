<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Listado.aspx.cs" Inherits="ProyectoCarrito.Listado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="h1">Carrito de compras</h1>
    <%-- Grilla --%>
    <%-- <style>

        .oculto {
            display: none;
        }

    </style>--%>

    <style>
        .h1 {
            color: aliceblue;
            margin-bottom: 60px;
            margin-top: 30px;
            text-align: center
        }

        .gridview-white-background {
            background-color: white;
        }
    </style>

    <%--<asp:GridView ID="dgvArticulos" runat="server" CssClass="table table-dark gridview-white-background" DataKeyNames="Id" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged">

        <Columns>--%>
    <%--<asp:BoundField  HeaderText="Id" DataField="Id" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"/>--%>
    <%--   <asp:BoundField HeaderText="Nombre de artículo" DataField="Nombre" />
            <asp:BoundField HeaderText="Codigo de artículo" DataField="CodigoArticulo" />
            <asp:BoundField HeaderText="Marca" DataField="Marcas.NombreMarca" />
            <asp:BoundField HeaderText="Categoría" DataField="Categorias.NombreCategoria" />

            <asp:BoundField HeaderText="urlImagen" DataField="Imagenes[0].UrlImagen" Visible="false" />

            <asp:BoundField HeaderText="Precio" DataField="Precio" />--%>


    <%--<asp:CommandField ShowSelectButton="true" SelectText="Ver Detalle" HeaderText="Acción" />--%>
    <%-- </Columns>

    </asp:GridView>--%>




    <%@ Import Namespace="Dominio" %>

    <%-- Titulos --%>
    <div class="card mb-3" style="background-color: black;">
        <div class="row g-0">
            <div class="col-md-12" style="color: white; font-weight: bold;">
                <div class="card-body d-flex">
                    <div class="col-md-2"></div>
                    <!-- Columna vacía sin título -->
                    <div class="col-md-2">
                        <p class="card-title itemsCarrito">Nombre</p>
                    </div>
                    <div class="col-md-2">
                        <p class="card-title itemsCarrito">Marca</p>
                    </div>
                    <div class="col-md-2">
                        <p class="card-title itemsCarrito">Precio</p>
                    </div>
                    <div class="col-md-2">
                        <p class="card-title itemsCarrito">Cantidad</p>
                    </div>
                    <div class="col-md-2">
                        <p class="card-title itemsCarrito">Total</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<asp:ScriptManager ID="script3" runat="server"></asp:ScriptManager>--%>
    <asp:UpdatePanel ID="updatePanelCarrito" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Repeater ID="repCarrito" runat="server" OnItemDataBound="repCarrito_ItemDataBound">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="row g-0">

                            <div class="col-md-12">
                                <div class="card-body d-flex">
                                    <div class="col-md-2">
                                        <asp:Image ID="imgImagen" runat="server" CssClass="img-fluid rounded-start" Style="max-height: 15vh; width: 100%;" />
                                    </div>
                                    <div class="col-md-2 itemsCarrito">
                                        <p class="card-text"><%# Eval("Nombre") %></p>
                                    </div>
                                    <div class="col-md-2 itemsCarrito">
                                        <p class="card-text"><%# Eval("Marcas.NombreMarca") %></p>
                                    </div>
                                    <div class="col-md-2 itemsCarrito">
                                        <p class="card-text">$ <%# Eval("Precio") %></p>
                                    </div>
                                    <div class="col-md-2 itemsCarrito d-flex justify-content-evenly align-items-center ">

                                        <asp:Button ID="btnRestar" Text="<" runat="server" CssClass="btn btn-sm   btn-dark" CommandArgument='<%#Eval("Id")%>' OnClick="btnRestar_Click" />
                                        <%--<p class="card-text"><%# carrito.ObtenerCantidadArticulo(((Articulo)Container.DataItem).Id) %></p>--%>
                                        <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                                        <asp:Button ID="btnSumar" Text=">" runat="server" CssClass="btn btn-sm   btn-dark" CommandArgument='<%#Eval("Id")%>' OnClick="btnSumar_Click" />

                                    </div>
                                    <div class="col-md-2 itemsCarrito">
                                        <asp:Label ID="lblTotalCantXart" runat="server"></asp:Label>

                                        <%--<p class="card-text">$ <%# ((decimal)Eval("Precio")) * carrito.ObtenerCantidadArticulo(((Articulo)Container.DataItem).Id) %></p>--%>
                                    </div>
                                    <%--<div class="col-md-2">
                                <a href="Detalle.aspx" class="btn btn-primary">Ver Detalle</a>
                            </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div style="display: flex; justify-content: flex-end;">
                <h4 style="color: white;">TOTAL: $<asp:Label ID="lblPrecioTotal" runat="server" Text=""></asp:Label></h4>

                <%--<h4 style="color: white;">TOTAL COMPRA: $<%: carrito.PrecioTotal %></h4>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <style>
        .itemsCarrito {
            margin: auto;
            text-align: center;
        }
    </style>



</asp:Content>

<%--public int Id { get; set; }
        public string CodigoArticulo  { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
        public Marca Marcas { get; set; }

        public Categoria Categorias { get; set; }
        //Lista de imagenes por si hay mas de 1
        public List<string> imagenes { get; set; }
        public Articulo()
        {
            imagenes = new List<string>();
        }--%>
