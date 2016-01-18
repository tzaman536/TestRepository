<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="demo-section hidden-on-narrow k-content wide" style="min-width: 910px;">
        <h4>Rearrange the photos</h4>
        <div id="sortable-horizontal">
            <img src="<%:Url.Content("~/content/web/sortable/1.jpg")%>" /><img
            src="<%:Url.Content("~/content/web/sortable/2.jpg")%>" /><img
            src="<%:Url.Content("~/content/web/sortable/3.jpg")%>" /><img
            src="<%:Url.Content("~/content/web/sortable/4.jpg")%>" /><img
            src="<%:Url.Content("~/content/web/sortable/5.jpg")%>" />
        </div>
    </div>
    
    <div class="responsive-message"></div>

    <%:Html.Kendo().Sortable()
        .For("#sortable-horizontal")
        .ContainerSelector("#sortable-horizontal")
        .Axis(SortableAxis.X)
        .Cursor("move")
        .Placeholder("<div class='placeholder'>Drop Here!</div>")
        .HintHandler("hint")
    %>

    <script>
        function hint(element) {
            return element.clone().addClass("tooltip");
        }
    </script>

    <style>
        #example {
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

        #sortable-horizontal {
            overflow: hidden;
            width: 930px;
            text-align: center;
        }

        #sortable-horizontal img {
            width: 166px;
            margin: 10px 20px 10px 0;
            vertical-align: middle;
            cursor: move;
        }

        .placeholder {
            display: inline-block;
            width: 164px;
            height: 123px;
            border: 1px dashed #ddd;
            background-color: #f3f5f7;
            margin: 10px 20px 10px 0;
            font-size: 1.3em;
            font-weight: bold;
            line-height: 125px;
            vertical-align: middle;
            color: #777;
        }

        .tooltip {
            opacity: .6;
        }
    </style>
</asp:Content>