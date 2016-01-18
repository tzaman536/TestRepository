<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="wrapper">
        <%= Html.Kendo().TabStrip()
              .Name("tabstrip")
              .Items(tabstrip =>
              {
                  tabstrip.Add().Text("Dimensions & Weights")
                      .Selected(true)
                      .LoadContentFrom(Url.Content("~/Content/web/tabstrip/ajax/ajaxContent1.html"));

                  tabstrip.Add().Text("Engine")
                      .LoadContentFrom(Url.Content("~/Content/web/tabstrip/ajax/ajaxContent2.html"));

                  tabstrip.Add().Text("Chassis")
                      .LoadContentFrom(Url.Content("~/Content/web/tabstrip/ajax/ajaxContent3.html"));
              })
        %>
    </div>
<style>
    .wrapper {
        height: 455px;
        margin: 20px auto;
        padding: 20px 0 0 0;
        background: url('<%=Url.Content("~/Content/web/tabstrip/bmw.png") %>') no-repeat center 60px transparent;
    }
   #tabstrip {
        max-width: 400px;
        float: right;
        margin-bottom: 20px;
    }
    #tabstrip .k-content
    {
        height: 320px;
        overflow: auto;
    }
    .specification {
        max-width: 670px;
        margin: 10px 0;
        padding: 0;
    }
    .specification dt, dd {
        max-width: 140px;
        float: left;
        margin: 0;
        padding: 5px 0 8px 0;
    }
    .specification dt {
        clear: left;
        width: 100px;
        margin-right: 7px;
        padding-right: 0;
        opacity: 0.7;
    }
    .specification:after, .wrapper:after {
        content: ".";
        display: block;
        clear: both;
        height: 0;
        visibility: hidden;
    }
</style>
</asp:Content>