<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiniCarrito.aspx.cs" Inherits="ProyectoCarrito.MiniCarrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="containerPrincipal">
        <div class="containerCarrito">
            <h2>Mi carrito</h2>
            <button id="btnCerrar">X </button>
        </div>
        <div class="articulosCarrito">

            <ul>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
                <li>Articulo 1</li>
            </ul>

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
    </style>

    <script>

        //este agrego para que no tener que recargar la pagina para el evento abrir y cerrar
        window.addEventListener('DOMContentLoaded', function () {
            var hideButton = document.getElementById('btnCerrar');

            hideButton.addEventListener('click', function () {
                parent.postMessage('cerrarMiniCartContainer', '*');
            });
        });

    </script>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
