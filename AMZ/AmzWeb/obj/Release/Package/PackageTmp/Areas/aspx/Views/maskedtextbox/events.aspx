<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
 <div class="demo-section k-content">
    <h4>Enter phone number</h4>
    <%= Html.Kendo().MaskedTextBox()
          .Name("maskedtextbox")
          .Mask("(999) 000-0000")
          .Events(events => events.Change("change"))
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
</script>

</asp:Content>