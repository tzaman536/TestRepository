<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Find a product</h4>
    <%= Html.Kendo().AutoComplete()
          .Name("products")
          .DataTextField("ProductName")
          .Filter("contains")
          .MinLength(3)
          .HtmlAttributes(new { style = "width:100%" })
          .DataSource(source => {
              source.Read(read =>
              {
                  read.Action("GetProducts", "Home")
                      .Data("onAdditionalData");
              })
              .ServerFiltering(true);
          })
    %>
    <div class="demo-hint">Hint: type "che"</div>
</div>
<script>
    function onAdditionalData() {
        return {
            text: $("#products").val() 
        };
    }
</script>
</asp:Content>