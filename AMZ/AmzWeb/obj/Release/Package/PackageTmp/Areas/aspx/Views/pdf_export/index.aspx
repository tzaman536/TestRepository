<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="example">
    <div class="box">
        <h4>Export page content</h4>
        <div class="box-col">
            <button class='export-pdf k-button'>Export as PDF</button>
        </div>
        <div class="box-col">
            <button class='export-img k-button'>Export as Image</button>
        </div>
        <div class="box-col">
            <button class='export-svg k-button'>Export as SVG</button>
        </div>
    </div>

    <div class="demo-section k-header export-app">
        <div class="demo-section content-wrapper">
          <div class="demo-section charts-wrapper">
              <%: Html.Kendo().Chart()
                 .Name("users")
                 .Series(series => {
                     series.Column(new[] { 340, 894, 1345, 1012, 3043, 2013, 2561, 2018, 2435, 3012 }).Name("Users Reached");
                     series.Column(new[] { 50, 80, 120, 203, 324, 297, 176, 354, 401, 3492 }).Name("Applications");
                 })
                 .ValueAxis(axis => axis.Numeric()
                     .Labels(labels => labels.Visible(false))
                     .Line(line => line.Visible(false))
                     .MajorGridLines(lines => lines.Visible(false))
                 )
                 .CategoryAxis(axis => axis
                     .Categories(new [] {2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011 })
                     .Line(line => line.Visible(false))
                     .MajorGridLines(line => line.Visible(false))
                 )
                 .ChartArea(chartArea => chartArea.Background("none"))
                 .Tooltip(tooltip => tooltip.Visible(true).Format("{0}").Template("${ series.name } : ${ value }"))
             %>
             <%: Html.Kendo().Chart()
                 .Name("applications")
                 .Legend(legend => legend.Position(ChartLegendPosition.Bottom))
                 .Series(series => series
                     .Donut(new [] { new { source = "Approved", percentage = 237 }, new { source="Rejected", percentage = 112} } )
                     .Field("percentage")
                     .CategoryField("source")
                 )
                 .ChartArea(chartArea => chartArea.Background("none"))
                 .Tooltip(tooltip => tooltip.Visible(true).Template("${ category } - ${ value } applications"))
             %>
             <%: Html.Kendo().Chart()
                 .Name("referrals")
                 .Legend(legend => legend.Position(ChartLegendPosition.Bottom))
                 .Series(series => series
                     .Pie(new [] { 
                             new { source = "Dev", percentage = 42 }, 
                             new { source="Sales", percentage = 18}, 
                             new { source = "Finance", percentage = 29 }, 
                             new { source = "Legal", percentage = 11 } 
                         } 
                     )
                     .Field("percentage")
                     .CategoryField("source")
                 )
                 .ChartArea(chartArea => chartArea.Background("none"))
                 .Tooltip(tooltip => tooltip.Visible(true).Template("${ category } - ${ value }"))
             %>
          </div>
          <%: Html.Kendo().Grid<Kendo.Mvc.Examples.Models.CustomerViewModel>()
                .Name("grid")
                .Columns(columns =>
                {
                    columns.Bound(c => c.ContactName).Title("Contact").ClientTemplate("<div class=\'customer-name\'>#: ContactName #</div>").Width(200);
                    columns.Bound(c => c.ContactTitle).Width(220);
                    columns.Bound(c => c.Phone).Width(160);
                    columns.Bound(c => c.CompanyName);
                    columns.Bound(c => c.City).Width(160);
                })
                .DataSource(ds => ds.Ajax()
                    .Read("Customers_Read", "Grid")
                    .PageSize(15)
                    .Group(group => group.Add(c => c.ContactTitle))
                )
                .Scrollable()
                .Groupable()
                .Sortable()
                .HtmlAttributes(new { style = "height: 450px" })
                .Reorderable(r => r.Columns(true))
                .Resizable(r => r.Columns(true))
                .Filterable()
                .Pageable(pager => pager.Refresh(true).PageSizes(true).ButtonCount(5))
          %>

        </div>

        <div class="menu-wrapper">
            <%:Html.Kendo().PanelBar()
                .Name("menu")
                .Items(items => {
                    items.Add().Text("Dashboard");
                    items.Add().Text("Job Openings").Items(children => {
                        children.Add().Text("USA");
                        children.Add().Text("Australia");
                        children.Add().Text("Germany");
                    });
                    items.Add().Text("Applicants");
                    items.Add().Text("Interview Calendar");
                    items.Add().Text("Events Calendar");
                })
            %>
        </div>
    </div>
</div>
<style>
    /*
        Use the DejaVu Sans font for display and embedding in the PDF file.
        The standard PDF fonts have no support for Unicode characters.
    */
    .k-widget {
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

    $(".export-pdf").click(function () {
        // Convert the DOM element to a drawing using kendo.drawing.drawDOM
        kendo.drawing.drawDOM($(".content-wrapper"))
        .then(function (group) {
            // Render the result as a PDF file
            return kendo.drawing.exportPDF(group, {
                paperSize: "auto",
                margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
            });
        })
        .done(function (data) {
            // Save the PDF file
            kendo.saveAs({
                dataURI: data,
                fileName: "HR-Dashboard.pdf",
                proxyURL: '<%: Url.Action("Pdf_Export_Save") %>'
            });
        });
    });

    $(".export-img").click(function () {
        // Convert the DOM element to a drawing using kendo.drawing.drawDOM
        kendo.drawing.drawDOM($(".content-wrapper"))
        .then(function (group) {
            // Render the result as a PNG image
            return kendo.drawing.exportImage(group);
        })
        .done(function (data) {
            // Save the image file
            kendo.saveAs({
                dataURI: data,
                fileName: "HR-Dashboard.png",
                proxyURL: '<%: Url.Action("Pdf_Export_Save") %>'
            });
        });
    });

    $(".export-svg").click(function () {
        // Convert the DOM element to a drawing using kendo.drawing.drawDOM
        kendo.drawing.drawDOM($(".content-wrapper"))
        .then(function (group) {
            // Render the result as a SVG document
            return kendo.drawing.exportSVG(group);
        })
        .done(function (data) {
            // Save the SVG document
            kendo.saveAs({
                dataURI: data,
                fileName: "HR-Dashboard.svg",
                proxyURL: '<%: Url.Action("Pdf_Export_Save") %>'
            });
        });
    });
</script>

<!-- Load Pako ZLIB library to enable PDF compression -->
<script src="http://kendo.cdn.telerik.com/2015.1.408/js/pako_deflate.min.js"></script>
<style>
.export-app {
    display: table;
    width: 100%;
    height: 750px;
    padding: 0;
}
.export-app .demo-section {
    margin: 0 auto;
    border: 0;
}

.menu-wrapper,
.content-wrapper {
    display: table-cell;
    vertical-align: top;
}

.menu-wrapper {
    width: 15%;
}

#menu {
    height: 100%;
    border-width: 0 0 0 1px;
}

.charts-wrapper {
    height: 250px;
    padding: 0 0 20px;
}

#applications,
#users,
#referrals {
    display: inline-block;
    width: 50%;
    height: 240px;
    vertical-align: top;
}
#applications,
#referrals {
    width: 24%;
    height: 250px;
}

.customer-photo {
    display: inline-block;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    background-size: 40px 44px;
    background-position: center center;
    vertical-align: middle;
    line-height: 41px;
    box-shadow: inset 0 0 1px #999, inset 0 0 10px rgba(0,0,0,.2);
}
.customer-name {
    display: inline-block;
    vertical-align: middle;
    line-height: 41px;
    padding-left: 10px;
}
</style>
</asp:Content>
