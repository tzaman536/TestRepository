<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
       <div id="special-days">
       <%=Html.Kendo().Calendar()
        .Name("calendar")
        .Value(DateTime.Today)
        .Footer(" ")
        .MonthTemplate("# if ($.inArray(+data.date, events) != -1) { #" +
                                "<div class='" +
                            "# if (data.value < 10) { #" +
                                "exhibition" +
                            "# } else if ( data.value < 20 ) { #" +
                                "party" +
                            "# } else { #" +
                                "cocktail" +
                            "# } #" +
                        "'>#= data.value #</div>" +
                    "# } else { #" +
                    "#= data.value #" +
                    "# } #")                     
         %>
    </div>
</div>



<script>
    var today = new Date(),
        events = [
            +new Date(today.getFullYear(), today.getMonth(), 8),
            +new Date(today.getFullYear(), today.getMonth(), 12),
            +new Date(today.getFullYear(), today.getMonth(), 24),
            +new Date(today.getFullYear(), today.getMonth() + 1, 6),
            +new Date(today.getFullYear(), today.getMonth() + 1, 7),
            +new Date(today.getFullYear(), today.getMonth() + 1, 25),
            +new Date(today.getFullYear(), today.getMonth() + 1, 27),
            +new Date(today.getFullYear(), today.getMonth() - 1, 3),
            +new Date(today.getFullYear(), today.getMonth() - 1, 5),
            +new Date(today.getFullYear(), today.getMonth() - 2, 22),
            +new Date(today.getFullYear(), today.getMonth() - 2, 27)
        ];
</script>

<style>

    #calendar {
         width: 100%;
    }

    /* Template Days */

    .exhibition,
    .party,
    .cocktail {
        font-weight: bold;
    }

    .exhibition {
        color: #ff9e00;
    }

    .party {
        color: #ff4081;
    }

    .cocktail {
        color: #00a1e8;
    }

</style>
</asp:Content>