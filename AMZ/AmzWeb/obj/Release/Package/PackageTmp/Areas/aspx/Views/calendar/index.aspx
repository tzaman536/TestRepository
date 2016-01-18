<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content" style="text-align: center;">
    <h4>Pick a date</h4>
    <%= Html.Kendo().Calendar()
            .Name("calendar")
    %>
</div>
</asp:Content>