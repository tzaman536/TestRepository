<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div class="demo-section k-content">
        <h4>Select movie</h4>        
        <%= Html.Kendo().DropDownList()
                .Name("movies")
                .DataTextField("Text")
                .DataValueField("Value")
                .HtmlAttributes(new { style = "width: 100%" })
                .BindTo(new List<SelectListItem>()
                {
                    new SelectListItem() {
                    Text = "The Shawshank Redemption", Value ="1"
                    },
                    new SelectListItem() {
                    Text = "The Godfather", Value ="2"
                    },
                    new SelectListItem() {
                    Text = "The Godfather: Part II", Value ="3"
                    },
                    new SelectListItem() {
                    Text = "Il buono, il brutto, il cattivo.", Value ="4"
                    },
                    new SelectListItem() {
                    Text = "Pulp Fiction", Value ="5"
                    },
                    new SelectListItem() {
                    Text = "12 Angry Men", Value ="6"
                    },
                    new SelectListItem() {
                    Text = "Schindler's List", Value ="7"
                    },
                    new SelectListItem() {
                    Text = "One Flew Over the Cuckoo's Nest", Value ="8"
                    },
                    new SelectListItem() {
                    Text = "Inception", Value ="9"
                    },
                    new SelectListItem() {
                    Text = "The Dark Knight", Value ="10"
                    }
                })
        %>
 </div>          
            
<div class="box wide">
    <div class="box-col">
    <h4>API Functions</h4>
    <ul class="options">
        <li>
            <button id="Button1" class="k-button">Enable</button> <button id="Button2" class="k-button">Disable</button>
        </li>
        <li>
            <button id="Button3" class="k-button">Readonly</button>
        </li>
        <li>
            <button id="Button4" class="k-button">Open</button> <button id="Button5" class="k-button">Close</button>
        </li>
        <li>
            <button id="Button6" class="k-button">Get value</button> <button id="Button7" class="k-button">Get text</button>
        </li>
    </ul>
    </div>
    <div class="box-col">
    <h4>Selection</h4>
    <ul class="options">
        <li>
            <input id="Text1" value="0" class="k-textbox" style="width: 40px; margin: 0;" /> <button id="Button8" class="k-button">Select by index</button>
        </li>
        <li>
            <input id="Text2" value="1" class="k-textbox" style="width: 40px; margin: 0;" /> <button id="Button9" class="k-button">Select by value</button>
        </li>
        <li>
            <input id="Text3" value="Pulp" class="k-textbox" style="width: 100px; margin: 0;" /> <button id="Button10" class="k-button">Select item starting with</button>
        </li>
    </ul>
    </div>
</div>
    <script>
        $(document).ready(function() {
            $("#movies").closest(".k-widget")
                        .attr("id", "products_wrapper");

            var dropdownlist = $("#movies").data("kendoDropDownList"),
                setValue = function(e) {
                    if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode)
                        dropdownlist.value($("#value").val());
                },
                setIndex = function(e) {
                    if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode) {
                        var index = parseInt($("#index").val());
                        dropdownlist.select(index);
                    }
                },
                setSearch = function(e) {
                    if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode)
                        dropdownlist.search($("#word").val());
                };

            $("#enable").click(function() {
                dropdownlist.enable();
            });

            $("#disable").click(function() {
                dropdownlist.enable(false);
            });

            $("#readonly").click(function () {
                dropdownlist.readonly();
            });

            $("#open").click(function() {
                dropdownlist.open();
            });

            $("#close").click(function() {
                dropdownlist.close();
            });

            $("#getValue").click(function() {
                alert(dropdownlist.value());
            });

            $("#getText").click(function() {
                alert(dropdownlist.text());
            });

            $("#setValue").click(setValue);
            $("#value").keypress(setValue);

            $("#select").click(setIndex);
            $("#index").keypress(setIndex);

            $("#find").click(setSearch);
            $("#word").keypress(setSearch);
        });
    </script>
     <style>
        .configuration .k-textbox {
            width: 40px;
        }
        .k-button {
            min-width: 80px;
        }
    </style>
</asp:Content>