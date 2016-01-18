﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content wide">
<%= Html.Kendo().ToolBar()
    .Name("ToolBar")
    .Items(items => {
        items.Add().Type(CommandType.Button).Text("Button");
        items.Add().Type(CommandType.Button).Text("Toggle Button").Togglable(true);
        items.Add().Type(CommandType.SplitButton).Text("Insert").MenuButtons(menuButtons =>
        {
            menuButtons.Add().Text("Insert above").Icon("insert-n");
            menuButtons.Add().Text("Insert between").Icon("insert-m");
            menuButtons.Add().Text("Insert below").Icon("insert-s");
        });
        items.Add().Type(CommandType.Separator);
        items.Add().Template("<label>Format:</label>");
        items.Add().Template("<input id='dropdown' style='width: 150px;' />").Overflow(ShowInOverflowPopup.Never);
        items.Add().Type(CommandType.Separator);
        items.Add().Type(CommandType.ButtonGroup).Buttons(buttons =>
        {
            buttons.Add().Text("Left").Togglable(true).Group("text-align").SpriteCssClass("k-tool-icon k-justifyLeft");
            buttons.Add().Text("Center").Togglable(true).Group("text-align").SpriteCssClass("k-tool-icon k-justifyCenter");
            buttons.Add().Text("Right").Togglable(true).Group("text-align").SpriteCssClass("k-tool-icon k-justifyRight");
        });
        items.Add().Type(CommandType.ButtonGroup).Buttons(buttons =>
        {
            buttons.Add().Text("Bold").Togglable(true).SpriteCssClass("k-tool-icon k-bold").ShowText(ShowIn.Overflow);
            buttons.Add().Text("Italic").Togglable(true).SpriteCssClass("k-tool-icon k-italic").ShowText(ShowIn.Overflow);
            buttons.Add().Text("Underline").Togglable(true).SpriteCssClass("k-tool-icon k-underline").ShowText(ShowIn.Overflow);
        });
        items.Add().Type(CommandType.Button).Text("Action").Overflow(ShowInOverflowPopup.Always);
        items.Add().Type(CommandType.Button).Text("Another Action").Overflow(ShowInOverflowPopup.Always);
        items.Add().Type(CommandType.Button).Text("Something else here").Overflow(ShowInOverflowPopup.Always);
    })
%>
</div>

<script>
    $(document).ready(function () {
        $("#dropdown").kendoDropDownList({
            optionLabel: "Paragraph",
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Heading 1", value: 1 },
                { text: "Heading 2", value: 2 },
                { text: "Heading 3", value: 3 },
                { text: "Title", value: 4 },
                { text: "Subtitle", value: 5 },
            ]
        });
    });
</script>

</asp:Content>