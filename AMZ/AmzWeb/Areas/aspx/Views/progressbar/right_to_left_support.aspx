﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="k-rtl demo-section k-content">
        <h4>ProgressBar</h4>
        <div id="progressBar"></div>
        <%= Html.Kendo().ProgressBar()
              .Name("progressBar")
              .Type(ProgressBarType.Percent)
              .Animation(a => a.Duration(600))
              .Events(e => e.Complete("onComplete"))
        %>
        <button id="startProgress" class="k-button">Start progress</button>
    </div>

    <script>
        $(document).ready(function () {
            $("#progressBar").kendoProgressBar({
                type: "percent",
                animation: {
                    duration: 600
                },
                complete: onComplete
            });
        });

        $("#startProgress").click(function () {
            if (!$(this).hasClass("k-state-disabled")) {
                $(this).addClass("k-state-disabled");

                progress();
            }
        });

        function onComplete() {
            $("#startProgress").removeClass("k-state-disabled").text("Restart Progress");
        }

        function progress() {
            var pb = $("#progressBar").data("kendoProgressBar");
            pb.value(0);

            var interval = setInterval(function () {
                if (pb.value() < 100) {
                    pb.value(pb.value() + 10);
                } else {
                    clearInterval(interval);
                }
            }, 100);
        }
    </script>

    <style>
       #progressBar {
            width: 100%;
            margin-bottom: 10px;
        }
        .demo-section {
            text-align: right;
        }
    </style>

</asp:Content>