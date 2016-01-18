<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="tshirt-view" class="demo-section k-content">
    <h4>Customize your Kendo T-shirt</h4>
    <img id="tshirt" src='<%= Url.Content("~/Content/web/combobox/tShirt.png") %>' />
    <h4>T-shirt Fabric</h4>
    <%= Html.Kendo().ComboBox()
          .Name("fabric")
          .Filter("contains")
          .Placeholder("Select fabric...")
          .DataTextField("Text")
          .DataValueField("Value")
          .BindTo(new List<SelectListItem>() {
              new SelectListItem() {
                Text = "Cotton", Value = "1"   
              },
              new SelectListItem() {
                Text = "Polyester", Value = "2"   
              },
              new SelectListItem() {
                Text = "Cotton/Polyester", Value = "3"   
              },
              new SelectListItem() {
                Text = "Rib Knit", Value = "4"   
              }
          })
          .SelectedIndex(3)
          .Suggest(true)
          .HtmlAttributes(new { style="width:100%;" })
    %>

    <h4 style="margin-top: 2em;">T-shirt Size</h4> 
    <%= Html.Kendo().ComboBox()
          .Name("size")
          .Placeholder("Select size...")
          .BindTo(new List<string>() {
              "X-Small",
              "Small",
              "Medium",
              "Large",
              "X-Large",
              "2X-Large"
          })
          .SelectedIndex(0)
          .Suggest(true)
          .HtmlAttributes(new { style="width:100%;" })
    %>

    <button class="k-button k-primary" id="get" style="margin-top: 2em; float: right;">Customize</button>
</div>
<style>
    #tshirt {
        display: block;
        margin: 2em auto;
    }
    .k-readonly
    {
        color: gray;
    }
</style>

<script>
    $(document).ready(function() {
        var fabric = $("#fabric").data("kendoComboBox");
        var size = $("#size").data("kendoComboBox");

		$("#get").click(function() {
		    alert('Thank you! Your Choice is:\n\nFabric ID: ' + fabric.value() + ' and Size: ' + size.value());
        });
    });
</script>
</asp:Content>