<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <h4>Information</h4>
        <p>
            The Upload can be used as a drop-in replacement
            for file input elements. This "synchronous" mode does not require
            special handling on the server.
        </p>
    </div>
    <form method="post" action='<%= Url.Action("Submit") %>'>
        <div class="demo-section k-content">
            <%= Html.Kendo().Upload()
                .Name("files")
            %>
            <p style="padding-top: 1em; text-align: right">
                <input type="submit" value="Submit" class="k-button k-primary" />
            </p>
        </div>
    </form>
</asp:Content>
