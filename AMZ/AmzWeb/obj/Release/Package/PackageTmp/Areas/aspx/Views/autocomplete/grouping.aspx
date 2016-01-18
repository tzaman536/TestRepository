<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Find a Customer</h4>

    <%= Html.Kendo().AutoComplete()
          .Name("customers")
          .DataTextField("ContactName")
          .Height(400)
          .HtmlAttributes(new { style = "width:100%" })
          .Placeholder("Type a customer name")
          .DataSource(source =>  source
              .Custom()
              .Group(g => g.Add("Country", typeof(string)))
              .Transport(transport => transport
                .Read(read =>
                {
                    read.Action("Customers_Read", "AutoComplete")
                        .Data("onAdditionalData");
                }))
              .ServerFiltering(true))
     %>

     <div class="demo-hint">Hint: type "an"</div>
</div>
<script>
    function onAdditionalData() {
        return {
            text: $("#customers").val()
        };
    }
</script>
</asp:Content>