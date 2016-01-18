<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Products</h4>

    <%= Html.Kendo().ComboBox()
          .Name("products")
          .DataTextField("ProductName")
          .DataValueField("ProductID")
          .Placeholder("Select Product")
          .HtmlAttributes(new { style = "width:100%;" })
          .Filter("contains")
          .AutoBind(false)
          .MinLength(3)
          .DataSource(source => source
                .Custom()
                .Type("odata")
                .ServerFiltering(true)
                .Transport(transport => transport
                    .Read(read =>
                    {
                        read.Url("http://demos.telerik.com/kendo-ui/service/Northwind.svc/Products");
                    })
                )
          )
    %>
</div>
</asp:Content>