<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Products</h4>
    <%= Html.Kendo().DropDownList()
          .Name("products")
          .HtmlAttributes(new { style = "width: 100%;" })
          .DataTextField("ProductName")
          .DataValueField("ProductID")
          .Filter("contains")
          .DataSource(source => {
              source.Read(read =>
              {
                  read.Action("GetProducts", "Home");
              })
              .ServerFiltering(false);
          })
    %>
</div>
</asp:Content>