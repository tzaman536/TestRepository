<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #gauge-container {
            background: transparent url(<%= Url.Content("~/Content/dataviz/gauge/gauge-container.png") %>) no-repeat 50% 50%;
            width: 404px;
            height: 404px;
            text-align: center;
            margin: 0 auto 30px auto;
        }
    
        #gauge {
            width: 330px;
            height: 330px;
            margin: 0 auto 0;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="gauge-container">
    <%= Html.Kendo().RadialGauge()
            .Name("gauge")
            .Pointer(pointer => pointer.Value(65))
            .Scale(scale => scale
                .MinorUnit(5)
                .StartAngle(-30)
                .EndAngle(210)
                .Max(180)
                .Labels(labels => labels
                    .Position(GaugeRadialScaleLabelsPosition.Inside)
                )
                .Ranges(ranges => {
                    ranges.Add().From(80).To(120).Color("#ffc700");
                    ranges.Add().From(120).To(150).Color("#ff7a00");
                    ranges.Add().From(150).To(180).Color("#c20000");
                })
            )
    %>
</div>

<div class="box">
    <div class="box-col">
        <h4>Gauge Labels</h4>
        <ul class="options">
            <li>
                <input id="labels" checked="checked" type="checkbox" autocomplete="off">
                <label for="labels">Show labels</label>
            </li>
    
            <li>
                <input id="labels-inside" type="radio" value="inside" name="labels-position" checked="checked">
                <label for="labels-inside">- inside the gauge</label>
            </li>
    
            <li>
                <input id="labels-outside" type="radio" value="outside" name="labels-position">
                <label for="labels-outside">- outside of the gauge</label>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Gauge Ranges</h4>
        <ul class="options">
            <li>
                <input id="ranges" checked="checked" type="checkbox" autocomplete="off">
                <label for="ranges">Show ranges</label>
            </li>
        </ul>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $(".box").bind("change", refresh);

        window.configuredRanges = $("#gauge").data("kendoRadialGauge").options.scale.ranges;
    });

    function refresh() {
        var gauge = $("#gauge").data("kendoRadialGauge"),
            showLabels = $("#labels").prop("checked"),
            showRanges = $("#ranges").prop("checked"),
            positionInputs = $("input[name='labels-position']"),
            labelsPosition = positionInputs.filter(":checked").val(),
            options = gauge.options;

        options.transitions = false;
        options.scale.labels = options.scale.labels || {};
        options.scale.labels.visible = showLabels;
        options.scale.labels.position = labelsPosition;
        options.scale.ranges = showRanges ? window.configuredRanges : [];

        gauge.redraw();
    }
</script>
</asp:Content>