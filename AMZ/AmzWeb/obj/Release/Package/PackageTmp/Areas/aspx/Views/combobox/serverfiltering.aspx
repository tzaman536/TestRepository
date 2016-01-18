<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Find a product</h4>

    <%= Html.Kendo().ComboBox()
          .Name("products")
          .Placeholder("Select product")
          .HtmlAttributes(new { style = "width:100%;" })
          .DataTextField("ProductName")
          .DataValueField("ProductID")
          .Filter("contains")
          .AutoBind(false)
          .MinLength(3)
          .DataSource(source => {
              source.Read(read =>
              {
                  read.Action("GetProducts", "Home");
              })
              .ServerFiltering(true);
          })
    %>
</div>
</asp:Content>