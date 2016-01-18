<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section hidden-on-narrow k-content wide">
    <div id="bike">
        <div id="bike-tail"></div><div id="bike-head"></div>
    </div>

    <div class="picker-wrapper">
        <h4>Tail color</h4>
    <%= Html.Kendo().FlatColorPicker()
        .Name("tail")
        .HtmlAttributes(new { @class = "picker" })
        .Value("#000")
        .Events(ev => ev.Change("select"))
        .Preview(false)
    %>
    </div>
    <div class="picker-wrapper">
        <h4>Front &amp; side color</h4>
    <%= Html.Kendo().FlatColorPicker()
        .Name("head")
        .HtmlAttributes(new { @class = "picker" })
        .Value("#e15613")
        .Events(ev => ev.Change("select"))
        .Preview(false)
    %>
</div>
</div>

<div class="responsive-message"></div>

<script>
function select(e) {
    $("#bike-" + this.element.attr("id")).css("background-color", e.value);
}
</script>

<style>
.demo-section {
        text-align: center;
        padding: 0 0 16px;
    }

#bike {
    margin: 0 0 10px;
    background: url(<%= Url.Content("~/content/web/colorpicker/background.png") %>);
}

#bike-head, #bike-tail {
    background: url(<%= Url.Content("~/content/web/colorpicker/motor.png") %>);
        display: inline-block;
        height: 299px;
        width: 241px;
}

    #bike-tail {
        background-color: #000;
    }

    #bike-head {
        background-color: #e15613;
        background-position: -241px 0;
    }

    .picker-wrapper {
        display: inline-block;
        text-align: left;
        width: 252px;
        margin: 0 18px;
    }

    .picker-wrapper .k-hsv-gradient {
        height: 140px;
    }

    .picker-wrapper h3 {
        padding: 13px 0 5px;
        text-align: left;
    }
</style>
</asp:Content>