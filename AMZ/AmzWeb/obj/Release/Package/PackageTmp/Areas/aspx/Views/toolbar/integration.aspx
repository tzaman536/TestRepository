﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content wide">
    <h4>Customize the element</h4>
    <%= Html.Kendo().ToolBar()
        .Name("ToolBar")
        .Resizable(false)
        .Items(items => {
            items.Add().Template("<label>SHAPE:</label><input id='shape' />");
            items.Add().Type(CommandType.Separator);
            items.Add().Template("<label>H:</label><input id='height' style='width: 70px;' type='text' />");
            items.Add().Template("<label>W:</label><input id='width' style='width: 70px;' type='text' />");
            items.Add().Type(CommandType.Separator);
            items.Add().Template("<label>POSITION:</label>");
            items.Add().Type(CommandType.ButtonGroup).Buttons(buttons =>
            {
                buttons.Add().Text("Right").Togglable(true).Group("position");
                buttons.Add().Text("Center").Togglable(true).Group("position").Selected(true);
                buttons.Add().Text("Left").Togglable(true).Group("position");
            });
            items.Add().Type(CommandType.Separator);
            items.Add().Template("<label>BACKGROUND:</label><div type='color' id='background-picker'></div>");
            items.Add().Type(CommandType.Separator);
            items.Add().Template("<label>BORDER:</label><div type='color' id='border-picker'></div>");
            items.Add().Template("<input id='border-style' style='width: 100px;' />");
        })
        .Events(e => e.Toggle("onToggle"))        
    %>
</div>
<div class="box wide">
    <div id="target"></div>
</div>

<script>
    var target;

    function onToggle(e) {
        var position = e.target.text().toLowerCase();

        if (position == "center") {
            position = "none";
        }

        target.css("float", position);
    }

    $(document).ready(function () {
        target = $("#target");

        $("#shape").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Rectangle", value: 0 },
                { text: "Rounded rectangle", value: 30 },
                { text: "Circle/Ellipse", value: 300 }
            ],
            change: function () {
                target.css("border-radius", parseInt(this.value()));
            }
        });

        $("#border-style").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: [
                { text: "Solid", value: "solid" },
                { text: "Dashed", value: "dashed" },
                { text: "Dotted", value: "dotted" }
            ],
            change: function () {
                target.css("border-style", this.value());
            }
        });

        $("#background-picker").kendoColorPicker({
            value: "#ffffff",
            buttons: false,
            change: function () {
                target.css("background-color", this.value());
            }
        });

        $("#border-picker").kendoColorPicker({
            value: "#333333",
            buttons: false,
            change: function () {
                target.css("border-color", this.value());
            }
        });

        $("#height, #width").kendoNumericTextBox({
            value: 150,
            decimals: 0,
            format: "n0",
            max: 300,
            min: 50,
            change: function () {
                var dimension = this.element.attr("id");

                target.css(dimension, this.value());
            }
        });
    });
</script>

<style>
    #toolbar {
        margin: 1em 0;
        padding: 0 .9em;
    }
                
    #toolbar label {
        font-size: .85em;
        font-weight: bold;
    }
    #toolbar .k-separator {
        margin: 0 .9em;
    }
    #target {
        border: 3px solid #333333;
        border-radius: 0;
        background-color: #ffffff;
        height: 150px;
        width: 150px;
        margin: 0 auto;
    }
</style>

</asp:Content>