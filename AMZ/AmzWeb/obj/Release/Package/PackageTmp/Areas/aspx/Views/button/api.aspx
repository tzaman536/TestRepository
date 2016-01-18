<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="box">
    <div class="box-col">
        <h4>Button API Functions</h4>
        <ul class="options">
            <li>
                <button class="k-button" id="enableButton" type="button">Enable</button>
                <button class="k-button" id="disableButton" type="button">Disable</button>
            </li>
        </ul>
    </div>
</div>

<div class="demo-section k-content">
<%= Html.Kendo().Button()
    .Name("iconTextButton")
    .Icon("ungroup")
    .HtmlAttributes( new {type = "button"} )
    .Content("Kendo UI Button") %>
</div>

<script>

$(document).ready(function () {
    var buttonObject = $("#iconTextButton").data("kendoButton");

    $("#enableButton").click(function () {
        buttonObject.enable(true);
    });

    $("#disableButton").click(function () {
        buttonObject.enable(false);
    });
});

</script>

<style>
    .demo-section {
        text-align: center;
    }
    .box .k-textbox {
        margin: 0;
        width: 80px;
    }
</style>


</asp:Content>