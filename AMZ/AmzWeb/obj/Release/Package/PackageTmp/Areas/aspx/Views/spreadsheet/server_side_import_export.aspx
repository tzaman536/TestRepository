<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script src="//cdnjs.cloudflare.com/ajax/libs/jszip/2.4.0/jszip.min.js"></script>


<div class="box wide">
    <div class="box-col">
        <h4>Export</h4>
        <form action="<%=Url.Action("Download", "Spreadsheet")%>" method="POST">
            <input type="hidden" id="download-data" name="data" />
            <input type="hidden" id="download-extension" name="extension" />
            <ul>
                <li><input type="submit" class="k-button download" data-extension=".xlsx" value="Save as XLSX" /></li>
                <li><input type="submit" class="k-button download" data-extension=".csv" value="Save as CSV" /></li>
                <li><input type="submit" class="k-button download" data-extension=".txt" value="Save as Tab-delimited text" /></li>
                <li><input type="submit" class="k-button download" data-extension=".json" value="Save as JSON" /></li>
            </ul>
        </form>
    </div>
    <div class="box-col">
        <h4>Import</h4>
        <input type="file" name="file" id="upload" />
    </div>
</div>
<%: Html.Kendo().Spreadsheet()
    .Name("spreadsheet")
    .HtmlAttributes(new { style = "width:100%;" })
    .BindTo((IEnumerable<SpreadsheetSheet>)ViewBag.Sheets)
%>
   
<script>
    $(function () {            
        var spreadsheet = $("#spreadsheet").data("kendoSpreadsheet");

        var ALLOWED_EXTENSIONS = [".xlsx", ".csv", ".txt", ".json"];

        $("#upload").kendoUpload({
            async: {
                saveUrl: "<%=Url.Action("Upload", "Spreadsheet")%>"
            },
            multiple: false,
            localization: {
                "select": "Select file to import..."
            },
            select: function (e) {
                var extension = e.files[0].extension.toLowerCase();
                if (ALLOWED_EXTENSIONS.indexOf(extension) == -1) {
                    alert("Please, select a supported file format");
                    e.preventDefault();
                }
            },
            success: function (e) {
                // Load the converted document into the spreadsheet
                spreadsheet.fromJSON(e.response);
            }
        });

        $(".download").click(function () {
            $("#download-data").val(JSON.stringify(spreadsheet.toJSON()));
            $("#download-extension").val($(this).data("extension"));
        });
    });
</script>

    
<style>
    .download { width: 260px; }
</style>
</asp:Content>