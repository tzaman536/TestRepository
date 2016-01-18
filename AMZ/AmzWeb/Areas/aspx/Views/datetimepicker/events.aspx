<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content">
    <h4>Select date</h4>
    <%= Html.Kendo().DateTimePicker()
          .Name("datetimepicker")
          .HtmlAttributes(new { style = "width:100%;" })
          .Events(e =>
          {
              e.Change("change").Open("open").Close("close");
          })
    %>
</div>
<div class="box">                
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<script>
    function open(e) {
        kendoConsole.log("Open :: " + e.view + "-view");
    }

    function close(e) {
        kendoConsole.log("Close :: " + e.view + "-view");
    }

    function change() {
        kendoConsole.log("Change :: " + kendo.toString(this.value(), 'g'));
    }
</script>

</asp:Content>