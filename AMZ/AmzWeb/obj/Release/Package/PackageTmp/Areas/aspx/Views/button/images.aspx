<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content">
    <h4>Kendo UI Button with icons</h4>

<%= Html.Kendo().Button()
    .Name("iconButton")
    .HtmlAttributes( new {type = "button"} )
    .SpriteCssClass("k-icon netherlandsFlag")
    .Content("Sprite icon") %>

<%= Html.Kendo().Button()
    .Name("kendoIconButton")
    .HtmlAttributes( new {type = "button"} )
    .Icon("funnel")
    .Content("Kendo UI sprite icon") %>

<%= Html.Kendo().Button()
    .Name("imageButton")
    .HtmlAttributes( new {type = "button"} )
    .ImageUrl(Url.Content("~/Content/shared/icons/sports/snowboarding.png"))
    .Content("Image icon") %>
    
</div>

<style>
    .demo-section {
        line-height: 4em;
    }
    .demo-section .k-button {
        margin-right: 10px;
    }
    .k-button .k-image {
        height: 16px;
    }
    .netherlandsFlag {
        background-image: url('<%= Url.Content("~/Content/shared/flags.png") %>');
        background-position: 0 -64px;
    }
</style>


</asp:Content>