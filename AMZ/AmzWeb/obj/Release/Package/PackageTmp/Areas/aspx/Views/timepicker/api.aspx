<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="box wide">
    <div class="box-col">
    <h4>API Functions</h4>
    <ul class="options">
        <li>
            <button id="get" class="k-button">Get value</button>
        </li>
        <li>
            <input id="value" value="10:30 AM" style="float:none" />
            <button id="set" class="k-button">Set value</button>
        </li>
    </ul>
    </div>

    <div class="box-col">
    <h4>Enable / Disable</h4>
    <ul class="options">
        <li>
            <button id="enable" class="k-button">Enable</button>
            <button id="disable" class="k-button">Disable</button>
                    
        </li>
        <li>
            <button id="readonly" class="k-button">Readonly</button>
        </li>
    </ul>
    </div>

    <div class="box-col">
    <h4>Calendar Open / Close</h4>
    <ul class="options">
        <li>
            <button id="open" class="k-button">Open</button>
            <button id="close" class="k-button">Close</button>
                    
        </li>
    </ul>
    </div>
</div>            
<div class="demo-section k-content">
    <h4>Select time</h4>
    <%= Html.Kendo().TimePicker()
      .Name("timepicker")
      .HtmlAttributes(new { style = "width: 100%" })
    %>
</div>

<script>
    $(document).ready(function() {
        $("#timepicker").closest(".k-widget")
                        .attr("id", "timepicker_wrapper");

        var timepicker = $("#timepicker").data("kendoTimePicker");

        var setValue = function () {
            timepicker.value($("#value").val());
        };

        $("#enable").click(function() {
            timepicker.enable();
        });

        $("#disable").click(function() {
            timepicker.enable(false);
        });

        $("#readonly").click(function() {
            timepicker.readonly();
        });

        $("#open").click(function() {
            timepicker.open();
        });

        $("#close").click(function() {
            timepicker.close();
        });

        $("#value").kendoTimePicker({
            change: setValue
        });

        $("#set").click(setValue);

        $("#get").click(function() {
            alert(timepicker.value());
        });
    });
</script>
</asp:Content>
