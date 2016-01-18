<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<form class="box">
    <h4>Animation Settings</h4>
    <ul class="options">
        <li>
            <input name="animation" type="radio" <%= ViewBag.animation == "toggle" ? "checked=\"checked\"" : "" %> value="toggle" /> <label for="toggle">toggle animation</label>
        </li>
        <li>
            <input name="animation" type="radio" <%= ViewBag.animation != "toggle" ? "checked=\"checked\"" : "" %> value="expand" /> <label for="expand">expand animation</label>
        </li>
        <li>
            <%= Html.CheckBox("opacity", (bool)ViewBag.opacity) %> <label for="opacity">animate opacity</label>
        </li>
        <li>
            <button class="k-button">Apply</button>
        </li>
    </ul>
</form>

<div class="demo-section k-content">
    <h4>Conversation history</h4>

<%= Html.Kendo().PanelBar()
    .Name("panelbar")
    .Animation(animation =>
    {
        animation.Enable(ViewBag.animation == "expand" || ViewBag.opacity);
        
        animation.Expand(config =>
        {
            if (ViewBag.animation != "toggle")
            {
                config.Expand();
            }

            if (ViewBag.opacity != null)
            {
                config.Fade(FadeDirection.In);
            }
        });
    })
    .Items(panelbar =>
    {
        panelbar.Add().Text("Today")
            .Expanded(true)
            .Items(items =>
            {
                items.Add().Text("Jane King");
                items.Add().Text("Bob Fuller");
                items.Add().Text("Lynda Kallahan");
                items.Add().Text("Matt Sutnar");
            });

        panelbar.Add().Text("Yesterday")
            .Items(items =>
            {
                items.Add().Text("Stewart");
                items.Add().Text("Jane King");
                items.Add().Text("Steven");
                items.Add().Text("Ken Stone");
            });

        panelbar.Add().Text("Wednesday, February 01, 2012")
            .Items(items =>
            {
                items.Add().Text("Jane King");
                items.Add().Text("Lynda Kallahan");
                items.Add().Text("Todd");
                items.Add().Text("Bob Fuller");
            });

        panelbar.Add().Text("Tuesday, January 31, 2012")
            .Items(items =>
            {
                items.Add().Text("Emily Davolio");
                items.Add().Text("Matt Sutnar");
                items.Add().Text("Bob Fuller");
                items.Add().Text("Jenn Heinlein");
            });

        panelbar.Add().Text("Monday, January 30, 2012")
            .Items(items =>
            {
                items.Add().Text("Matt Sutnar");
                items.Add().Text("Joshua");
                items.Add().Text("Michael");
                items.Add().Text("Jenn Heinlein");
            });
    })
%>
</div>
</asp:Content>