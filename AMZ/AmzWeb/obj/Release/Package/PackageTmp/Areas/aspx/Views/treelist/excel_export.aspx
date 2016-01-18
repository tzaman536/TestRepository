<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<Kendo.Mvc.Examples.Models.TreeList.EmployeeDirectoryModel>>" %>

    
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

<%: Html.Kendo().TreeList<Kendo.Mvc.Examples.Models.TreeList.EmployeeDirectoryModel>()
    .Name("treelist")
    .Columns(columns =>
    {
        columns.Add().Field(e => e.FirstName).Width(220);
        columns.Add().Field(e => e.LastName);
        columns.Add().Field(e => e.Position);
        columns.Add().Field(e => e.Address);
    })
    .Toolbar(tools => tools.Excel())
    .Excel(excel => excel.FileName("Kendo UI TreeList Export.xlsx").ProxyURL(Url.Action("Excel_Export_Save")))
    .Filterable()
    .Sortable()
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