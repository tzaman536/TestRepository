<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
<%:Html.Kendo().PanelBar()
    .Name("panelbar")
    .ExpandMode(PanelBarExpandMode.Multiple)
    .Items(panelbar =>
    {
        panelbar.Add().Text("Projects")
            .Items(projects =>
            {
                projects.Add().Text("New Business Plan");

                projects.Add().Text("Sales Forecasts")
                    .Items(forecasts =>
                    {
                        forecasts.Add().Text("Q1 Forecast");
                        forecasts.Add().Text("Q2 Forecast");
                        forecasts.Add().Text("Q3 Forecast");
                        forecasts.Add().Text("Q4 Forecast");
                    });

                projects.Add().Text("Sales Reports");
            });

        panelbar.Add().Text("Programs")
            .Items(programs =>
            {
                programs.Add().Text("Monday");
                programs.Add().Text("Tuesday");
                programs.Add().Text("Wednesday");
                programs.Add().Text("Thursday");
                programs.Add().Text("Friday");
            });

        panelbar.Add().Text("Communication").Enabled(false);
    })
%>
</div>
<div class="box">
    <div class="box-col">
        <h4>Focus</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button leftAlign wider">Alt</span>
                    +
                    <span class="key-button">w</span>
                </span>
                <span class="button-descr">
                    focuses the widget
                </span>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>Supported keys and user actions</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button wider leftAlign">up arrow</span>
                </span>
                <span class="button-descr">
                    highlights previous item
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button wider leftAlign">left arrow</span>
                </span>
                <span class="button-descr">
                    highlights previous item
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button wider leftAlign">down arrow</span>
                </span>
                <span class="button-descr">
                    highlights next item
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button wider leftAlign">right arrow</span>
                </span>
                <span class="button-descr">
                    highlights next item
                </span>
            </li>
        </ul>
    </div>
    <div class="box-col">
        <h4>&nbsp;</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button">home</span>
                </span>
                <span class="button-descr">
                    selects first item in the list
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button">end</span>
                </span>
                <span class="button-descr">
                    selects last item in the list
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button wider rightAlign">enter</span>
                </span>
                <span class="button-descr">
                    selects highlighted item / toggles item's group
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button widest">space</span>
                </span>
                <span class="button-descr">
                    selects highlighted item / toggles item's group
                </span>
            </li>
        </ul>
    </div>
</div>

<script>
    $(document.body).keydown(function (e) {
        if (e.altKey && e.keyCode == 87) {
            $("#panelbar").focus();
        }
    });
</script>

</asp:Content>