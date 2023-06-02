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

        .filtros {
            /*margin-top:4.5vh;*/
            position: fixed;
            top: 0;
            left: 0;
            height: 100%;
            background-color: black;
        }

        .busquedaAvanzada {
            margin: 0px;
            margin-top: 70px;
            color: white;
            float: left;
            padding-inline-start: auto;
        }

        .lineaDivisoria {
            width: 100%;
            height: 5px;
            background-color: lightgray;
        }

        .filtros {
            width: 15%;
            float: left;
        }

        .cards-container {
            width: 90%;
            float: right;
        }

        .estiloFiltros {
        }

        .filtrosAv {
            width: 60%;
            margin-left: 5vh;
        }

        .filtroRapido {
            display: none;
        }

        .lbOrden {
            margin-top: 10px;
            margin-left: 10px;
            color: whitesmoke;
        }

        .ordenarLista {
            color: whitesmoke;
            margin-right: auto;
            margin-left: 40px;
            margin-top: 0px;
            padding-left: 0px;
        }

        .rango {
            color: whitesmoke;
            margin: 0px auto;
            align-content: center;
            align-items: center;
            width: 75%;
        }
    </style>





    <%-- Agregamos updatePanel --%>
    <%--<asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>--%>


    <div class="filtros" id="filtros" style="display: none;" runat="server">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>


                <%--Filtro comun--%>
                <div class="estiloFiltros">

                    <div class="row filtroRapido" style="margin-top: 5vh;">
                        <div class="col-6 filtrosAv ">
                            <div class="mb-3">
                                <asp:Label Text="Buscar" runat="server" Style="color: white;" />
                                <asp:TextBox runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="filtro_TextChanged" CssClass="form-control" />

                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <%-- Checkbox --%>
                        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
                            <div class="busquedaAvanzada">

                                <asp:CheckBox Text="Busqueda Avanzada" runat="server" CssClass="" ID="chkAvanzado" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                    <%-- Termina filtro comun --%>



                    <%-- Filtro avanzado --%>
                    <%-- Tambien se podría directamente preguntar si chkAvanzado.checked esta en true y ya --%>

                    <%if (chkAvanzado.Checked)
                        {%>

                    <div class="row">
                        <div class="col-3 filtrosAv " style="margin-top: 20vh auto;">
                            <div class="mb-3">
                                <asp:Label Text="Campo" ID="ddlCampo1" runat="server" Style="color: white;" />
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged" CssClass="form-control text-center">
                                    <asp:ListItem Text="Precio" />
                                    <asp:ListItem Text="Nombre" />
                                    <asp:ListItem Text="Marcas" />
                                    <asp:ListItem Text="Categorias" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-3 filtrosAv ">
                            <div class="mb-3">
                                <asp:Label Text="Criterio" Style="color: white;" runat="server" />
                                <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-control text-center">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-3 filtrosAv ">
                            <div class="mb-3">
                                <asp:Label Text="Filtro" runat="server" Style="color: white;" />
                                <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-3 filtrosAv " style="align-items: center; margin-top: 4vh;">
                            <div class="mb-3">
                                <asp:Button Text="Buscar" runat="server" ID="btnBuscar" CssClass="btn btn-outline-light form-control " OnClick="btnBuscar_Click" />

                            </div>
                        </div>
                    </div>
                    <%-- Termina filtro avanzado --%>

                    <%} %>

                    <%-- Filtro por rango de precio--%>
                    <br />
                    <div class="lineaDivisoria"></div>
                    <br />
                    <div class="rango">
                        <label class="labelPrecio">Filtrar valor entre:</label>
                        <asp:TextBox class="rangoInicial" runat="server" ID="txtRangoInicial" CssClass="form-control me-2" type="search" placeholder="$ Minimo" aria-label="Minimo">
                        </asp:TextBox>
                        <label class="labelSeparador">- </label>
                        <asp:TextBox class="rangoFinal" runat="server" ID="txtRangoFinal" CssClass="form-control me-2" type="search" placeholder="$ Maximo" aria-label="Maximo">
                        </asp:TextBox>
                        <asp:Button Style="margin-top: 15px;" Text="Buscar" runat="server" ID="btnRango" CssClass="btn btn-outline-light" type="submit" OnClick="btnRango_Click" />
                    </div>

                    <br />

                    <%-- Ordenar la lista mostrada segun criterio--%>
                    <label class="lbOrden">Ordenar por: </label>
                    <div class="ordenarLista">


                        <asp:RadioButton class="orden" GroupName="ordenar" ID="rbRelevancia" runat="server" Text=" Alfabeticamente" AutoPostBack="true" OnCheckedChanged="rbRelevancia_CheckedChanged" />
                        <br />
                        <asp:RadioButton class="orden" GroupName="ordenar" ID="rbAscendente" runat="server" Text=" Menor Precio" AutoPostBack="true" OnCheckedChanged="rbAscendente_CheckedChanged" />
                        <br />
                        <asp:RadioButton class="orden" GroupName="ordenar" ID="rbDescendente" runat="server" Text=" Mayor Precio" AutoPostBack="true" OnCheckedChanged="rbDescendente_CheckedChanged" />

                    </div>
                </div>
                <%-- Estilo filtros --%>
            
           %>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- Terminan filtros --%>
    </div>



    <asp:UpdatePanel ID="panel1" runat="server" OnLoad="panel1_Load">
        <ContentTemplate>
            <%-- Cards --%>
            <div class="cards-container">
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
                                            <p class="card-text">$<%# Eval("Precio")%></p>
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

        function toggleFiltros() {
            var filtros = document.getElementById('filtros');
            filtros.style.display = (filtros.style.display === 'none') ? 'block' : 'none';
            return false;
        }


        function asignarEventos() {
            var btnAgregarCesta = document.querySelectorAll(".btn.btn-success");
            btnAgregarCesta.forEach(function (button) {
                button.addEventListener("click", sumarAlCarrito);
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            asignarEventos();
        });


        function redirectToListado() {
            window.location.href = "Listado.aspx";
        }




    </script>


</asp:Content>
