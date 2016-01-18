<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
<h4>Uploaded files</h4>

<% if (TempData.ContainsKey("UploadedFiles")) { %>
    <div class="t-widget t-upload">
        <ul class="t-upload-files t-reset">
    <% foreach (var attachment in (IEnumerable<string>)TempData["UploadedFiles"])
       { %>
            <li class="t-file"><span class="t-icon t-success"></span><%= attachment%></li>
    <% } %>
        </ul>
    </div>
<% } else {%>
    -- None --
<% } %>

<p style="margin-top: 1em;">
    <a href='<%=Url.Action("Index", "Upload") %>' class="k-button k-primary">Go back</a>
</p>
</div>
</asp:Content>
