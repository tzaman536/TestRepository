<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content wide">
    <ul id="products" class="dairy-photos">
        <li><a href="#" data-id="11" title="A cheese made in the artisan tradition by rural dairy farmers in the north of Spain">
            <img src="<%:Url.Content("~/content/web/foods/11.jpg")%>" />
            Queso de Cabrales</a> </li>
        <li><a href="#" data-id="12" title="A cheese made in the La Mancha region of Spain from the milk of sheep of the Manchega breed">
            <img src="<%:Url.Content("~/content/web/foods/12.jpg")%>" />
            Queso<br />
            Manchego</a> </li>
        <li><a href="#" data-id="31" title="A veined Italian blue cheese, made from unskimmed cow's milk">
            <img src="<%:Url.Content("~/content/web/foods/31.jpg")%>" />
            Gorgonzola Telino</a> </li>
        <li><a href="#" data-id="32" title="An Italian cheese made from cream">
            <img src="<%:Url.Content("~/content/web/foods/32.jpg")%>" />
            Mascarpone Fabioli</a> </li>
        <li><a href="#" data-id="33" title="A white cheese made from goat milk">
            <img src="<%:Url.Content("~/content/web/foods/33.jpg")%>" />
            Geitost</a> </li>
        <li><a href="#" data-id="59" title="A semi-firm, cow's milk cheese">
            <img src="<%:Url.Content("~/content/web/foods/59.jpg")%>" />
            Raclette Courdavault</a> </li>
        <li><a href="#" data-id="60" title="A soft, creamy, surface-ripened cow's milk cheese">
            <img src="<%:Url.Content("~/content/web/foods/60.jpg")%>" />
            Camembert Pierrott</a> </li>
        <li><a href="#" data-id="72" title="A fresh cheese, originally from southern Italy, made from either buffalo's or cow's milk by the pasta filata method">
            <img src="<%:Url.Content("~/content/web/foods/72.jpg")%>" />
            Mozzarella di Giovanni</a> </li>
    </ul>
</div>

<%:Html.Kendo().Tooltip()
    .For("#products")
    .Filter("a")        
    .ContentTemplateId("template")
    .Position(TooltipPosition.Top)
    .Width(400)
    .Height(200)
%>

<script id="template" type="text/x-kendo-template">
    <img src="<%:Url.Content("~/content/web/foods/200/")%>#=target.data('id')#.jpg" alt="#=target.data('title')#" />
    <p>#=target.data('title')#</p>
</script>

<style>
     .dairy-photos {
        list-style-type: none;
        margin: 15px 0 15px 10px;
        padding: 0;
    }
    .dairy-photos li {
        display: inline-block;
        margin: 5px;
        padding: 0;
        vertical-align: top;
    }
    .dairy-photos a {
        display: block;
        width: 100px;
        height: 155px;
        padding: 0 0 0 10px;
        font-size: .9em;
        line-height: 1.2em;
        text-transform: uppercase;
        color: #777;
        background-color: rgba(255,255,255,0.8);
        -moz-box-shadow: 0 1px 2px 0 rgba(0,0,0,0.2);
        -webkit-box-shadow: 0 1px 2px 0 rgba(0,0,0,0.2);
        box-shadow: 0 1px 2px 0 rgba(0,0,0,0.2);
    }
    .dairy-photos a:hover {
        color: #fff;
        background-color: #000;
    }
    .dairy-photos a img {
        margin: 0 0 5px -10px;
    }
    .k-tooltip-content {
        vertical-align: middle;
    }
    .k-tooltip-content img,
    .k-tooltip-content p {
        float: left;
    }
    .k-tooltip-content p {
        font-size: 1.4em;
        padding: 20px;
        width: 160px;
        text-align: left;
    }
</style>


</asp:Content>