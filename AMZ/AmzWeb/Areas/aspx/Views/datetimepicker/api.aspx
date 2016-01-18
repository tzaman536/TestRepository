<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content">
    <%= Html.Kendo().DateTimePicker()
          .Name("datetimepicker")
          .HtmlAttributes(new { style = "width:100%;" })
    %>
</div>

<div class="box wide">
    <div class="box-col">
        <h4>Set / Get Value</h4>
        <ul class="options">
            <li>
                <input id="value" value="10/10/2000 12:00 AM" style="float:none" />
           <button id="set" class="k-button">Set value</button>
            </li>
            <li style="text-align: right;">
                <button id="get" class="k-button">Get value</button>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Open / Close</h4>
        <ul class="options">
            <li>
                <button id="openDateView" class="k-button">Open date view</button>
                <button id="closeDateView" class="k-button">Close date view</button>
            </li>
            <li>
                <button id="openTimeView" class="k-button">Open time view</button>
                <button id="closeTimeView" class="k-button">Close time view</button>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Enable / Disable</h4>
        <ul class="options">
            <li>
                <button id="enable" class="k-button">Enable</button> 
                <button id="disable" class="k-button">Disable</button> 
                <button id="readonly" class="k-button">Readonly</button>
            </li>
        </ul>
    </div>
</div>

<script>
    $(document).ready(function () {
        $("#datetimepicker").closest(".k-widget").attr("id", "datetimepicker_wrapper");

        var datetimepicker = $("#datetimepicker").data("kendoDateTimePicker");

        var setValue = function () {
            datetimepicker.value($("#value").val());
        };

        $("#enable").click(function () {
            datetimepicker.enable();
        });

        $("#disable").click(function () {
            datetimepicker.enable(false);
        });

        $("#readonly").click(function() {
            datetimepicker.readonly();
        });

        $("#openDateView").click(function () {
            datetimepicker.open("date");
        });

        $("#openTimeView").click(function () {
            datetimepicker.open("time");
        });

        $("#closeDateView").click(function () {
            datetimepicker.close("date");
        });

        $("#closeTimeView").click(function () {
            datetimepicker.close("time");
        });

        $("#value").kendoDateTimePicker({
            change: setValue
        });

        $("#set").click(setValue);

        $("#get").click(function () {
            alert(datetimepicker.value());
        });
    });
</script>

</asp:Content>
