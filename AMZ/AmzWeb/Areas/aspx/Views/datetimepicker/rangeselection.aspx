<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Start date</h4>

    <%= Html.Kendo().DateTimePicker()
          .Name("start")
          .Value(DateTime.Today)
          .Max(DateTime.Today)
          .ParseFormats(new string[] { "MM/dd/yyyy" })
          .Events(e => e.Change("startChange"))
          .HtmlAttributes(new { style = "width:100%;" })
    %>

    <h4 style="margin-top: 2em;">End date</h4>
    <%= Html.Kendo().DateTimePicker()
          .Name("end")
          .Value(DateTime.Today)
          .Min(DateTime.Today)
          .ParseFormats(new string[] { "MM/dd/yyyy" })
          .Events(e => e.Change("endChange"))
          .HtmlAttributes(new { style = "width:100%;" })
    %>
</div>
<script>
    function startChange() {
        var endPicker = $("#end").data("kendoDateTimePicker"),
            startDate = this.value();

        if (startDate) {
            startDate = new Date(startDate);
            startDate.setDate(startDate.getDate() + 1);
            endPicker.min(startDate);
        }
    }

    function endChange() {
        var startPicker = $("#start").data("kendoDateTimePicker"),
            endDate = this.value();

        if (endDate) {
            endDate = new Date(endDate);
            endDate.setDate(endDate.getDate() - 1);
            startPicker.max(endDate);
        }
    }
</script>

</asp:Content>