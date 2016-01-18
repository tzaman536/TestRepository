<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Balance</h4>
    <%= Html.Kendo().Slider()
            .Name("slider")
            .IncreaseButtonTitle("Right")
            .DecreaseButtonTitle("Left")
            .Min(-10)
            .Max(10)
            .SmallStep(2)
            .LargeStep(1)
            .Value(0)
            .HtmlAttributes(new { @class = "balSlider" })
    %>
    <h4 style="padding-top: 2em;">Equalizer</h4>
    <div id="equalizer">
        <%= Html.Kendo().Slider()
                .Name("eqSlider1")
                .Orientation(SliderOrientation.Vertical)
                .Min(-20)
                .Max(20)
                .SmallStep(1)
                .LargeStep(20)
                .ShowButtons(false)
                .Value(10)
                .HtmlAttributes(new { @class = "eqSlider" })
        %>

        <%= Html.Kendo().Slider()
                .Name("eqSlider2")
                .Orientation(SliderOrientation.Vertical)
                .Min(-20)
                .Max(20)
                .SmallStep(1)
                .LargeStep(20)
                .ShowButtons(false)
                .Value(5)
                .HtmlAttributes(new { @class = "eqSlider" })
        %>

        <%= Html.Kendo().Slider()
                .Name("eqSlider3")
                .Orientation(SliderOrientation.Vertical)
                .Min(-20)
                .Max(20)
                .SmallStep(1)
                .LargeStep(20)
                .ShowButtons(false)
                .Value(0)
                .HtmlAttributes(new { @class = "eqSlider" })
        %>

        <%= Html.Kendo().Slider()
                .Name("eqSlider4")
                .Orientation(SliderOrientation.Vertical)
                .Min(-20)
                .Max(20)
                .SmallStep(1)
                .LargeStep(20)
                .ShowButtons(false)
                .Value(10)
                .HtmlAttributes(new { @class = "eqSlider" })
        %>

        <%= Html.Kendo().Slider()
                .Name("eqSlider5")
                .Orientation(SliderOrientation.Vertical)
                .Min(-20)
                .Max(20)
                .SmallStep(1)
                .LargeStep(20)
                .ShowButtons(false)
                .Value(15)
                .HtmlAttributes(new { @class = "eqSlider" })
        %>
    </div>
</div>

<style>
    .demo-section {
        text-align: center;
    }

    #equalizer {
        padding-right: 15px;
    }

    div.balSlider {
        width: 100%;
    }

    div.balSlider .k-slider-selection {
        display: none;
    }

    div.eqSlider {
        display: inline-block;
        margin: 1em;
        height: 122px;
        vertical-align: top;
    }
</style>
</asp:Content>