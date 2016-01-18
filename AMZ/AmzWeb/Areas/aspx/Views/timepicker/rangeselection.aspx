<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">

     <h4>Start time</h4>
    <%= Html.Kendo().TimePicker()
          .Name("start")
          .Value("8:00 AM")
          .Min("8:00 AM")
          .Max("6:00 PM")
          .Events(e => e.Change("startChange"))
          .HtmlAttributes(new { style = "width: 100%" })
     %>

    <h4 style="margin-top: 2em;">End time</h4>
    <%= Html.Kendo().TimePicker()
          .Name("end")
          .Value("8:30 AM")
          .Min("8:00 AM")
          .Max("7:30 AM")
          .HtmlAttributes(new { style = "width: 100%" })
     %>
</div>
<script>
    function startChange() {
        var startTime = this.value(),
            endPicker = $("#end").data("kendoTimePicker");

        if (startTime) {
            startTime = new Date(startTime);

            endPicker.max(startTime);

            startTime.setMinutes(startTime.getMinutes() + this.options.interval);

            endPicker.min(startTime);
            endPicker.value(startTime);
        }
    }
</script>

</asp:Content>