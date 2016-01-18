<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <div class="k-rtl">
        <h4>RTL DropDownList</h4>
        <%= Html.Kendo().DropDownList()
                .Name("dropdownlist")
                .DataTextField("Text")
                .DataValueField("Value")
                .BindTo(new List<SelectListItem>()
                {
                    new SelectListItem() {
                        Text = "Item1", Value = "1"  
                    },
                    new SelectListItem() {
                        Text = "Item2", Value = "2"  
                    },
                    new SelectListItem() {
                        Text = "Item3", Value = "3"  
                    }
                })
               .HtmlAttributes(new { style = "width: 100%" })
        %>
    </div>
</div>
</asp:Content>