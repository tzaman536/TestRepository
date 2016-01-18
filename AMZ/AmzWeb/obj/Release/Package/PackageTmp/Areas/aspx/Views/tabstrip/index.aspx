<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
    <% Html.Kendo().TabStrip()
          .Name("tabstrip")
          .Animation(animation => 
              animation.Open(effect => 
                  effect.Fade(FadeDirection.In)))
          .Items(tabstrip =>
          {
              tabstrip.Add().Text("Paris")
                  .Selected(true)
                  .Content(() => 
                  { %>
                    <div class="weather">
                        <h2>17<span>&ordm;C</span></h2>
                        <p>Rainy weather in Paris.</p>
                    </div>
                    <span class="rainy">&nbsp;</span>
                  <% });

              tabstrip.Add().Text("New York")
                  .Content(() => { %>
                    <div class="weather">
                        <h2>29<span>&ordm;C</span></h2>
                        <p>Sunny weather in New York.</p>
                    </div>
                    <span class="sunny">&nbsp;</span>
                  <% });

              tabstrip.Add().Text("Moscow")
                  .Content(() => { %>
                    <div class="weather">
                        <h2>16<span>&ordm;C</span></h2>
                        <p>Cloudy weather in Moscow.</p>
                    </div>
                    <span class="cloudy">&nbsp;</span>
                  <% });

              tabstrip.Add().Text("Sydney")
                  .Content(() => { %>
                    <div class="weather">
                        <h2>17<span>&ordm;C</span></h2>
                        <p>Rainy weather in Sidney.</p>
                    </div>
                    <span class="rainy">&nbsp;</span>
                  <% });
          })
          .Render();
    %>
</div>

<style>
    .sunny, .cloudy, .rainy {
        display: block;
        margin: 30px auto 10px;
        width: 128px;
        height: 128px;
        background: url('<%= Url.Content("~/Content/web/tabstrip/weather.png") %>') transparent no-repeat 0 0;
    }

    .cloudy{
        background-position: -128px 0;
    }

    .rainy{
        background-position: -256px 0;
    }

   .weather {
    margin: 0 auto 30px;
    text-align: center;
    }

     #tabstrip h2 {
    font-weight: lighter;
    font-size: 5em;
    line-height: 1;
    padding: 0 0 0 30px;
    margin: 0;
    }

    #tabstrip h2 span {
        background: none;
        padding-left: 5px;
        font-size: .3em;
        vertical-align: top;
    }

    #tabstrip p {
        margin: 0;
        padding: 0;
    }
</style>
</asp:Content>