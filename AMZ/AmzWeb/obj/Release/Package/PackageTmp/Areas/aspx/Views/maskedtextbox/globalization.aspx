<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%= Url.Content("~/Scripts/cultures/kendo.culture.en-US.min.js") %>"></script>
    <script src="<%= Url.Content("~/Scripts/cultures/kendo.culture.en-GB.min.js") %>"></script>
    <script src="<%= Url.Content("~/Scripts/cultures/kendo.culture.de-DE.min.js") %>"></script>
    <script src="<%= Url.Content("~/Scripts/cultures/kendo.culture.fr-FR.min.js") %>"></script>
    <script src="<%= Url.Content("~/Scripts/cultures/kendo.culture.bg-BG.min.js") %>"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="product-view" class="demo-section k-content">
    <ul id="fieldlist">
        <li>
          <label for="culture">Choose culture:</label>
            <%= Html.Kendo().DropDownList()
                  .Name("culture")
                  .Items(items => {
                      items.Add().Text("en-US").Value("en-US");
                      items.Add().Text("en-GB").Value("en-GB");
                      items.Add().Text("de-DE").Value("de-DE");
                      items.Add().Text("fr-FR").Value("fr-FR");
                      items.Add().Text("bg-BG").Value("bg-BG");
                  })
                  .Events(events => events.Change("changeCulture"))
                  .HtmlAttributes(new { style = "width: 100%" })
            %>
        </li>
        <li>
         <label for="initial">Initial price:</label>
            <%= Html.Kendo().MaskedTextBox()
                  .Name("initial")
                  .Mask("9,999.99 $")
                  .Value("1234.56")
                  .HtmlAttributes(new { style = "width: 100%" })
            %>
        </li>
     </ul>
</div>
<script>
    function changeCulture() {
        kendo.culture(this.value());

        $("#initial").data("kendoMaskedTextBox").setOptions(initial.options);
    }
</script>
<style>
    #fieldlist {
        margin: 0 0 -2em;
        padding: 0;
    }

    #fieldlist li {
        list-style: none;
        padding-bottom: 2em;
    }

    #fieldlist label {
        display: block;
        padding-bottom: 1em;
        font-weight: bold;
        text-transform: uppercase;
        font-size: 12px;
        color: #444;
    }

</style>
</asp:Content>