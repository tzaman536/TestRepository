<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="demo-section k-content wide">
        <h2>Sales statistics</h2>
        <%=Html.Kendo().Chart().Name("chart-oct")
            .Series(series =>
                series.Funnel(new dynamic[]{
                    new {
                        category= "Impressions ",
                        value= 434823,
                        color= "#0e5a7e"
                    },
                    new {
                        category= "Clicks",
                        value= 356854,
                        color= "#166f99"
                    },
                    new {
                        category= "Unique Visitors",
                        value= 280022,
                        color= "#2185b4"
                    },
                    new {
                        category= "Downloads",
                        value= 190374,
                        color= "#319fd2"
                    },
                    new {
                        category= "Purchases",
                        value= 120392,
                        color= "#3eaee2"
                    }
                })
            )
            .Title(t => t.Text("October").Position(ChartTitlePosition.Bottom))
            .Legend(l => l.Visible(false))
            .SeriesDefaults(sd =>
            {
                sd.Funnel().Labels(lbls =>
                {
                    lbls.Visible(true)
                        .Background("transparent")
                        .Color("white")
                        .Format("N0");
                })
                .DynamicSlope(false)
                .DynamicHeight(false);
            })
            .Tooltip(tt => tt.Visible(true).Template("#=category#"))
        %>
        <%=Html.Kendo().Chart().Name("chart-nov")
            .Series(series =>
                series.Funnel(new dynamic[]{
                    new {
                        category= "Impressions ",
                        value= 984528,
                        color= "#0e5a7e"
                    },
                    new {
                        category= "Clicks",
                        value= 856287,
                        color= "#166f99"
                    },
                    new {
                        category= "Unique Visitors",
                        value= 643694,
                        color= "#2185b4"
                    },
                    new {
                        category= "Downloads",
                        value= 567843,
                        color= "#319fd2"
                    },
                    new {
                        category= "Purchases",
                        value= 389137,
                        color= "#3eaee2"
                    }
                })
            )
            .Title(t => t.Text("November").Position(ChartTitlePosition.Bottom))
            .Legend(l => l.Visible(false))
            .SeriesDefaults(sd =>
            {
                sd.Funnel().Labels(lbls =>
                {
                    lbls.Visible(true)
                        .Background("transparent")
                        .Color("white")
                        .Format("N0");
                })
                .DynamicSlope(false)
                .DynamicHeight(false);
            })
            .Tooltip(tt => tt.Visible(true).Template("#=category#"))
        %>
        <%=Html.Kendo().Chart().Name("chart-dec")
            .Series(series =>
                series.Funnel(new dynamic[]{
                    new {
                        category= "Impressions ",
                        value= 1200528,
                        color= "#0e5a7e"
                    },
                    new {
                        category= "Clicks",
                        value= 989287,
                        color= "#166f99"
                    },
                    new {
                        category= "Unique Visitors",
                        value= 885694,
                        color= "#2185b4"
                    },
                    new {
                        category= "Downloads",
                        value= 788843,
                        color= "#319fd2"
                    },
                    new {
                        category= "Purchases",
                        value= 694137,
                        color= "#3eaee2"
                    }
                })
            )
            .Title(t => t.Text("December").Position(ChartTitlePosition.Bottom))
            .Legend(l => l.Visible(false))
            .SeriesDefaults(sd =>
            {
                sd.Funnel().Labels(lbls =>
                {
                    lbls.Visible(true)
                        .Background("transparent")
                        .Color("white")
                        .Format("N0");
                })
                .DynamicSlope(false)
                .DynamicHeight(false);
            })
            .Tooltip(tt => tt.Visible(true).Template("#=category#"))
        %>
    </div>
   <div class="box wide">
        <div class="box-col">
            <h4><input type="checkbox" id="dynamicSlope"/> Dynamic Slope</h4>
            <i>The slope for each segment depends on the ratio between the current and the next value</i>
        </div>
        <div class="box-col">
            <h4><input type="checkbox" id="dynamicHeight"/> Dynamic Height</h4>
            <i>The height of the segment is the overall percentage for that dataItem</i>
        </div>
        <div class="box-col">
            <h4>Neck Ratio</h4>
            <ul class="options">
                <li><input id="neckSlider" value="0.3"/></li>
            </ul>
            <i>The ratio between the bases of the whole funnel element</i>
        </div>
    </div>
    <script>
        function refresh() {
            var slider = $('#neckSlider').data("kendoSlider");
            var chartNames = ["chart-oct", "chart-nov", "chart-dec"];

            for (var idx in chartNames) {
                var chart = $("#" + chartNames[idx]).data("kendoChart");

                var options =
                {
                    seriesDefaults: {
                        neckRatio: slider.value(),
                        dynamicHeight : $('#dynamicHeight').is(':checked'),
                        dynamicSlope : $('#dynamicSlope').is(':checked'),
                        labels: {
                            visible: true,
                            background: "transparent",
                            color:"white",
                            format: "N0"
                        },
                    }
                }

                chart.setOptions(options);
                slider.enable(!options.seriesDefaults.dynamicSlope);
            }
        }

        $(document).ready(function () {
            $("#neckSlider").kendoSlider({
                change: refresh,
                value: 0.3,
                min: 0,
                max: 10,
                smallStep: 0.01,
                largeStep: 0.1,
                showButtons: false
            });

            $('.box').on('click', ':checkbox', refresh);
        });

    </script>

  <style>
   .demo-section {
        text-align: center;
    }
    #chart-oct,
    #chart-nov,
    #chart-dec {
        display: inline-block;
        width: 180px;
        height: 300px;
        margin: 15px 65px;
    }
    .box-col
    {
        width:25%;
    }
  </style>

</asp:Content>
