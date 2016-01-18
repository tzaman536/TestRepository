<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content k-rtl">
    <h4>Set Value</h4>
    <%= Html.Kendo().MaskedTextBox()
          .Name("maskedtextbox")
          .Mask("(999) 000-0000")
          .HtmlAttributes(new { style = "width: 100%" })
    %>
</div>
</asp:Content>