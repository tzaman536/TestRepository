<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Search for shipping names</h4>

    <%=Html.Kendo().MultiSelect()
          .Name("customers")
          .DataTextField("ContactName")
          .DataValueField("CustomerID")
          .Placeholder("Select customers...")
          .DataSource(source =>  source
              .Custom()
              .Group(g => g.Add("Country", typeof(string)))
              .Transport(transport => transport
                .Read(read =>
                {
                    read.Action("Customers_Read", "MultiSelect")
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