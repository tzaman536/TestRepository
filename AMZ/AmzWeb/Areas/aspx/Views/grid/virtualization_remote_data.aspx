<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.Kendo().Grid<Kendo.Mvc.Examples.Models.OrderViewModel>()
        .Name("grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(110);
            columns.Bound(o => o.CustomerID).Width(130);
            columns.Bound(o => o.ShipName).Width(280);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.ShipCity).Width(160);
            columns.Bound(o => o.ShipCountry).Width(160);
        })
        .Sortable()
        .Scrollable(scrollable => scrollable.Virtual(true))
        .HtmlAttributes(new { style = "height:543px;" })
        .DataSource(dataSource => dataSource
            .Ajax()
            .PageSize(100)
            .Read(read => read.Action("Virtualization_Read", "Grid"))
         )
    %>

<style>
   /*horizontal Grid scrollbar should appear if the browser window is shrinked too much*/
    #grid table
    {
        min-width: 1190px;
    }

</style>
</asp:Content>
