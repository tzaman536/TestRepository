<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master"%>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content wide">
    <h4>Grid with multiple row selection enabled</h4>
    <%=Html.Kendo().Grid<Kendo.Mvc.Examples.Models.OrderViewModel>()
        .Name("rowSelection")
        .Columns(columns => {
            columns.Bound(o => o.ShipCountry).Width(300);
            columns.Bound(p => p.Freight).Width(300);
            columns.Bound(p => p.OrderDate).Format("{0:dd/MM/yyyy}");
        })
        .Pageable(pageable => pageable.ButtonCount(5))
        .Selectable(selectable => selectable
            .Mode(GridSelectionMode.Multiple))
        .Navigatable()
        .DataSource(dataSource => dataSource
            .Ajax()
            .PageSize(5)
            .Read(read => read.Action("Orders_Read", "Grid"))
         )
    %>
</div>

<div class="demo-section k-content wide">
   <h4>Grid with multiple cell selection enabled</h4>
    <%=Html.Kendo().Grid<Kendo.Mvc.Examples.Models.OrderViewModel>()
        .Name("cellSelection")
        .Columns(columns => {
            columns.Bound(o => o.ShipCountry).Width(200);
            columns.Bound(p => p.Freight).Width(200);
            columns.Bound(p => p.OrderDate).Format("{0:dd/MM/yyyy}");
        })
        .Pageable(pageable => pageable.ButtonCount(5))
        .Selectable(selectable => selectable
            .Mode(GridSelectionMode.Multiple)
            .Type(GridSelectionType.Cell))
        .Navigatable()
        .DataSource(dataSource => dataSource
            .Ajax()
            .PageSize(6)
            .Read(read => read.Action("Orders_Read", "Grid"))
         )
    %>
</div>

</asp:Content>
