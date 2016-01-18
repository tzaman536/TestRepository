<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.Kendo().Grid<Kendo.Mvc.Examples.Models.EmployeeViewModel>()
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(e => e.FirstName).Width(140);
            columns.Bound(e => e.LastName).Width(140);
            columns.Bound(e => e.Title);
            columns.Command(command => command.Custom("ViewDetails").Click("showDetails")).Width(180);
        })
        .Pageable()
        .HtmlAttributes(new { style = "height: 400px;" })
        .DataSource(dataSource => dataSource
            .Ajax()
            .PageSize(20)
            .Read(read => read.Action("CustomCommand_Read", "Grid"))
         )
    %>

    <%: Html.Kendo().Window().Name("Details")
        .Title("Customer Details")
        .Visible(false)
        .Modal(true)
        .Draggable(true)
        .Width(300)       
    %>

     <script type="text/x-kendo-template" id="template">
        <div id="details-container">
            <h2>#= FirstName # #= LastName #</h2>
            <em>#= Title #</em>
            <dl>
                <dt>City: #= City #</dt>
                <dt>Address: #= Address #</dt>
            </dl>
        </div>
    </script>

    <script type="text/javascript">
        var detailsTemplate = kendo.template($("#template").html());

        function showDetails(e) {
            e.preventDefault();
            
            var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
            var wnd = $("#Details").data("kendoWindow");

            wnd.content(detailsTemplate(dataItem));
            wnd.center().open();
        }
    </script>
    <style type="text/css">
        #details-container
        {
            padding: 10px;
        }

        #details-container h2
        {
            margin: 0;
        }

        #details-container em
        {
            color: #8c8c8c;
        }

        #details-container dt
        {
            margin:0;
            display: inline;
        }
    </style>
</asp:Content>
