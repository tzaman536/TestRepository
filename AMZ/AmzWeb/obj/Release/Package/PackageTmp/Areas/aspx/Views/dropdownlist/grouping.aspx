<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <h4>Customers</h4>

    <%= Html.Kendo().DropDownList()
          .Name("customers")
          .DataTextField("ContactName")
          .DataValueField("CustomerID")
          .Height(400)
          .HtmlAttributes(new { style = "width:100%" })
          .DataSource(source =>  source
              .Custom()
              .Group(g => g.Add("Country", typeof(string)))
              .Transport(transport => transport
                .Read(read =>
                {
                    read.Action("Customers_Read", "DropDownList");
                })))
     %>
</div>
</asp:Content>