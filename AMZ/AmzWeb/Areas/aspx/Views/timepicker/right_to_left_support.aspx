<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

 <div class="demo-section k-content k-rtl">
    <h4>Select time</h4>
    <%= Html.Kendo().TimePicker()
            .Name("timepicker")
            .HtmlAttributes(new { style = "width: 100%" })
    %>

</div>

</asp:Content>