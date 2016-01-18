<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Customers</h4>

    <%= Html.Kendo().ComboBox()
          .Name("customers")
          .DataTextField("ContactName")
          .DataValueField("CustomerID")
          .MinLength(3)
          .HtmlAttributes(new { style = "width:100%;" })
          .Height(400)
          .DataSource(source =>  source
              .Custom()
              .Group(g => g.Add("Country", typeof(string)))
              .Transport(transport => transport
                .Read(read =>
                {
                    read.Action("Customers_Read", "ComboBox")
                        .Data("onAdditionalData");
                }))
              .ServerFiltering(true))
     %>
</div>
<script>
    function onAdditionalData() {
        return {
            text: $("#customers").val()
        };
    }
</script>
</asp:Content>