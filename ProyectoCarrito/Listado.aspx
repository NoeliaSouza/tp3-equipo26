<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Listado.aspx.cs" Inherits="ProyectoCarrito.Listado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="h1">Listado de articulos</h1>
    <%-- Grilla --%>
    <%-- <style>

        .oculto {
            display: none;
        }

    </style>--%>

    <style>
        .h1
         {
            
            color: aliceblue;
            margin-bottom:60px;
            margin-top:30px;
            text-align:center

        }
        .gridview-white-background {
            background-color: white;
        }
    </style>

    <asp:GridView ID="dgvArticulos" runat="server" CssClass="table table-dark gridview-white-background" DataKeyNames="Id" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged">

        <Columns>
            <%--<asp:BoundField  HeaderText="Id" DataField="Id" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"/>--%>
            <asp:BoundField HeaderText="Nombre de artículo" DataField="Nombre" />
            <asp:BoundField HeaderText="Codigo de artículo" DataField="CodigoArticulo" />
            <asp:BoundField HeaderText="Marca" DataField="Marcas.NombreMarca" />
            <asp:BoundField HeaderText="Categoría" DataField="Categorias.NombreCategoria" />
            <asp:BoundField HeaderText="urlImagen" DataField="Imagenes[0].UrlImagen" Visible="false" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:CommandField ShowSelectButton="true" SelectText="Ver Detalle" HeaderText="Acción" />
        </Columns>

    </asp:GridView>


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