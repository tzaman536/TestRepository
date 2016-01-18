﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="add-product" class="demo-section k-content">
        <p class="title">Add new product</p>
        <ul id="fieldlist">
            <li>
                <label for="currency">Price:</label>
        <%= Html.Kendo().NumericTextBox<decimal>()
            .Name("currency")
            .Format("c")
            .Min(0)
            .Max(100)
            .Value(30)
            .HtmlAttributes(new { style = "width: 100%" })
        %>
     </li>
     <li>
        <label for="percentage">Price Discount:</label>
        <%= Html.Kendo().NumericTextBox<double>()
            .Name("percentage")
            .Format("p0")
            .Min(0)
            .Max(0.9)
            .Step(0.01)
            .Value(0.05)
            .HtmlAttributes(new { style = "width: 100%" })
        %>
    </li>
    <li>
        <label for="custom">Weight:</label>
        <%= Html.Kendo().NumericTextBox<double>()
            .Name("custom")
            .Format("#.00 kg")
            .Value(2)
            .HtmlAttributes(new { style = "width: 100%" })
        %>
   </li>
    <li>
        <label for="numeric">Currently in stock:</label>
        <%= Html.Kendo().NumericTextBox<double>()
            .Name("numeric")
            .Value(17)
            .Placeholder("Enter numeric value")
            .HtmlAttributes(new { style = "width: 100%" })
        %>
    </li>
  </ul>
</div>
 <style>
    .demo-section {
        padding: 0;
    }

    #add-product .title {
        font-size: 16px;
        color: #fff;
        background-color: #1e88e5;
        padding: 20px 30px;
        margin: 0;
    }

    #fieldlist {
        margin: 0 0 -1.5em;
        padding: 30px;
    }

    #fieldlist li {
        list-style: none;
        padding-bottom: 1.5em;
    }

    #fieldlist label {
        display: block;
        padding-bottom: .6em;
        font-weight: bold;
        text-transform: uppercase;
        font-size: 12px;
        color: #444;
    }

</style>  

</asp:Content>
