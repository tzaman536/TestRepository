<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%: Html.Kendo().Map()
      .Name("map")
      .HtmlAttributes(new { style = "height: 300px;" })
      .Center(39.6924, -97.3370)
      .Zoom(4)
      .Layers(layers => 
      {
          layers.Add()
              .Style(style => style
                  .Fill(fill => fill.Color("#b3cde3"))
                  .Stroke(stroke => stroke.Color("#cceebc5"))
              )
              .Type(MapLayerType.Shape)
              .DataSource(dataSource => dataSource
                  .GeoJson()
                  .Read(read => read.Url(Url.Content("~/Content/dataviz/map/us.geo.json")))
              );
      })
      .Events(events => events
          .Click("onClick")
          .Reset("onReset")
          .Pan("onPan")
          .PanEnd("onPanEnd")
          .ShapeClick("onShapeClick")
          .ShapeCreated("onShapeCreated")
          .ShapeMouseEnter("onShapeMouseEnter")
          .ShapeMouseLeave("onShapeMouseLeave")
          .ZoomStart("onZoomStart")
          .ZoomEnd("onZoomEnd")
      )
%>

<div class="box wide">
    <h4>Console log</h4>
    <div class="console"></div>
</div>

<script>
    function onClick(e) {
        kendoConsole.log("Click at ...");
    }

    function onReset(e) {
        kendoConsole.log("Reset");
    }

    function onPan(e) {
        kendoConsole.log("Pan");
    }

    function onPanEnd(e) {
        kendoConsole.log("Pan end");
    }

    function onShapeClick(e) {
        kendoConsole.log(kendo.format(
            "Shape click :: {0}", e.shape.dataItem.properties.name
        ));
    }

    function onShapeCreated(e) {
        kendoConsole.log(kendo.format(
            "Shape created :: {0}", e.shape.dataItem.properties.name
        ));
    }

    function onShapeMouseEnter(e) {
        kendoConsole.log(kendo.format(
            "Shape mouseEnter :: {0}", e.shape.dataItem.properties.name
        ));
    }

    function onShapeMouseLeave(e) {
        kendoConsole.log(kendo.format(
            "Shape mouseLeave :: {0}", e.shape.dataItem.properties.name
        ));
    }

    function onZoomStart(e) {
        kendoConsole.log("Zoom start");
    }

    function onZoomEnd(e) {
        kendoConsole.log("Zoom end");
    }
</script>
</asp:Content>