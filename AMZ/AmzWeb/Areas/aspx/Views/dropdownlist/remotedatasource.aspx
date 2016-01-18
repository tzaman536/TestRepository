<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Products</h4>
    <%= Html.Kendo().DropDownList()
          .Name("products")
          .DataTextField("ProductName")
          .DataValueField("ProductID")
          .DataSource(source => {
              source.Read(read =>
              {
                  read.Action("GetProducts", "Home");
              }); 
          })
        .HtmlAttributes(new { style = "width: 100%" })
    %>
</div>
</asp:Content>