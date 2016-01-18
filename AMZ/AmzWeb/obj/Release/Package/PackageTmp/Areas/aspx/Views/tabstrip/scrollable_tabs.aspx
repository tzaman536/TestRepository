<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div class="demo-section k-content">

<% Html.Kendo().TabStrip()
    .Name("tabstrip")
    .SelectedIndex(0)
    .Items(items =>
    {
        items.Add().Text("Tab 1 with long text")
                .Content(() => 
                  { %>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer felis libero, lobortis ac rutrum quis, varius a velit.</p>
                <% });

        items.Add().Text("Tab 2 with long text")
                .Content(() => 
                  { %>
                    <p>Donec lacus erat, cursus sed porta quis, adipiscing et ligula. Duis volutpat, sem pharetra accumsan pharetra, mi ligula cursus felis, ac aliquet leo diam eget risus.</p>
                <% });

        items.Add().Text("Tab 3 with long text")
                .Content(() => 
                  { %>
                    <p>Integer facilisis, justo cursus venenatis vehicula, massa nisl tempor sem, in ullamcorper neque mauris in orci.</p>
                <% });
    
        items.Add().Text("Tab 4 with long text")
                .Content(() => 
                  { %>
                    <p>Ut orci ligula, varius ac consequat in, rhoncus in dolor. Mauris pulvinar molestie accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae.</p>
                <% });
    
    })
    .Render();
%>

</div>

</asp:Content>