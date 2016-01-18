<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content" style="text-align: center;">
<h4>Pick a date</h4>
    <%= Html.Kendo().Calendar()
            .Name("calendar")
            .HtmlAttributes(new { style = "width: 243px;" })
            .Events(e => e.Change("change").Navigate("navigate"))
    %>
</div>

<div class="box" style="text-align: center;">
    <h4>Events log</h4>
    <div class="console"></div>
</div>

<script>
    function change() {
        kendoConsole.log("Change :: " + kendo.toString(this.value(), 'd'));
    }

    function navigate() {
        kendoConsole.log("Navigate");
    }
</script>

</asp:Content>
