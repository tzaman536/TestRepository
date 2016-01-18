<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

 <div class="demo-section k-content">
    <h4>Customers</h4>
    <%= Html.Kendo().DropDownList()
          .Name("customers")
          .HtmlAttributes(new { style = "width: 100%" })
          .DataTextField("ContactName")
          .DataValueField("CustomerID")
          .DataSource(source =>
          {
              source.Read(read =>
              {
                  read.Action("GetCustomers", "Home");
              });
          })
          .Height(400)
          .HeaderTemplate("<div class=\"dropdown-header k-widget k-header\">" +
                        "<span>Photo</span>" +
                        "<span>Contact info</span>" +
                    "</div>")
          .Template("<span class=\"k-state-default\" style=\"background-image: url(" + Url.Content("~/Content/web/Customers/") + "#:data.CustomerID#.jpg\"></span>" +
                    "<span class=\"k-state-default\"><h3>#: data.ContactName #</h3><p>#: data.CompanyName #</p></span>")
          .ValueTemplate("<span class=\"selected-value\" style=\"background-image: url(" + Url.Content("~/Content/web/Customers/") + "#:data.CustomerID#.jpg\"></span><span>#:data.ContactName#</span>")
%>
    <p class="demo-hint">
        Open the DropDownList to see the customized appearance.
    </p>
</div>

<style>

    .dropdown-header {
        border-width: 0 0 1px 0;
        text-transform: uppercase;
    }

    .dropdown-header > span {
        display: inline-block;
        padding: 10px;
    }

    .dropdown-header > span:first-child {
        width: 50px;
    }

    .selected-value {
        display: inline-block;
        vertical-align: middle;
        width: 24px;
        height: 24px;
        background-size: 100%;
        margin-right: 5px;
        border-radius: 50%;
    }

    #customers-list .k-item {
        line-height: 1em;
        min-width: 300px;
    }
                
    /* Material Theme padding adjustment*/
                
    .k-material #customers-list .k-item,
    .k-material #customers-list .k-item.k-state-hover,
    .k-materialblack #customers-list .k-item,
    .k-materialblack #customers-list .k-item.k-state-hover {
        padding-left: 5px;
        border-left: 0;
    }

    #customers-list .k-item > span {
        -webkit-box-sizing: border-box;
        -moz-box-sizing: border-box;
        box-sizing: border-box;
        display: inline-block;
        vertical-align: top;
        margin: 20px 10px 10px 5px;
    }

    #customers-list .k-item > span:first-child {
        -moz-box-shadow: inset 0 0 30px rgba(0,0,0,.3);
        -webkit-box-shadow: inset 0 0 30px rgba(0,0,0,.3);
        box-shadow: inset 0 0 30px rgba(0,0,0,.3);
        margin: 10px;
        width: 50px;
        height: 50px;
        border-radius: 50%;
        background-size: 100%;
        background-repeat: no-repeat;
    }

    #customers-list h3 {
        font-size: 1.2em;
        font-weight: normal;
        margin: 0 0 1px 0;
        padding: 0;
    }

    #customers-list p {
        margin: 0;
        padding: 0;
        font-size: .8em;
    }

</style>
</asp:Content>
