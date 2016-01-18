﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>

<div id="example">
<%:Html.Kendo().Spreadsheet()
    .Name("spreadsheet")
    .HtmlAttributes(new { style = "width:100%" })
    .Rows(26)
    .Columns(30)
    .Sheetsbar(false)
    .Sheets(sheets =>
    {
        sheets.Add()
            .Name("ContactsForm")
            .MergedCells("A1:E1")
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
                rows.Add()
                    .Height(70)
                    .Cells(cells => cells
                        .Add()
                        .Index(0)
                        .Value("CONTACTS FORM")
                        .FontSize(32)
                        .Background("rgb(96,181,255)")
                        .Enable(false)
                        .TextAlign(SpreadsheetTextAlign.Center)
                        .Color("white"));

                rows.Add()
                    .Height(25)
                    .Cells(cells => {
                        cells.Add()
                            .Value("Full Name")
                            .Background("rgb(167,214,255)")
                            .TextAlign(SpreadsheetTextAlign.Center)
                            .Color("rgb(0,62,117)")
                            .Enable(false);

                        cells.Add()
                            .Value("Email")
                            .Background("rgb(167,214,255)")
                            .TextAlign(SpreadsheetTextAlign.Center)
                            .Color("rgb(0,62,117)")
                            .Enable(false);

                        cells.Add()
                            .Value("Date of Birth")
                            .Background("rgb(167,214,255)")
                            .TextAlign(SpreadsheetTextAlign.Center)
                            .Color("rgb(0,62,117)")
                            .Enable(false);

                        cells.Add()
                            .Value("Phone")
                            .Background("rgb(167,214,255)")
                            .TextAlign(SpreadsheetTextAlign.Center)
                            .Color("rgb(0,62,117)")
                            .Enable(false);

                        cells.Add()
                            .Value("Confirmed")
                            .Background("rgb(167,214,255)")
                            .TextAlign(SpreadsheetTextAlign.Center)
                            .Color("rgb(0,62,117)")
                            .Enable(false);
                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells => {
                        cells.Add()
                            .Value("Maria Anders")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A3)>3, LEN(A3)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("maria.anders@mail.com")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B3))), NOT(ISERROR(FIND(\".\", B3))), ISERROR(FIND(\" \", J1)), LEN(B3)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value(31232)
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value(0921123465)
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D3),LEN(D3)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value(true)
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));
                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value("Ana Trujillo")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A4)>3, LEN(A4)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("ana.trujillo@mail.com")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B4))), NOT(ISERROR(FIND(\".\", B4))), ISERROR(FIND(\" \", J1)), LEN(B4)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value(31222)
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value(55554729)
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D4),LEN(D4)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value(true)
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));
                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value("Antonio Moreno")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A5)>3, LEN(A5)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("antonio.moreno@mail.com")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B5))), NOT(ISERROR(FIND(\".\", B5))), ISERROR(FIND(\" \", J1)), LEN(B5)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value(32232)
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value("(5) 555-3932")
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D5),LEN(D5)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value(true)
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));

                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value("Thomas Hardy")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A6)>3, LEN(A6)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("thomas.hardy@mail.com")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B6))), NOT(ISERROR(FIND(\".\", B6))), ISERROR(FIND(\" \", J1)), LEN(B6)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value(21232)
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value(1715557788)
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D6),LEN(D6)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value(true)
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));

                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value("Christina Toms")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A7)>3, LEN(A7)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("christina.toms")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B7))), NOT(ISERROR(FIND(\".\", B7))), ISERROR(FIND(\" \", J1)), LEN(B7)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value("30102")
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value(0921123465)
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D7),LEN(D7)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value(true)
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));

                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value("Hanna Moos")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A8)>3, LEN(A8)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("hanna.moos@mail.com")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B8))), NOT(ISERROR(FIND(\".\", B8))), ISERROR(FIND(\" \", J1)), LEN(B8)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value(0)
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value(062108460)
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D8),LEN(D8)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value(true)
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));

                    });

                rows.Add()
                    .Height(25)
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value("")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(LEN(A9)>3, LEN(A9)200)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Full Name validation error")
                                .MessageTemplate("The full name should be longer than 3 letters and shorter than 200."));

                        cells.Add()
                            .Value("")
                            .Validation(validation => validation
                                .DataType("custom")
                                .From("AND(NOT(ISERROR(FIND(\"@\", B9))), NOT(ISERROR(FIND(\".\", B9))), ISERROR(FIND(\" \", J1)), LEN(B9)>5)")
                                .AllowNulls(true)
                                .Type("reject")
                                .TitleTemplate("Email validation error")
                                .MessageTemplate("The value entered is not an valid email address."));

                        cells.Add()
                           .Value("")
                           .Format("m/d/yyyy")
                           .Validation(validation => validation
                               .DataType("date")
                               .ComparerType("between")
                               .From("DATEVALUE(\"1/1/1900\")")
                               .To("DATEVALUE(\"1/1/1998\")")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Birth Date validaiton error")
                               .MessageTemplate("Birth Date should be between 1899 and 1998 year."));

                        cells.Add()
                           .Value("")
                           .Validation(validation => validation
                               .DataType("custom")
                               .From("AND(ISNUMBER(D9),LEN(D9)<14)")
                               .AllowNulls(true)
                               .Type("reject")
                               .TitleTemplate("Phone validation error")
                               .MessageTemplate("The value entered is not an valid phone number. Please enter numeric value with less than 14 digits."));

                        cells.Add()
                         .Value("")
                         .Validation(validation => validation
                             .DataType("list")
                             .From("ListValues!A1:B1")
                             .AllowNulls(true)
                             .Type("reject")
                             .TitleTemplate("Invalid value")
                             .MessageTemplate("Valid values are 'true' and 'false'."));

                    });
            });

            sheets.Add()
                .Name("ListValues")
                .Rows(rows => rows
                    .Add()
                    .Cells(cells =>
                    {
                        cells.Add()
                            .Value(true);
                        cells.Add()
                          .Value(false);
                    }));
    })

%>
</div>
</asp:Content>