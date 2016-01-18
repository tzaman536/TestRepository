﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content">
    <h4>Buttons</h4>

<%= Html.Kendo().Button()
    .Name("textButton")
    .Content("Text button")
    .HtmlAttributes( new {type = "button"} )
    .Events(ev => ev.Click("onClick")) %>

<%= Html.Kendo().Button()
    .Name("refreshButton")
    .Icon("refresh")
    .Content("Refresh Button")
    .HtmlAttributes( new {type = "button"} )
    .Events(ev => ev.Click("onClick")) %>

<%= Html.Kendo().Button()
    .Name("disabledButton")
    .Enable(false)
    .Content("Disabled button")
    .HtmlAttributes( new {type = "button"} )
    .Events(ev => ev.Click("onClick")) %>

    <p class="demo-hint">The disabled button will not fire click events.</p>
</div>

<div class="box">                
    <h4>Console log</h4>
    <div class="console"></div>
</div>

<script>
    function onClick(e) {
        kendoConsole.log("event :: click (" + $(e.event.target).closest(".k-button").attr("id") + ")");
    }
</script>

<style>
    .demo-section {
        line-height: 4em;
    }
    .demo-section .k-button {
        margin-right: 10px;
    }
</style>

</asp:Content>