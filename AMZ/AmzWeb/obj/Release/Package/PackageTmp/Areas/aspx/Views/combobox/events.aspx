<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>ComboBox</h4>
    <%= Html.Kendo().ComboBox()
            .Name("combobox")
            .DataTextField("Text")
            .DataValueField("Value")
            .Filter("startswith")
            .HtmlAttributes(new { style = "width:100%;" })
            .BindTo(new List<SelectListItem>()
            {
                new SelectListItem() {
                    Text = "Item 1", Value = "1"  
                },
                new SelectListItem() {
                    Text = "Item 2", Value = "2"  
                },
                new SelectListItem() {
                    Text = "Item 3", Value = "3"  
                }
            })
            .Events(e =>
            {
                e.Change("onChange").Select("onSelect").Open("onOpen").Close("onClose").DataBound("onDataBound").Filtering("onFiltering");
            })
    %>
</div>
<div class="box">                
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<script>
    function onOpen() {
        kendoConsole.log("event: open");
    }

    function onClose() {
        kendoConsole.log("event: close");
    }

    function onChange() {
        kendoConsole.log("event: change");
    }

    function onDataBound() {
        kendoConsole.log("event: dataBound");
    }

    function onFiltering() {
        if ("kendoConsole" in window) {
            kendoConsole.log("event: filtering");
        }
    }

    function onSelect(e) {
        if ("kendoConsole" in window) {
            var dataItem = this.dataItem(e.item.index());
            kendoConsole.log("event :: select (" + dataItem.Text + " : " + dataItem.Value + ")");
        }
    }
</script>
</asp:Content>