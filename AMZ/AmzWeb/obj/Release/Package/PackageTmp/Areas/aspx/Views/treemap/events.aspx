﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%= Html.Kendo().TreeMap()
        .Name("treeMap")
        .DataSource(dataSource => dataSource
            .Read(read => read
                .Action("_PopulationUSA", "TreeMap")
            )
            .Model(m => m.Children("Items"))
        )
        .ValueField("Value")
        .TextField("Name")
        .Events(events => events
            .ItemCreated("onItemCreated")
            .DataBound("onDataBound")
        )
        .HtmlAttributes(new { style = "height:600px; font-size: 12px;" })
%>
<div class="box wide">
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<script>
    function onDataBound(e) {
        kendoConsole.log("Data bound");
    }

    function onItemCreated(e) {
        kendoConsole.log("Item is created");
    }
</script>
</asp:Content>
