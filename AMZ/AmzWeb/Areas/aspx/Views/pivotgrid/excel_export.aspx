﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <style>
   #export
    {
        margin: 0 0 10px 1px;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

    <div class="responsive-message"></div>

    <button id="export" class="k-button k-button-icontext hidden-on-narrow"><span class="k-icon k-i-excel"></span>Export to Excel</button>

    <%= Html.Kendo().PivotGrid()
        .Name("pivotgrid")
        .Excel(excel => excel
            .FileName("Kendo UI PivotGrid Export.xlsx")
            .Filterable(true)
            .ProxyURL(Url.Action("Excel_Export_Save", "PivotGrid"))
        )
        .HtmlAttributes(new { @class = "hidden-on-narrow" })
        .ColumnWidth(200)
        .Height(570)
        .DataSource(dataSource => dataSource.
            Xmla()
            .Columns(columns => {
                columns.Add("[Date].[Calendar]").Expand(true);
                columns.Add("[Product].[Category]");
            })
            .Rows(rows => rows.Add("[Geography].[City]"))
            .Measures(measures => measures.Values(new string[]{"[Measures].[Reseller Freight Cost]"}))
            .Transport(transport => transport
                .Connection(connection => connection
                    .Catalog("Adventure Works DW 2008R2")
                    .Cube("Adventure Works"))
                .Read(read => read
                    .Url("http://demos.telerik.com/olap/msmdpump.dll")
                    .DataType("text")
                    .ContentType("text/xml")
                    .Type(HttpVerbs.Post) 
                )
            )
            .Events(e => e.Error("onError"))
        )
    %>

<script>
    $(function () {
        $("#export").click(function () {
            $("#pivotgrid").getKendoPivotGrid().saveAsExcel();
        });
    });

    function onError(e) {
        alert("error: " + kendo.stringify(e.errors[0]));
    }
</script>
</asp:Content>
