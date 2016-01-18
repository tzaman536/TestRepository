﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content wide">
    <table id="weather" class="weather">
        <thead>
            <tr>
                <th class="month">MONTH</th>
                <th>MAX TEMP &deg;C</th>
                <th>WIND SPEED KM/H</th>
                <th>RAINFALL MM</th>
            </tr>
        </thead>
        <tbody>
<%
string[] months = { "January", "February", "March", "April", "May", "June" };
for (int i = 0; i < months.Length; i++) {
    var monthNumber = i + 1;
    var readUrl = Url.Action("_Weather", "Sparklines", new { station = "SOFIA", year = 2012, month = monthNumber });
%>
        <tr>
            <td class="month"><%= months[i] %></td>
            <td>
                <%= Html.Kendo().Sparkline()
                        .Name("sparkline-tmax-" + i)
                        .DataSource(ds => ds.Read(read => read.Url(readUrl)))
                        .Series(series => series
                            .Column("TMax").Color("#ff0000").NegativeColor("#0099ff")
                        )
                %>
            </td>
            <td>
                <%= Html.Kendo().Sparkline()
                        .Name("sparkline-wnd-" + i)
                        .DataSource(ds => ds.Read(read => read.Url(readUrl)))
                        .Series(series => series
                            .Line("Wind").Color("#5b8f00")
                        )
                %>
            </td>
            <td>
                <%= Html.Kendo().Sparkline()
                        .Name("sparkline-rain-" + i)
                        .DataSource(ds => ds.Read(read => read.Url(readUrl)))
                        .Series(series => series
                            .Area("Rain").Color("#0099ff")
                        )
                %>
            </td>
        </tr>
<%
}
%>
        </tbody>
    </table>
</div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .weather {
            border-collapse: collapse;
            line-height: 50px;
            width: 100%;
        }
        .weather td, .weather th {
            padding: 0;
            text-align: center;
        }
        .weather .month, .weather .month {
            text-align: right;
            padding-right: 20px;
        }
    </style>
</asp:Content>
