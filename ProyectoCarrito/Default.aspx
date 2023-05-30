<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoCarrito.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <%-- Css --%>
    <style>
        .card-img-top {
            height: 300px; /* Establece la altura deseada para las imágenes */
        }


        .h1 {
            color: aliceblue;
            margin-bottom: 60px;
            margin-top: 30px;
            text-align: center
        }

        .filtros{
            margin-top:4.5vh;
            position: fixed;
            top: 0;
            left: 0;
            height: 100%;
            width: 30vh; 
            background-color: black; 
        }

    </style>





    <%-- Agregamos updatePanel --%>
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>



    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="filtros">
                <%--Filtro comun--%>

                <div class="row">
                    <div class="col-6">
                        <div class="mb-3">
                            <asp:Label Text="Buscar" runat="server" Style="color: white;" />
                            <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />

                        </div>
                    </div>
                    
                </div>

                <div class="row">
                    <%-- Checkbox --%>
                    <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                        <div class="mb-3">
                            <asp:CheckBox Text="Filtro Avanzado" runat="server" CssClass="" ID="chkAvanzado" Style="color: white;" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
                        </div>
                    </div>
                </div>
                <%-- Termina filtro comun --%>



                <%-- Filtro avanzado --%>
                <%-- Tambien se podría directamente preguntar si chkAvanzado.checked esta en true y ya --%>

                <%if (FiltroAvanzado)
                    {%>

                <div class="row">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:Label Text="Campo" ID="ddlCampo1" runat="server" Style="color: white;" />
                            <asp:DropDownList runat="server"  AutoPostBack="true" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                                <asp:ListItem Text="Precio" />
                                <asp:ListItem Text="Nombre" />
                                <asp:ListItem Text="Marcas" />
                                <asp:ListItem Text="Categorias" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                    <div class="row">
                        <div class="col-3">
                        <div class="mb-3">
                            <asp:Label Text="Criterio" Style="color: white;" runat="server" />
                            <asp:DropDownList ID="ddlCriterio" runat="server" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    </div>
                    

                    <div class="row">
                         <div class="col-3">
                        <div class="mb-3">
                            <asp:Label Text="Filtro" runat="server" Style="color: white;" />
                            <asp:TextBox runat="server" ID="txtFiltroAvanzado"  />
                        </div>
                    </div>
                    </div>
                   

                    <div class="row">
                        <div class="col-3" style="align-items: center;">
                            <div class="mb-3">
                                <asp:Button Text="Buscar" runat="server" ID="btnBuscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />

                            </div>
                        </div>
                    </div>
                    <%-- Termina filtro avanzado --%>

                    <%} %>
                </div>
                <%-- Terminan filtros --%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>





    <asp:UpdatePanel ID="panel1" runat="server" OnLoad="panel1_Load">
        <ContentTemplate>
            <%-- Cards --%>
            <div class="row row-cols-1 row-cols-md-3 g-4">
                <h1 class="h1">Nuestros Articulos</h1>
                <%-- Repeater --%>
                <div class="row row-cols-3">

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
                                        <%--<a href="DetalleArticulo.aspx?id=<%# Eval("Id") %>" class="btn btn-primary">Ver detalle</a>--%>
                                        <%-- Agregar al carrito --%>
                                        <%--<a href="#" class="btn btn-success">Agregar al carrito</a>--%>
                                        <asp:Button Text="Ver Detalle" ID="btnDetalle" CssClass="btn btn-primary" runat="server" OnClick="btnDetalle_Click" CommandArgument='<%#Eval("Id")%>' />
                                        <asp:Button Text="Agregar al carrito" ID="btnEjemplo" CssClass="btn btn-success" runat="server" OnClick="btnAgregarCarrito_Click" CommandArgument='<%# Eval("Id") %>' />

                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>




    <%-- Arreglo visual luego de agregar el updatePanel (se veia todo mas chico) --%>
    <style>
        @media (min-width: 768px) {
            .row-cols-md-3 > * {
                flex: 0 0 auto;
                width: 100%;
            }
        }

        .row {
            --bs-gutter-y: 1.5rem;
        }
    </style>


    <%-- volvemos a asignar los eventos al click del carrito-master --%>
    <script>
        function asignarEventos() {
            var btnAgregarCesta = document.querySelectorAll(".btn.btn-success");
            btnAgregarCesta.forEach(function (button) {
                button.addEventListener("click", sumarAlCarrito);
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            asignarEventos();
        });
    </script>


</asp:Content>
