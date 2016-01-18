<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">

        <h4>Categories:</h4>
        <%= Html.Kendo().DropDownList()
              .Name("categories")
              .HtmlAttributes(new { style = "width:100%" })
              .OptionLabel("Select category...")
              .DataTextField("CategoryName")
              .DataValueField("CategoryId")
              .DataSource(source => {
                   source.Read(read => {
                       read.Action("GetCascadeCategories", "ComboBox");
                   });
              })
        %>
        <h4 style="margin-top: 2em;">Products:</h4>

        <%= Html.Kendo().DropDownList()
              .Name("products")
              .HtmlAttributes(new { style = "width:100%" })
              .OptionLabel("Select product...")
              .DataTextField("ProductName")
              .DataValueField("ProductID")
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
                    categories: $("#categories").val()
                };
            }
        </script>
        <h4 style="margin-top: 2em;">Orders:</h4>

        <%= Html.Kendo().DropDownList()
              .Name("orders")
              .HtmlAttributes(new { style = "width:100%" })
              .OptionLabel("Select order...")
              .DataTextField("ShipCity")
              .DataValueField("OrderID")
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
                    products: $("#filterOrders").val()
                };
            }
        </script>
  <button class="k-button k-primary" id="get" style="margin-top: 2em; float: right;">View Order</button>
</div>
           
<script>
    $(document).ready(function () {
        var categories = $("#categories").data("kendoDropDownList"),
            products = $("#products").data("kendoDropDownList"),
            orders = $("#orders").data("kendoDropDownList");

        $("#get").click(function () {
            var categoryInfo = "\nCategory: { id: " + categories.value() + ", name: " + categories.text() + " }",
                productInfo = "\nProduct: { id: " + products.value() + ", name: " + products.text() + " }",
                orderInfo = "\nOrder: { id: " + orders.value() + ", name: " + orders.text() + " }";

            alert("Order details:\n" + categoryInfo + productInfo + orderInfo);
        });
    });
</script>
<style>
    .k-readonly
    {
        color: gray;
    }
</style>
</asp:Content>