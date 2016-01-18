using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using IOFile = System.IO.File;

namespace Kendo.Mvc.Examples.Models
{
    public static class NavigationProvider
    {
        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public static IEnumerable<NavigationWidget> SuiteWidgets()
        {
            var TITLES = new Dictionary<String, String>() {
                { "application", "Application"},
                { "buttongroup", "ButtonGroup"},
                { "actionsheet", "ActionSheet"},
                { "collapsible", "Collapsilble"},
                { "drawer", "Drawer"},
                { "mobile-button", "Button"},
                { "mobile-forms", "Forms"},
                { "mobile-layout", "Layout"},
                { "mobile-listview", "ListView"},
                { "mobile-styling", "Styling"},
                { "mobile-tabstrip", "TabStrip"},
                { "mobile-view", "View"},
                { "modalview", "ModalView"},
                { "navbar", "NavBar"},
                { "popover", "PopOver"},
                { "scroller", "Scroller"},
                { "scrollview", "ScrollView"},
                { "splitview", "SplitView"},
                { "switch", "Switch"},
                { "touchevents", "Touch Events" }
            };



            var navigationJson = IOFile.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/nav.json"));

            var mobileNavJson = IOFile.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/mobile-nav.json"));

            var mobileItems = Serializer.Deserialize<IEnumerable<NavigationExample>>(mobileNavJson);

            var widgets = Serializer.Deserialize<IEnumerable<NavigationWidget>>(navigationJson).ToList();
            foreach (var item in mobileItems)
            {
                var widgetName = item.Url.Split('/')[0];
                var widget = widgets.Find(w => w.Name == widgetName);

                if (widget == null)
                {
                    widget = new NavigationWidget { Name = widgetName, Text = TITLES[widgetName], Items = new List<NavigationExample>() };
                    widgets.Add(widget);
                }

                widget.Items.Add(item);
            }

            return widgets;
        }
    }
}