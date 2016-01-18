<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Select time:</h4>
    <%= Html.Kendo().TimePicker()
            .Name("timepicker")
            .HtmlAttributes(new { style = "width:100%", accesskey = "w" })
    %>
 </div>

            
<div class="box">
    <div class="box-col">
        <h4>Focus</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button leftAlign wider"><a target="_blank" href="http://en.wikipedia.org/wiki/Access_key">Access key</a></span>
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
        <h4>Closed popup</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button wider rightAlign">enter</span>
                </span>
                <span class="button-descr">
                    triggers change event
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button">esc</span>
                </span>
                <span class="button-descr">
                    closes the popup
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button">alt</span>
                    <span class="key-button wider leftAlign">down arrow</span>
                </span>
                <span class="button-descr">
                    opens the popup
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button">alt</span>
                    <span class="key-button wider leftAlign">up arrow</span>
                </span>
                <span class="button-descr">
                    closes the popup
                </span>
            </li>
        </ul>
    </div>


    <div class="box-col">
        <h4>Opened popup (time view)</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button wide leftAlign">up arrow</span>
                </span>
                <span class="button-descr">
                    selects previous available time
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button wider leftAlign">down arrow</span>
                </span>
                <span class="button-descr">
                    selects next available time
                </span>
            </li>
        </ul>
    </div>

</div>


</asp:Content>