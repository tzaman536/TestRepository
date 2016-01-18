<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.EmployeeViewModel>()
            .Name("grid")
            .Columns(columns =>
            {
                columns.Bound(e => e.FirstName).Width(110);
                columns.Bound(e => e.LastName).Width(110);
                columns.Bound(e => e.Country).Width(110);
                columns.Bound(e => e.City).Width(110);
                columns.Bound(e => e.Title);

            })
            .Sortable()
            .Pageable()
            .Scrollable()
            .ClientDetailTemplateId("template")
            .HtmlAttributes(new { style = "height:600px;" })
            .DataSource(dataSource => dataSource
                .Ajax()
                .PageSize(6)
                .Read(read => read.Action("HierarchyBinding_Employees", "Grid"))
            )
            .Events(events => events.DataBound("dataBound"))
    %>

    <script id="template" type="text/kendo-tmpl">
        <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.OrderViewModel>()
                .Name("grid_#=EmployeeID#") // template expression, to be evaluated in the master context
                .Columns(columns =>
                {
                    columns.Bound(o => o.OrderID).Width(110);
                    columns.Bound(o => o.ShipCountry).Width(110);
                    columns.Bound(o => o.ShipAddress);
                    columns.Bound(o => o.ShipName).Width(300);
                })
                .DataSource(dataSource => dataSource
                    .Ajax()
                    .PageSize(10)
                    .Read(read => read.Action("HierarchyBinding_Orders", "Grid", new { employeeID = "#=EmployeeID#" }))
                )
                .Pageable()
                .Sortable()
                .ToClientTemplate()               
        %>
    </script>
    <script>
        function dataBound() {
            this.expandRow(this.tbody.find("tr.k-master-row").first());
        }
    </script>
</asp:Content>
