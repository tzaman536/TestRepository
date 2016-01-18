<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="demo-section k-content k-rtl">
    <h4>RTL Slider</h4>
    <%= Html.Kendo().Slider()
            .Name("slider")
            .Min(0)
            .Max(30)
            .SmallStep(1)
            .LargeStep(10)
            .Value(18)
    %>
</div>

<div class="demo-section k-content k-rtl">
    <h4>RTL RangeSlider</h4>
    <%= Html.Kendo().RangeSlider()
            .Name("rangeslider")
            .Min(0)
            .Max(10)
            .SmallStep(1)
            .LargeStep(10)
    %>
</div>

</asp:Content>