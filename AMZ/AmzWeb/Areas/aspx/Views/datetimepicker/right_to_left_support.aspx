<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <div class="k-rtl">
    <h4>Choose date:</h4>
    <%= Html.Kendo().DateTimePicker()
          .Name("datetimepicker")
          .HtmlAttributes(new { style = "width:100%;" })
    %>
    </div>
</div>
</asp:Content>