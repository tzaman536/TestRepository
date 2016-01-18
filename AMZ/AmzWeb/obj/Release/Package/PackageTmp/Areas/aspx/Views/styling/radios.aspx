<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="demo-section k-content">
		    <h4>Select Car Engine</h4>
            <ul class="fieldlist">
              <li>
                   <%= Html.Kendo().RadioButton().Name("engine1").Checked(true).HtmlAttributes(new{@name = "engine"}).Label("1.4 Petrol, 92kW") %>
              </li>
    	      <li>
                   <%= Html.Kendo().RadioButton().Name("engine2").HtmlAttributes(new{@name = "engine"}).Label("1.8 Petrol, 118kW") %>
              </li>
              <li>
                   <%= Html.Kendo().RadioButton().Name("engine3").HtmlAttributes(new{@name = "engine"}).Label("2.0 Petrol, 147kW") %>
              </li>
              <li>
                   <%= Html.Kendo().RadioButton().Name("engine4").Enable(false).HtmlAttributes(new{@name = "engine"}).Label("3.6 Petrol, 191kW") %>
              </li>
              <li>
                   <%= Html.Kendo().RadioButton().Name("engine5").HtmlAttributes(new{@name = "engine"}).Label("1.6 Diesel, 77kW") %>
              </li>
              <li>
                   <%= Html.Kendo().RadioButton().Name("engine6").HtmlAttributes(new{@name = "engine"}).Label("2.0 Diesel, 103kW") %>
              </li>
              <li>
                   <%= Html.Kendo().RadioButton().Name("engine7").Enable(false).HtmlAttributes(new{@name = "engine"}).Label("2.0 Diesel, 125kW") %>
              </li>
            </ul>
  </div>    
  <style>
        .fieldlist {
            margin: 0 0 -1em;
            padding: 0;
        }

        .fieldlist li {
            list-style: none;
            padding-bottom: 1em;
        }
    </style>    
</asp:Content>