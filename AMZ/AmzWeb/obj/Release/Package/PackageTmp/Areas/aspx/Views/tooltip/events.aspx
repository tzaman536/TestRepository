<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <span id="autohide-true" class="k-button wider">Hover me!</span> <span id="autohide-false"
        class="k-button wider">Hover me too!</span>
</div>
<div class="box">
    <h4>Console log</h4>
    <div class="console"></div>
</div>
            
<%: Html.Kendo().Tooltip()
        .For("#autohide-true")        
        .Position(TooltipPosition.Top)
        .Content("Hello!")
        .Width(120)
        .Events(events => events.Hide("onHide").Show("onShow"))
%>
    
<%:Html.Kendo().Tooltip()
    .For("#autohide-false")    
    .AutoHide(false)
    .Position(TooltipPosition.Top)
    .Content("Hello!")
    .Width(120)
    .Events(events => events.Hide("onHide").Show("onShow"))
%>
<script type="text/javascript">
    function onShow(e) {
        kendoConsole.log("event :: show");
    }

    function onHide(e) {
        kendoConsole.log("event :: hide");
    }
</script>
<style>
    .demo-section {
        text-align: center;
    }
    .wider {
        display: block;
        margin: 20px 0;
        padding: 15px 8px;
        line-height: 23px;
        width: 100%;
    }
</style>

</asp:Content>