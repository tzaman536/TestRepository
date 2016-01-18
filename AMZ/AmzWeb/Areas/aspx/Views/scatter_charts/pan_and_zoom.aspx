﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Kendo.Mvc.Examples.Models.ChartScatterPoint>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content wide">
    <%= Html.Kendo().Chart(Model)
        .Name("chart")
        .RenderAs(RenderingMode.Canvas)
        .Series(series => {
            series.ScatterLine(model => model.X, model => model.Y)
                .Style(ChartScatterLineStyle.Smooth)
                .Markers(markers => markers.Visible(false));
        })
        .XAxis(x => x
            .Numeric()
            .Min(-2)
            .Max(2)            
            .Labels(labels => labels.Format("{0:n1}"))
        )
        .YAxis(y => y
            .Numeric()
            .Labels(labels => labels.Format("{0:n2}"))
        )
        .Pannable()
        .Zoomable()
    %>
</div>
</asp:Content>
