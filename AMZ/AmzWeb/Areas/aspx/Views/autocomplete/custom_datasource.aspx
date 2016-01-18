<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Products</h4>

    <%= Html.Kendo().AutoComplete()
          .Name("products")
          .DataTextField("ProductName")
          .Filter("contains")
          .MinLength(3)
          .HtmlAttributes(new { style = "width:100%" })
          .DataSource(source => source
              .Custom()
              .Type("odata")
              .ServerFiltering(true)
              .ServerPaging(true)
              .PageSize(20)
              .Transport(transport => transport
                  .Read(read => read.Url("http://demos.telerik.com/kendo-ui/service/Northwind.svc/Products"))
              )
          )
    %>
</div>
</asp:Content>