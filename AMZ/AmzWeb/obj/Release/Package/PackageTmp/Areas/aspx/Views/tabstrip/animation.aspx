<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


 <div class="demo-section k-content">

<% Html.Kendo().TabStrip()
    .Name("tabstrip")
    .Animation(animation =>
    {
        animation.Enable(ViewBag.animation == "expand:vertical" || ViewBag.opacity);

        animation.Open(config =>
        {
            if (ViewBag.animation != "toggle")
            {
                config.Expand();
            }

            if (ViewBag.opacity == true)
            {
                config.Fade(FadeDirection.In);
            }

            config.Duration(AnimationDuration.Fast);
        });
    })
    .SelectedIndex(0)
    .Items(tabstrip =>
    {
        tabstrip.Add().Text("First Tab")
                .Content(() => { %>
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer felis libero, lobortis ac rutrum quis, varius a velit. Donec lacus erat, cursus sed porta quis, adipiscing et ligula. Duis volutpat, sem pharetra accumsan pharetra, mi ligula cursus felis, ac aliquet leo diam eget risus. Integer facilisis, justo cursus venenatis vehicula, massa nisl tempor sem, in ullamcorper neque mauris in orci.</p>
                <% });

        tabstrip.Add().Text("Second Tab")
                .Content(() => { %>
                    <p>Ut orci ligula, varius ac consequat in, rhoncus in dolor. Mauris pulvinar molestie accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Aenean velit ligula, pharetra quis aliquam sed, scelerisque sed sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam dui mi, vulputate vitae pulvinar ac, condimentum sed eros.</p>
                <% });

        tabstrip.Add().Text("Third Tab")
                .Content(() => { %>
                    <p>Aliquam at nisl quis est adipiscing bibendum. Nam malesuada eros facilisis arcu vulputate at aliquam nunc tempor. In commodo scelerisque enim, eget sodales lorem condimentum rutrum. Phasellus sem metus, ultricies at commodo in, tristique non est. Morbi vel mauris eget mauris commodo elementum. Nam eget libero lacus, ut sollicitudin ante. Nam odio quam, suscipit a fringilla eget, dignissim nec arcu. Donec tristique arcu ut sapien elementum pellentesque.</p>
                <% });

        tabstrip.Add().Text("Fourth Tab")
                .Content(() => { %>
                    <p>Maecenas vitae eros vel enim molestie cursus. Proin ut lacinia ipsum. Nam at elit arcu, at porttitor ipsum. Praesent id viverra lorem. Nam lacinia elementum fermentum. Nulla facilisi. Nulla bibendum erat sed sem interdum suscipit. Vestibulum eget molestie leo. Aliquam erat volutpat. Ut sed nulla libero. Suspendisse id euismod quam. Aliquam interdum turpis vitae purus consectetur in pulvinar libero accumsan. In id augue dui, ac volutpat ante. Suspendisse purus est, ullamcorper id bibendum sed, placerat id leo.</p>
                <% });
    })
    .Render();
%>
</div>


<form>
     <div class="box">
                <h4>Animation Settings</h4>
    <ul class="options">
        <li>
            <input id="toggle" name="animation" type="radio" <%= ViewBag.animation == "toggle" ? "checked=\"checked\"" : "" %> value="toggle" /> <label for="toggle">toggle animation</label>
        </li>
        <li>
            <input id="expand" name="animation" type="radio" <%= ViewBag.animation != "toggle" ? "checked=\"checked\"" : "" %> value="expand:vertical" /> <label for="expand">expand animation</label>
        </li>
        <li>            
            <%= Html.CheckBox("opacity", (bool)ViewBag.opacity) %> <label for="opacity">animate opacity</label>
        </li>
    </ul>

    <button class="k-button">Apply</button>
         </div>
</form>
<style>
.demo-section {
        min-height: 250px;
    }
    .k-tabstrip > .k-content {
        padding: 20px;
    }
</style>
</asp:Content>


