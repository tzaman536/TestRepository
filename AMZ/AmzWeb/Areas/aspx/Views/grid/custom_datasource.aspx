<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.ProductViewModel>()    
    .Name("Grid")    
    .Columns(columns => {        
        columns.Bound(p => p.ProductName);
        columns.Bound(p => p.UnitPrice).Width(140);
        columns.Bound(p => p.UnitsInStock).Width(140);
        columns.Bound(p => p.Discontinued).Width(100);
        columns.Command(command => command.Destroy()).Width(110);
    })
    .ToolBar(toolbar => {
        toolbar.Create();
        toolbar.Save();        
    })
    .Editable(editable => editable.Mode(GridEditMode.InCell))
    .Pageable()
    .Navigatable()
    .Sortable()
    .Scrollable()
    .DataSource(dataSource => dataSource        
        .Custom()         
        .Batch(true)
        .PageSize(20)
        .Schema(schema => schema.Model(m => m.Id(p => p.ProductID)))
        .Transport(transport =>
        {
            transport.Read(read =>
               read.Url("http://demos.telerik.com/kendo-ui/service/products")
                   .DataType("jsonp")
            );
            transport.Create(create =>
               create.Url("http://demos.telerik.com/kendo-ui/service/products/create")
                     .DataType("jsonp")
            );
            transport.Update(update =>
               update.Url("http://demos.telerik.com/kendo-ui/service/products/update")
                     .DataType("jsonp")
            );
            transport.Destroy(destroy =>
               destroy.Url("http://demos.telerik.com/kendo-ui/service/products/destroy")
                      .DataType("jsonp")
            );
            transport.ParameterMap("parameterMap");
        })
    )
%>
<script>
    function parameterMap(options, operation) {
        if (operation !== "read" && options.models) {
            return { models: kendo.stringify(options.models) };
        }
    }
</script>
</asp:Content>