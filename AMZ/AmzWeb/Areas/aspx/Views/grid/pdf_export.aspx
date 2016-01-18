<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /*
            Use the DejaVu Sans font for display and embedding in the PDF file.
            The standard PDF fonts have no support for Unicode characters.
        */
        .k-grid {
            font-family: "DejaVu Sans", "Arial", sans-serif;
        }

        /* Hide the Grid header and pager during export */
        .k-pdf-export .k-grid-toolbar,
        .k-pdf-export .k-pager-wrap
        {
            display: none;
        }
    </style>

    <!-- Load Pako ZLIB library to enable PDF compression -->
    <script src="<%= Url.Content("~/Scripts/pako.min.js") %>"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="box wide">
    <p style="margin-bottom: 1em"><b>Important:</b></p>

    <p style="margin-bottom: 1em">This page loads pako_deflate.min.js.  This enables compression
    in the PDF, and it is required if your dataset is very large.
    Chrome is known to crash on grids with lots of pages when pako is
    not loaded.</p>

    <p>In order for the output to be precise, and for Unicode support,
    you must declare TrueType fonts.  Please read the information about
    fonts
    <a href="http://docs.telerik.com/kendo-ui/framework/drawing/drawing-dom#custom-fonts-and-pdf">here</a>
    and <a href="http://docs.telerik.com/kendo-ui/framework/drawing/pdf-output#using-custom-fonts">here</a>.
    This demo renders the grid in "DejaVu Sans" font family, which is
    declared in kendo.common.css, but it also declares the paths to the
    font files using <tt>kendo.pdf.defineFont</tt>, because the
    stylesheet is hosted on a different domain.
    </p>
</div>

<%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.EmployeeViewModel>()    
    .Name("grid")    
    .HtmlAttributes( new { style = "height: 500px;" } )
    .Columns(columns =>
    {
        columns.Bound(e => e.EmployeeID).Width(140).Title("Picture");
        columns.Bound(e => e.Title).Width(350).Title("Details");
        columns.Bound(e => e.Title).Title("Country");
        columns.Bound(e => e.EmployeeID).Title("EmployeeID");
    })
    .ClientRowTemplate(
        "<tr data-uid='#: uid #'>" +
            "<td class='photo'>" +
               "<img src='" + Url.Content("~/Content/web/Employees/") +"#:data.EmployeeID#.jpg' alt='#: data.EmployeeID #' />" +
            "</td>" +
            "<td class='details'>" +
                "<span class='name'>#: FirstName# #: LastName#</span>" +
                "<span class='title'>Title: #: Title #</span>" +
            "</td>" +
            "<td class='country'>" +
                "#: Country #" +
            "</td>" +
            "<td class='employeeID'>" +
                "#: EmployeeID #" +
            "</td>" +
         "</tr>"       
    )
    .ClientAltRowTemplate(
        "<tr class='k-alt' data-uid='#: uid #'>" +
            "<td class='photo'>" +
                "<img src='" + Url.Content("~/Content/web/Employees/") + "#:data.EmployeeID#.jpg' alt='#: data.EmployeeID #' />" +
            "</td>" +
            "<td class='details'>" +
                "<span class='name'>#: FirstName# #: LastName#</span>" +
                "<span class='title'>Title: #: Title #</span>" +
            "</td>" +
            "<td class='country'>" +
                "#: Country #" +
            "</td>" +
            "<td class='employeeID'>" +
                "#: EmployeeID #" +
            "</td>" +
            "</tr>"
    )
    .ToolBar(tools => tools.Pdf())
    .Pdf(pdf => pdf
        .AllPages()
        .FileName("Kendo UI Grid Export.pdf")
        .ProxyURL(Url.Action("Pdf_Export_Save", "Grid"))
    )
    .DataSource(dataSource => dataSource
        .Ajax()                 
        .Read(read => read.Action("Pdf_Export_Read", "Grid"))
        .PageSize(5)
    )
    .Scrollable()
    .Pageable()
%>

<style>
    .employeeID,
    .country {
        font-size: 50px;
        font-weight: bold;
        color: #898989;
    }
    .name {
        display: block;
        font-size: 1.6em;
    }
    .title {
        display: block;
        padding-top: 1.6em;
    }
    td.photo, .employeeID {
        text-align: center;
    }
    .k-grid-header .k-header {
        padding: 10px 20px;
    }
    .k-grid tr {
        background: -moz-linear-gradient(top,  rgba(0,0,0,0.05) 0%, rgba(0,0,0,0.15) 100%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(0,0,0,0.05)), color-stop(100%,rgba(0,0,0,0.15)));
        background: -webkit-linear-gradient(top,  rgba(0,0,0,0.05) 0%,rgba(0,0,0,0.15) 100%);
        background: -o-linear-gradient(top,  rgba(0,0,0,0.05) 0%,rgba(0,0,0,0.15) 100%);
        background: -ms-linear-gradient(top,  rgba(0,0,0,0.05) 0%,rgba(0,0,0,0.15) 100%);
        background: linear-gradient(to bottom,  rgba(0,0,0,0.05) 0%,rgba(0,0,0,0.15) 100%);
        padding: 20px;
    }
    .k-grid tr.k-alt {
        background: -moz-linear-gradient(top,  rgba(0,0,0,0.2) 0%, rgba(0,0,0,0.1) 100%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(0,0,0,0.2)), color-stop(100%,rgba(0,0,0,0.1)));
        background: -webkit-linear-gradient(top,  rgba(0,0,0,0.2) 0%,rgba(0,0,0,0.1) 100%);
        background: -o-linear-gradient(top,  rgba(0,0,0,0.2) 0%,rgba(0,0,0,0.1) 100%);
        background: -ms-linear-gradient(top,  rgba(0,0,0,0.2) 0%,rgba(0,0,0,0.1) 100%);
        background: linear-gradient(to bottom,  rgba(0,0,0,0.2) 0%,rgba(0,0,0,0.1) 100%);
    }
</style>
</asp:Content>
