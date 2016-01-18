﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="example">
    <div class="demo-section k-content">
        <h4>Disable weekends</h4>
        <%= Html.Kendo().DateTimePicker()
                .Name("weekend-datetime-picker")
                .HtmlAttributes(new { style = "width:100%;" })
                .DisableDates(new string[] { "sa", "su" })
            %>

        <h4 style="margin-top: 1em">Disable Federal Holidays in USA in 2015</h4>
        <%= Html.Kendo().DateTimePicker()
                .Name("national-datetime-picker")
                .HtmlAttributes(new { style = "width:100%;" })
                .DisableDates("disableDates")
            %>
    </div>
    <script>
        function disableDates(date) {
            var dates = [
              new Date("1/1/2015"),
              new Date("1/19/2015"),
              new Date("2/16/2015"),
              new Date("4/16/2015"),
              new Date("5/10/2015"),
              new Date("5/25/2015"),
              new Date("6/21/2015"),
              new Date("7/3/2015"),
              new Date("9/7/2015"),
              new Date("10/12/2015"),
              new Date("11/11/2015"),
              new Date("11/26/2015"),
              new Date("11/27/2015"),
              new Date("12/25/2015")
            ];

            if (date && compareDates(date, dates)) {
                return true;
            } else {
                return false;
            }
        }

        function compareDates(date, dates) {
            for (var i = 0; i < dates.length; i++) {
                if (dates[i].getDate() == date.getDate() &&
                  dates[i].getMonth() == date.getMonth() &&
                    dates[i].getYear() == date.getYear()) {
                    return true
                }
            }
        }
    </script>
</div>

</asp:Content>
