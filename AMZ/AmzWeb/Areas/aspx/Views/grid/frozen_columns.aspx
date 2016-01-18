<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.Order>()
    .Name("Grid")
    .Columns(columns =>
    {
        columns.Bound(o => o.OrderID).Width(150).Locked(true).Lockable(false);
        columns.Bound(o => o.ShipCountry).Width(300);
        columns.Bound(o => o.ShipCity).Width(300);
        columns.Bound(o => o.ShipName).Width(300).Locked(true);
        columns.Bound(o => o.ShipAddress).Width(400).Lockable(false);
    })
    .DataSource(dataSource => dataSource
        .Ajax()
        .PageSize(30)
        .Read(read => read.Action("FrozenColumns_Read", "Grid"))
     )
    .Scrollable(scrollable => scrollable.Height(540))
    .Reorderable(reorderable => reorderable.Columns(true))
    .Resizable(resizable => resizable.Columns(true))
    .Pageable()
    .Filterable()
    .Sortable()
    .Groupable()
    .ColumnMenu()
%>
</asp:Content>
