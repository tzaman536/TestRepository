<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

<%:Html.Kendo().Spreadsheet()
    .Name("spreadsheet")
    .HtmlAttributes(new { style = "width:100%;" })
    .Sheets(sheets =>
    {
        sheets.Add()
            .Name("Food Order")
            .MergedCells("A1:G1", "C15:E15")
            .Columns(columns =>
            {
                columns.Add().Width(100);
                columns.Add().Width(215);
                columns.Add().Width(115);
                columns.Add().Width(115);
                columns.Add().Width(115);
                columns.Add().Width(155);
            })
            .Rows(rows =>
            {
                rows.Add().Height(70).Cells(cells =>
                {
                    cells.Add()
                        .Value("Invoice #52 - 06/23/2015")
                        .FontSize(32)
                        .Background("rgb(96,181,255)")
                        .TextAlign(SpreadsheetTextAlign.Center)
                        .Color("white");
                });

                rows.Add().Height(25).Cells(cells =>
                {
                    cells.Add()
                        .Value("ID")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Product")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Quantity")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Price")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Tax")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Amount")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                     cells.Add()
                        .Background("rgb(167,214,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(216321)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Calzone")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(1)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(12.39)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Formula("C3*D3*0.2")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Formula("C3*D3+E3")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                     cells.Add()
                        .Background("rgb(255,255,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(546897)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Margarita")
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(2)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(8.79)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C4*D4*0.2")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C4*D4+E4")
                        .Format("$#,##0.00");

                     cells.Add()
                        .Background("rgb(229,243,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(456231)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Pollo Formaggio")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(1)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(13.99)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Formula("C5*D5*0.2")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Formula("C5*D5+E5")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                     cells.Add()
                        .Background("rgb(255,255,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(455873)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Greek Salad")
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(1)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(9.49)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C6*D6*0.2")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C6*D6+E6")
                        .Format("$#,##0.00");

                     cells.Add()
                        .Background("rgb(229,243,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(456892)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Spinach and Blue Cheese")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(3)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(11.49)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Formula("C7*D7*0.2")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Formula("C7*D7+E7")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                     cells.Add()
                        .Background("rgb(255,255,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(546564)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Rigoletto")
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(1)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(10.99)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C8*D8*0.2")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C8*D8+E8")
                        .Format("$#,##0.00");

                     cells.Add()
                        .Background("rgb(229,243,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(789455)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Creme Brulee")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(5)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(6.99)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Formula("C9*D9*0.2")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Formula("C9*D9+E9")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                     cells.Add()
                        .Background("rgb(255,255,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(123002)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Radeberger Beer")
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(4)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(4.99)
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C10*D10*0.2")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("C10*D10+E10")
                        .Format("$#,##0.00");

                     cells.Add()
                        .Background("rgb(229,243,255)");
                });

                rows.Add().Cells(cells =>
                {
                    cells.Add()
                        .Value(564896)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value("Budweiser Beer")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Value(3)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Center);

                    cells.Add()
                        .Value(4.49)
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)")
                        .Format("$#,##0.00");

                    cells.Add()
                        .Formula("C11*D11*0.2")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Formula("C11*D11+E11")
                        .Format("$#,##0.00")
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                     cells.Add()
                        .Background("rgb(255,255,255)");
                });

                rows.Add().Index(11).Cells(cells =>
                {
                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(229,243,255)")
                        .Color("rgb(0,62,117)");

                });

                rows.Add().Index(12).Cells(cells =>
                {
                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                    cells.Add()
                        .Background("rgb(255,255,255)")
                        .Color("rgb(0,62,117)");

                });

                rows.Add().Index(13).Cells(cells =>
                {
                    cells.Add()
                        .Background("rgb(167,214,255)");

                    cells.Add()
                        .Background("rgb(167,214,255)");

                    cells.Add()
                        .Background("rgb(167,214,255)");

                    cells.Add()
                        .Background("rgb(167,214,255)");

                    cells.Add()
                        .Value("Tip")
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .TextAlign(SpreadsheetTextAlign.Right);

                    cells.Add()
                        .Background("rgb(167,214,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("SUM(F3:F11)*0.1")
                        .Format("$#,##0.00")
                        .Bold(true);

                    cells.Add()
                        .Background("rgb(167,214,255)");
                });

                rows.Add().Index(14).Height(50).Cells(cells =>
                {
                    cells.Add()
                        .Index(0)
                        .Background("rgb(193,226,255)");

                    cells.Add()
                        .Index(1)
                        .Background("rgb(193,226,255)");

                    cells.Add()
                        .Value("Total Amount")
                        .Index(2)
                        .TextAlign(SpreadsheetTextAlign.Right)
                        .Color("rgb(0,62,117)")
                        .FontSize(20)
                        .Background("rgb(193,226,255)");

                    cells.Add()
                        .Index(5)
                        .Background("rgb(193,226,255)")
                        .Color("rgb(0,62,117)")
                        .Formula("SUM(F3:F14)")
                        .Format("$#,##0.00")
                        .FontSize(20)
                        .Bold(true);

                     cells.Add()
                        .Index(6)
                        .Background("rgb(193,226,255)");
                });
            });
    })
%>
</asp:Content>