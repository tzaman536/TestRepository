<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<%: Html.Kendo().Map()
        .Name("map")
        .Center(51.505, -0.09)
        .Zoom(3)
        .Layers(layers =>
        {
            layers.Add()
                  .Type(MapLayerType.Bing)
                  .ImagerySet(MapLayersImagerySet.AerialWithLabels)
                  
                  // IMPORTANT: This key is locked to demos.telerik.com/kendo-ui
                  // Please replace with your own Bing Key
                  .Key("AqaPuZWytKRUA8Nm5nqvXHWGL8BDCXvK8onCl2PkC581Zp3T_fYAQBiwIphJbRAK");
        })
%>

</asp:Content>