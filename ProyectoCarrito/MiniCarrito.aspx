<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiniCarrito.aspx.cs" Inherits="ProyectoCarrito.MiniCarrito" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="containerPrincipal">
        <div class="containerCarrito">
            <h2>Mi Carrito</h2>
            <button id="btnCerrar">X </button>
        </div>
        <div class="articulosCarrito">
            <%-- LLAMO AL UPDATE Y CREO EL REPETIDOR PARA QUE MUESTRA TODO LO QUE HAY EN CARRITO--%>
            <%--<asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="panelCarrito" runat="server" OnLoad="panelCarrito_Load">
                <ContentTemplate>--%>
           <%--<asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updatePanelCarrito" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
            <asp:Repeater ID="repCarrito" runat="server" OnItemDataBound="repCarrito_ItemDataBound">
                <ItemTemplate>


                    <div class="card mb-3 text-bg-dark p-3" style="max-width: 360px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <asp:Image ID="ImagenCarrito" CssClass="img-fluid rounded-start" runat="server" />
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h4 class="card-title"><%# Eval("Nombre") %></h4>
                                    <h5 class="card-title"><%# Eval("Marcas.NombreMarca") %></h5>
                                    <h6 class="card-title">$<%# Eval("Precio") %></h6>
                                    <div class="botonesYcantidad">

                                        <%-- PASAMOS LOS ID COMO ARGUMENTO A LOS BOTONES PARA QUE ENCUENTRE LOS ARTICULOS QUE TIENE Q RESTAR O SUMAR --%>
                                        <asp:Button ID="btnRestar" Text="-" runat="server" CssClass="btn btn-light" OnClick="restarArticulo_Click" CommandArgument='<%#Eval("Id")%>'/>
                                         <p>CANTIDAD: <asp:Label ID="lblCantidad" runat="server"></asp:Label></p>
                                        <asp:Button ID="btnSumar" Text="+" runat="server" CssClass="btn btn-light" OnClick="sumarArticulo_Click" CommandArgument='<%#Eval("Id")%>'/>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        <h4>Precio Total:$<asp:Label ID="lblPrecioTotal" runat="server" Text=""></asp:Label></h4>
        
            <%-- </ContentTemplate>

            </asp:UpdatePanel>--%>
        </div>
    </div>




    <style>
        #form1, html {
            background-color: black;
            color: white;
        }

        .containerCarrito {
            display: flex;
        }


        #btnCerrar {
            margin-left: 20vh;
        }

        .articulosCarrito {
            margin-top: 4vh;
        }

            .articulosCarrito ul {
                list-style-type: none;
                margin: 0;
                padding: 0;
            }

            .articulosCarrito li {
                margin-bottom: 1vh;
            }

        .card.mb-3 {
            display: flex;
        }

            .card.mb-3 .col-md-4 {
                flex-basis: 30%;
                max-width: 30%;
            }

            .card.mb-3 .col-md-8 {
                flex-basis: 70%;
                max-width: 70%;
            }

        .botonesYcantidad {
            display: flex;
            align-items: center;
        }

            .botonesYcantidad p {
                margin: 0 10px;
            }
    </style>

    <%--<script>

        //este agrego para que no tener que recargar la pagina para el evento abrir y cerrar
        window.addEventListener('DOMContentLoaded', function () {
            var hideButton = document.getElementById('btnCerrar');

            hideButton.addEventListener('click', function () {
                parent.postMessage('cerrarMiniCartContainer', '*');
            });
        });


        function asignarEventos() {
            var btnAgregarCesta = document.querySelectorAll(".btn.btn-success");
            btnAgregarCesta.forEach(function (button) {
                button.addEventListener("click", sumarAlCarrito);
            });
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            asignarEventos();
        });






    </script>--%>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
