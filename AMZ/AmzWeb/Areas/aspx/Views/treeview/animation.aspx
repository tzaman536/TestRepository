﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Animation", "TreeView", FormMethod.Post))
  {%>
   <div class="box">
     <h4>Animation Settings</h4>
     <ul class="options">
        <li>
            <%= Html.RadioButton("animation", "toggle") %>
            <%= Html.Label("toggle", "toggle animation") %>            
        </li>
        <li>
            <%= Html.RadioButton("animation", "expand") %>
            <%= Html.Label("expand", "expand animation") %>            
        </li>
        <li>
            <%= Html.CheckBox("opacity") %>
            <%= Html.Label("opacity", "animate opacity") %>               
        </li>
    </ul>

    <button class="k-button">Apply</button>
   </div>
  <% }
%>


<div class="demo-section k-content">
    <%:Html.Kendo().TreeView()
        .Name("treeview")
        .Animation(animation =>
        {
            animation.Expand(open =>
            {
                if (ViewBag.animation == "expand")
                {
                    open.Expand(ExpandDirection.Vertical);
                }

                if (ViewBag.animation == "slide")
                {
                    open.SlideIn(SlideDirection.Down);
                }

                if (ViewBag.opacity)
                {
                    open.Fade(FadeDirection.In);
                }
            });
        })
        .Items(treeview =>
        {
            treeview.Add().Text("Furniture")
                .Items(root =>
                {
                    root.Add().Text("Tables & Chairs");
                    root.Add().Text("Sofas");
                    root.Add().Text("Occasional Furniture");
                    root.Add().Text("Childerns Furniture");
                    root.Add().Text("Beds");
                });

            treeview.Add().Text("Decor")
                .Expanded(true)
                .Items(root =>
                {
                    root.Add().Text("Bed Linen");
                    root.Add().Text("Throws");
                    root.Add().Text("Curtains & Blinds");
                    root.Add().Text("Rugs");
                    root.Add().Text("Carpets");
                });

            treeview.Add().Text("Storage")
                .Items(root =>
                {
                    root.Add().Text("Wall Shelving");
                    root.Add().Text("Kids Storage");
                    root.Add().Text("Baskets");
                    root.Add().Text("Multimedia Storage");
                    root.Add().Text("Floor Shelving");
                    root.Add().Text("Toilet Roll Holders");
                    root.Add().Text("Storage Jars");
                    root.Add().Text("Drawers");
                    root.Add().Text("Boxes");
                });
        })
    %>
</div>

 <style>
    /* demo style, do not show treeview scrollbars */
    div.k-treeview { overflow: visible; }
</style>
</asp:Content>

