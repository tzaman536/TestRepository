<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Select date</h4>
    <%= Html.Kendo().DatePicker()
          .Name("datepicker")
          .Events(e =>
          {
              e.Change("change").Open("open").Close("close");
          })
         .HtmlAttributes(new { style = "width: 100%" })
    %>
</div>
<div class="box">                
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<script>
    function open() {
        kendoConsole.log("Open");
    }

    function close() {
        kendoConsole.log("Close");
    }

    function change() {
        kendoConsole.log("Change :: " + kendo.toString(this.value(), 'd'));
    }
</script>

</asp:Content>