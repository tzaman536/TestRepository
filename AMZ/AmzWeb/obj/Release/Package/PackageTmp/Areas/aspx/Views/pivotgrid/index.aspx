<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style>
    #configurator
    {
        display: inline-block;
        vertical-align: top;
    }

    #pivotgrid {
        width: 70%;
    }

    .hidden-on-narrow {
        display: inline-block;
        vertical-align: top;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="responsive-message"></div>

    <%= Html.Kendo().PivotConfigurator()
        .Name("configurator")
        .Filterable(true)
        .HtmlAttributes(new { @class = "hidden-on-narrow" })
        .Sortable()
        .Height(570)
    %>

    <%= Html.Kendo().PivotGrid()
        .Name("pivotgrid")
        .HtmlAttributes(new { @class = "hidden-on-narrow" })
        .ColumnWidth(200)
        .Filterable(true)
        .Sortable()
        .Height(570)
        .Configurator("#configurator")
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
    function onError(e) {
        alert("error: " + kendo.stringify(e.errors[0]));
    }
</script>
</asp:Content>
