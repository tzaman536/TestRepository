<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
      <h4>Set value</h4>
    <%= Html.Kendo().NumericTextBox()
            .Name("numerictextbox")
            .Events(e => e
                 .Change("change")
                 .Spin("spin")
            )
            .HtmlAttributes(new { style = "width: 100%" })
    %>
 </div>
<div class="box">
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<script>
    function change() {
        kendoConsole.log("Change :: " + this.value());
    }

    function spin() {
        kendoConsole.log("Spin :: " + this.value());
    }
</script>
</asp:Content>
