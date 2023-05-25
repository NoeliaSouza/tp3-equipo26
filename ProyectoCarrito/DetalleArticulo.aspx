<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="ProyectoCarrito.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style>

        body {
            background-color:#333;
        }

    .card {
      background-color: #222;
      max-width: 800px;
      margin: 0 auto;
      margin-top: 20px;
      border-radius: 10px;
      color: #fff;
    }

    .card-img-top {
      width: 100%;
      height: 400px;
      object-fit: cover;
      border-top-left-radius: 10px;
      border-top-right-radius: 10px;
    }

    .card-body {
      padding: 20px;
      text-align:center;
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
  </style>

  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="card">
          <img src="imagen.jpg" class="card-img-top" alt="Imagen del artículo">
          <div class="card-body">
            <h5 class="card-title">Nombre del artículo</h5>
            <div class="details">
              <p><span class="label">Código del articulo:</span> Código del artículo</p>
            </div>
            <div class="details">
              <p><span class="label">Marca:</span> Marca del artículo</p>
            </div>
            <div class="details">
              <p><span class="label">Categoría:</span> Categoría del artículo</p>
            </div>
            <div class="details">
              <p><span class="label">Precio:</span> $99.99</p>
            </div>
            <div class="add-to-cart">
              <button class="btn btn-primary">Agregar a carrito</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
       
    
</asp:Content>
