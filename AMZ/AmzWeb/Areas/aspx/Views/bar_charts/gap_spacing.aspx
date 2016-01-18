﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content wide">
<%= Html.Kendo().Chart()
        .Name("chart")
        .Title("Internet Users")
        .Legend(legend => legend.Position(ChartLegendPosition.Bottom))
        .Series(series => {
            series.Column(new double[] { 67.96, 61.93, 75, 71, 78 }).Name("United States");
            series.Column(new double[] { 15.7, 11, 20, 25, 36.6 }).Name("World");
        })
        .CategoryAxis(axis => axis
            .Categories("2005", "2006", "2007", "2008", "2009")
        )
        .ValueAxis(axis => axis
            .Numeric().Labels(labels => labels.Format("{0}%"))
        )
        .Tooltip(tooltip => tooltip
            .Visible(true)
            .Format("{0}%")
        )
%>
</div>
<div class="box wide">
    <div class="box-col">
        <h4>Gap</h4>
        <ul class="options">
            <li>
                <input id="gap" type="number" value="1.5" step="0.1" style="width: 80px;" />
                <button id="getGap" class="k-button">Set gap</button>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Spacing</h4>
        <ul class="options">
            <li>
                <input id="spacing" type="number" value="0.4" step="0.1" style="width: 80px;" />
                <button id="getSpacing" class="k-button">Set spacing</button>
            </li>
        </ul>
    </div>
</div>
<script>
    $(document).ready(function() {
        var chart = $("#chart").data("kendoChart"),
            firstSeries = chart.options.series;

        $("#getGap").click(function () {
            firstSeries[0].gap = parseFloat($("#gap").val(), 10);
            chart.redraw();
        });

        $("#getSpacing").click(function () {
            firstSeries[0].spacing = parseFloat($("#spacing").val(), 10);
            chart.redraw();
        });

        if (kendo.ui.NumericTextBox) {
            $("#gap").kendoNumericTextBox();
            $("#spacing").kendoNumericTextBox();
        }
    });
</script>
</asp:Content>
