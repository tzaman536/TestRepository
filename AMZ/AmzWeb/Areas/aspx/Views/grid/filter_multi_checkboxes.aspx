<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
     <div class="demo-section k-content wide">
    <h4>Client Operations</h4>
    <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.ProductViewModel>()
            .Name("client")
            .Columns(columns =>
            {
                columns.Bound(p => p.ProductName).Filterable(ftb => ftb.Multi(true).Search(true));
                columns.Bound(p => p.UnitPrice).Width(140).Filterable(ftb => ftb.Multi(true)).Format("{0:c}");
                columns.Bound(p => p.UnitsInStock).Width(140).Filterable(ftb => ftb.Multi(true));
                columns.Bound(p => p.Discontinued).Width(100).Filterable(ftb => ftb.Multi(true));
                columns.Command(command => command.Destroy()).Width(110);
            })
            .ToolBar(toolbar =>
            {
                toolbar.Create();
                toolbar.Save();
            })
            .HtmlAttributes(new { style = "height: 550px;" })
            .Editable(editable => editable.Mode(GridEditMode.InCell))
            .Filterable()
            .Pageable()
            .Navigatable()
            .Sortable()
            .Scrollable()
            .DataSource(dataSource => dataSource
                .Ajax()
                .Batch(true)
                .PageSize(20)                
                .ServerOperation(false)
                .Events(events => events.Error("error_handler"))
                .Model(model => model.Id(p => p.ProductID))
                .Create("Editing_Create", "Grid")
                .Read("Editing_Read", "Grid")
                .Update("Editing_Update", "Grid")
                .Destroy("Editing_Destroy", "Grid")
            )
    %>
    </div>
     <div class="demo-section k-content wide">
    <h4>Server Operations</h4>
    <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.EmployeeViewModel>()
            .Name("server")
            .Columns(columns =>
            {
                //when ServerOperations of the Grid is enabled, dataSource should be provided for all the Filterable Multi Check columns
                columns.Bound(e => e.FirstName).Width(220).Filterable(ftb => ftb.Multi(true)
                    .DataSource(ds => ds.Read(r => r.Action("Unique", "Grid").Data("{ field: 'FirstName' }")))
                );
                columns.Bound(e => e.LastName).Width(220).Filterable(ftb => ftb.Multi(true)
                    .DataSource(ds => ds.Read(r => r.Action("Unique", "Grid").Data("{ field: 'LastName' }")))
                );
                columns.Bound(e => e.Country).Width(220).Filterable(ftb => ftb.Multi(true).ItemTemplate("itemTemplate")
                    .DataSource(ds => ds.Read(r => r.Action("Unique", "Grid").Data("{ field: 'Country' }")))
                );
                columns.Bound(e => e.City).Width(220).Filterable(ftb => ftb.Multi(true).CheckAll(false).BindTo(new[]{
                        new { City = "Seatle" },
                        new { City = "Tacoma" },
                        new { City = "Kirkland" },
                        new { City = "Redmond" },
                        new { City = "London" }
                }));
                columns.Bound(e => e.Title).Filterable(ftb => ftb.Multi(true)
                    .DataSource(ds => ds.Read(r => r.Action("Unique", "Grid").Data("{ field: 'Title' }")))
                );
            })
            .Sortable()
            .Pageable()
            .Scrollable()
            .Filterable()
            .DataSource(dataSource => dataSource
                .Ajax()
                .PageSize(20)     
                .ServerOperation(true)
                .Read(read => read.Action("HierarchyBinding_Employees", "Grid"))
            )
    %>
  </div>
    <script type="text/javascript">
        function itemTemplate(e) {
            if (e.field == "all") {
                //handle the check-all checkbox template
                return "<div><label><strong><input type='checkbox' />#= all#</strong></label></div>";
            } else {
                //handle the other checkboxes
                return "<span><label><input type='checkbox' name='" + e.field + "' value='#=Country#'/><span>#= Country #</span></label></span>"
            }
        }


        function error_handler(e) {
            if (e.errors) {
                var message = "Errors:\n";
                $.each(e.errors, function (key, value) {
                    if ('errors' in value) {
                        $.each(value.errors, function () {
                            message += this + "\n";
                        });
                    }
                });
                alert(message);
            }
        }
    </script>
</asp:Content>
