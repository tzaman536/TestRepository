<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <ul id="fieldlist">
    <li>
        <label for="categories">Catergories:</label>
        <%= Html.Kendo().ComboBox()
              .Name("categories")
              .HtmlAttributes(new { style = "width:100%;" })
              .Placeholder("Select category...")
              .DataTextField("CategoryName")
              .DataValueField("CategoryId")
              .Filter(FilterType.Contains)
              .DataSource(source => {
                   source.Read(read => {
                       read.Action("GetCascadeCategories", "ComboBox");
                   });
              })
        %>
    </li>
    <li>
        <label for="products">Products:</label>
        <%= Html.Kendo().ComboBox()
              .Name("products")
              .HtmlAttributes(new { style = "width:100%;" })
              .Placeholder("Select product...")
              .DataTextField("ProductName")
              .DataValueField("ProductID")
              .Filter(FilterType.Contains)
              .DataSource(source => {
                  source.Read(read =>
                  {
                      read.Action("GetCascadeProducts", "ComboBox")
                          .Data("filterProducts");
                  })
                  .ServerFiltering(true);
              })
              .Enable(false)
              .AutoBind(false)
              .CascadeFrom("categories")
        %>
        <script>
            function filterProducts() {
                return {
                    categories: $("#categories").val(),
                    productFilter: $("#products").data("kendoComboBox").input.val()
                };
            }
        </script>
    </li>
    <li>
        <label for="orders">Orders:</label>
        <%= Html.Kendo().ComboBox()
              .Name("orders")
              .HtmlAttributes(new { style = "width:100%;" })
              .Placeholder("Select order...")
              .DataTextField("ShipCity")
              .DataValueField("OrderID")
              .Filter(FilterType.Contains)
              .DataSource(source => {
                  source.Read(read =>
                  {
                      read.Action("GetCascadeOrders", "ComboBox")
                          .Data("filterOrders");
                  })
                  .ServerFiltering(true);
              })
              .Enable(false)
              .AutoBind(false)
              .CascadeFrom("products")
        %>
        <script>
            function filterOrders() {
                return {
                    products: $("#products").val(),
                    orderFilter: $("#orders").data("kendoComboBox").input.val()
                };
            }
        </script>
    </li>
    <li>
        <button class="k-button k-primary" id="get">View Order</button>
    </li>
</div>
<script>
    $(document).ready(function () {
        var categories = $("#categories").data("kendoComboBox"),
            products = $("#products").data("kendoComboBox"),
            orders = $("#orders").data("kendoComboBox");

        $("#get").click(function () {
            var categoryInfo = "\nCategory: { id: " + categories.value() + ", name: " + categories.text() + " }",
                productInfo = "\nProduct: { id: " + products.value() + ", name: " + products.text() + " }",
                orderInfo = "\nOrder: { id: " + orders.value() + ", name: " + orders.text() + " }";

            alert("Order details:\n" + categoryInfo + productInfo + orderInfo);
        });
    });
</script>
<style>
    #fieldlist {
        margin: 0;
        padding: 0;
    }

    #fieldlist li {
        list-style: none;
        padding-bottom: 1.5em;
        text-align: left;
    }

    #fieldlist label {
        display: block;
        padding-bottom: .3em;
        font-weight: bold;
        text-transform: uppercase;
        font-size: 12px;
    }
</style>
</asp:Content>