<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

 <div class="demo-section k-content">
     <h4>Set alarm time</h4>

    <%= Html.Kendo().TimePicker()
            .Name("timepicker")
            .Value("10:00 AM")
            .HtmlAttributes(new { style = "width: 100%" })
    %>
        <h4 style="padding-top: 2em;">Alarm descriprion</h4>
        <input id="descr" class="k-textbox" type="text" value="Wake up" style="width: 100%" />
</div>
</asp:Content>