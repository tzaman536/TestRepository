<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<div class="k-rtl">
    <div class="demo-section k-content">
        <h4>Pick a day:</h4>
        <%= Html.Kendo().Calendar()
                .Name("calendar")
        %>
    </div>
</div>

</asp:Content>