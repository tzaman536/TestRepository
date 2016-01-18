<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="box wide">
    <div class="box-col">
    <h4>Selection</h4>
    <ul class="options">
        <li>
            <input type="text" value="0" id="selectRow" class="k-textbox"/>
            <button class="selectRow k-button">Select row</button>
        </li>
        <li>
            <button class="clearSelection k-button">Clear selected rows</button>
        </li>
    </ul>
    </div>
    <div class="box-col">
    <h4>Expand / Collapse</h4>
    <ul class="options">
        <li>
            <input type="text" value="0" id="groupRow" class="k-textbox"/>
            <button class="toggleGroup k-button">Collapse/Expand group</button>
        </li>
    </ul>
    </div>
</div>

<%= Html.Kendo().Grid<Kendo.Mvc.Examples.Models.ProductViewModel>()
    .Name("grid")
    .Columns(columns =>
    {
        columns.Bound(p => p.ProductName).Title("Product Name");
        columns.Bound(p => p.UnitPrice).Title("Unit Price");
        columns.Bound(p => p.UnitsInStock).Title("Units In Stock");
    })
    .Pageable()
    .Groupable()
    .Sortable()
    .Selectable(selectable=> selectable
        .Mode(GridSelectionMode.Multiple)
        .Type(GridSelectionType.Row))
    .DataSource(dataSource => dataSource
        .Ajax()
        .PageSize(5)
        .Group(group => group.Add(p => p.UnitsInStock))
        .Read(read => read.Action("Products_Read", "Grid"))
     )
%>

<script type="text/javascript">
    $(".clearSelection").click(function () {
        $("#grid").data("kendoGrid").clearSelection();
    });

    var selectRow = function (e) {
        if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode) {
            var grid = $("#grid").data("kendoGrid"),
                                    rowIndex = $("#selectRow").val(),
                                    row = grid.tbody.find(">tr:not(.k-grouping-row)").eq(rowIndex);

            grid.select(row);
        }
    },
    toggleGroup = function (e) {
        if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode) {
            var grid = $("#grid").data("kendoGrid"),
                rowIndex = $("#groupRow").val(),
                row = grid.tbody.find(">tr.k-grouping-row").eq(rowIndex);

            if (row.has(".k-i-collapse").length) {
                grid.collapseGroup(row);
            } else {
                grid.expandGroup(row);
            }
        }
    };

    $(document).ready(function () {
        $(".selectRow").click(selectRow);
        $("#selectRow").keypress(selectRow);

        $(".toggleGroup").click(toggleGroup);
        $("#groupRow").keypress(toggleGroup);
    });
    
</script>

</asp:Content>
