<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<Kendo.Mvc.Examples.Models.TreeList.EmployeeDirectoryModel>>" %>
    
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<style>
    /*
        Use the DejaVu Sans font for display and embedding in the PDF file.
        The standard PDF fonts have no support for Unicode characters.
    */
    .k-treelist {
        font-family: "DejaVu Sans", "Arial", sans-serif;
        font-size: .9em;
    }
</style>

<script>
    // Import DejaVu Sans font for embedding

    // NOTE: Only required if the Kendo UI stylesheets are loaded
    // from a different origin, e.g. kendo.cdn.telerik.com
    kendo.pdf.defineFont({
        "DejaVu Sans"             : "http://kendo.cdn.telerik.com/2014.3.1314/styles/fonts/DejaVu/DejaVuSans.ttf",
        "DejaVu Sans|Bold"        : "http://kendo.cdn.telerik.com/2014.3.1314/styles/fonts/DejaVu/DejaVuSans-Bold.ttf",
        "DejaVu Sans|Bold|Italic" : "http://kendo.cdn.telerik.com/2014.3.1314/styles/fonts/DejaVu/DejaVuSans-Oblique.ttf",
        "DejaVu Sans|Italic"      : "http://kendo.cdn.telerik.com/2014.3.1314/styles/fonts/DejaVu/DejaVuSans-Oblique.ttf"
    });
</script>

<!-- Load Pako ZLIB library to enable PDF compression -->
<script src="<%: Url.Content("~/content/shared/js/pako.min.js") %>"></script>
<%: Html.Kendo().TreeList<Kendo.Mvc.Examples.Models.TreeList.EmployeeDirectoryModel>()
    .Name("treelist")
    .Columns(columns =>
    {
        columns.Add().Field(e => e.FirstName).Width(220);
        columns.Add().Field(e => e.LastName);
        columns.Add().Field(e => e.Position);
        columns.Add().Field(e => e.Address);
    })
    .Toolbar(tools => tools.Pdf())
    .Excel(excel => excel.FileName("Kendo UI TreeList Export.pdf").ProxyURL(Url.Action("Pdf_Export_Save")))
    .Filterable()
    .Sortable()
    .Scrollable(true)
    .Height(300)
    .DataSource(dataSource => dataSource
        .Read(read => read.Action("All", "EmployeeDirectory"))
        .ServerOperation(false)
        .Model(m => {
            m.Id(f => f.EmployeeId);
            m.ParentId(f => f.ReportsTo);
            m.Expanded(true);
            m.Field(f => f.FirstName);
            m.Field(f => f.LastName);
            m.Field(f => f.ReportsTo);
        })
    )
    .Height(540)
%>
</asp:Content>
