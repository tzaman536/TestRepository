<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.ProductViewModel>()
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(p => p.ProductName);
            columns.Bound(p => p.UnitPrice).Width(120);
            columns.Bound(p => p.UnitsInStock).Width(120);
            columns.Bound(p => p.Discontinued).Width(120);
            columns.Command(command => { command.Edit(); command.Destroy(); }).Width(250);
        })
        .ToolBar(toolbar => toolbar.Create())
        .Editable(editable => editable.Mode(GridEditMode.InLine))
        .Pageable()
        .Scrollable()
        .HtmlAttributes(new { style = "height:550px;" })
        .DataSource(dataSource => dataSource
            .Ajax()
            .PageSize(20)
            .Events(events => events.Error("error_handler"))
            .Model(model => model.Id(p => p.ProductID))
            .Read(read => read.Action("EditingCustomValidation_Read", "Grid"))
            .Update(update => update.Action("EditingCustomValidation_Update", "Grid"))
            .Destroy(destroy => destroy.Action("EditingCustomValidation_Destroy", "Grid"))
            .Create(create => create.Action("EditingCustomValidation_Create", "Grid"))
        )
    %>
    <script type="text/javascript">

        //register custom validation rules
        (function ($, kendo) {
            $.extend(true, kendo.ui.validator, {
                rules: { // custom rules
                    productnamevalidation: function (input, params) {
                        if (input.is("[name='ProductName']") && input.val() != "") {
                            input.attr("data-productnamevalidation-msg", "Product Name should start with capital letter");
                            return /^[A-Z]/.test(input.val());
                        }

                        return true;
                    }
                },
                messages: { //custom rules messages
                    productnamevalidation: function (input) {
                        // return the message text
                        return input.attr("data-val-productnamevalidation");
                    }
                }
            });
        })(jQuery, kendo);

        //show server errors if any
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
