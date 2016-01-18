﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="box wide">
        <div class="box-col">
            <h4>Toggle state</h4>
            <ul class="options">
                <li>
                    <button class="k-button" id="enableProgressBar">Enable</button>
                    <button class="k-button" id="disableProgressBar">Disable</button>
                </li>
            </ul>
        </div>
        <div class="box-col">
            <h4>Value</h4>
            <ul class="options">
                <li>
                    <input type="text" id="newValue" value="25" class="k-textbox" placeholder="e.g. 23"/>             
                    <button class="k-button" id="setProgressBarValue">Set value</button>
                    <button class="k-button" id="getProgressBarValue">Get value</button>
                </li>
            </ul>
        </div>
        <div class="box-col">
            <h4>Indeterminate</h4>
            <ul class="options">
                <li>
                    <button class="k-button" id="setIndeterminate">Set indeterminate</button>
                </li>
            </ul>
        </div>
    </div>
    <div class="demo-section k-content">
        <%= Html.Kendo().ProgressBar()
              .Name("progressBar")
              .HtmlAttributes(new { style = "width: 100%;" })
              .Min(0)
              .Max(100)
              .Type(ProgressBarType.Value)
              .Animation(a => a.Duration(400))
        %>
    </div>

    <script>
        $(function () {
            var pb = $("#progressBar").data("kendoProgressBar");

            $("#setProgressBarValue").click(function () {
                pb.value($("#newValue").val());
            });

            $("#getProgressBarValue").click(function () {
                alert("Current ProgressBar value is: " + pb.value());
            });

            $("#setIndeterminate").click(function () {
                pb.value(false);
            });

            $("#enableProgressBar").click(function () {
                pb.enable();
            });

            $("#disableProgressBar").click(function () {
                pb.enable(false);
            });
        });
    </script>

    <style>
        .box .k-textbox {
            margin: 0;
            width: 80px;
        }
        .k-button {
            min-width: 80px;
        }
    </style>
</asp:Content>