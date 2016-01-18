<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="box wide">
        <div class="box-col select-col">
            <h4>Bubble symbol</h4>
            <ul id="select-symbol">
                <li>Circle</li>
                <li>Square</li>
            </ul>
        </div>
        <div id="info" class="box-col"></div>
    </div>
    <%: Html.Kendo().Map()
        .Name("map")
        .Center(45, 45)
        .Zoom(4)
        .MinZoom(3)
        .Wraparound(false)
        .Layers(layers =>
        {
            layers.Add()
                .Type(MapLayerType.Tile)
                .UrlTemplate("http://otile3.mqcdn.com/tiles/1.0.0/sat/#= zoom #/#= x #/#= y #.png")
                .Attribution("Tiles &copy; <a href='http://www.mapquest.com/' target='_blank'>MapQuest</a>");

            layers.Add()
                .Type(MapLayerType.Bubble)
                .Style(style => style
                    .Fill(fill => fill.Color("#fff").Opacity(0.4))
                    .Stroke(stroke => stroke.Width(0))
                )
                .DataSource(dataSource => dataSource
                      .Read(read => read.Action("_UrbanAreas", "Map"))
                )
                .LocationField("Location")
                .ValueField("Pop2010");
        })
        .Events(events => events
            .ShapeMouseEnter("onShapeMouseEnter")
            .Reset("onReset")
        )
    %>

    <script id="info-template" type="text/x-kendo-template">
        <h4>#: City #, #= Country #</h4>
        <p class="info">Population #= kendo.toString(Pop2010 * 1000, "N0") #</p>
    </script>

    <script id="empty-info-template" type="text/x-kendo-template">
        <h4>Hover over urban areas</h4>
        <p>&nbsp;</p>
    </script>

    <script>
        var template = kendo.template($("#info-template").html());
        var emptyTemplate = kendo.template($("#empty-info-template").html());
        var activeShape;

        function onShapeMouseEnter(e) {
            if (activeShape) {
                activeShape.options.set("stroke", null);
            }

            activeShape = e.shape;
            activeShape.options.set("stroke", { width: 1.5, color: "#fff" });

            $("#info").html(template(e.shape.dataItem));
        }

        function onReset() {
            $("#info").html(emptyTemplate({}));
            activeShape = null;
        }

        $("#select-symbol").kendoMobileButtonGroup({
            select: function (e) {
                var layer = $("#map").data("kendoMap").layers[1];
                layer.options.symbol = e.index === 0 ? "circle" : "square";
                layer.reset();
            },
            index: 0
        });
    </script>
</asp:Content>
