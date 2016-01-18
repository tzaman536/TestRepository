﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

       <div id="example">
        <div class="box wide">
            <div class="box-col">
                <h4>Options</h4>
                <ul class="options">
                    <li><input type="checkbox" id="text" checked="checked"/><label for="text">Show Text </label></li>
                    <li><input type="checkbox" id="checksum" /><label for="checksum">Show Checksum</label></li>
                </ul>
            </div>
            <div class="box-col">
                <h4>Types</h4>
                <ul class="options second-col">
                    <li><label for="type">Encoding:</label><input  id="type" /></li>
                    <li>
                        <label for="value">Value:</label><input  id="value" class="k-textbox" value="1234567"/>
                        <span id="validValue" class="k-widget k-tooltip k-tooltip-validation k-invalid-msg"></span>
                    </li>
                </ul>
            </div>
        </div>
        <div class="demo-section k-content">
            <%=Html.Kendo().Barcode()
                .Name("barcode")
                .Value("1234567")
                .Encoding(BarcodeSymbology.EAN8)
                .Background("transparent")
            %>
        </div>
        
        <script>
            function setOptions(e) {
                var validValue = $('#validValue');
                if (this.element && this.element[0].id == "type") {
                    $('#value').val(this.dataItem().value);
                }
                try {
                    var barcode = $('#barcode').data('kendoBarcode');
                    barcode.setOptions({
                        value: $('#value').val(),
                        checksum: $('#checksum').is(':checked'),
                        text: {
                            visible: $('#text').is(':checked')
                        },
                        type: $('#type').kendoDropDownList('value')
                    })
                    validValue.hide();
                } catch (e) {
                    validValue.text(e.message).show();
                }
            }

            $(document).ready(function () {

                $('#type').kendoDropDownList({
                    dataSource: [
                     { type: 'EAN8', value: "1234567" },
                     { type: 'EAN13', value: "123456789987" },
                     { type: 'UPCE', value: "123456" },
                     { type: 'UPCA', value: "12345678998" },
                     { type: 'Code11', value: "1234567" },
                     { type: 'Code39', value: "HELLO" },
                     { type: 'Code39Extended', value: "Hi!" },
                     { type: 'Code128', value: "Hello World!" },
                     { type: 'Code93', value: "HELLO" },
                     { type: 'Code93Extended', value: "Hello" },
                     { type: 'Code128A', value: "HELLO" },
                     { type: 'Code128B', value: "Hello" },
                     { type: 'Code128C', value: "1234567" },
                     { type: 'MSImod10', value: "1234567" },
                     { type: 'MSImod11', value: "1234567" },
                     { type: 'MSImod1010', value: "1234567" },
                     { type: 'MSImod1110', value: "1234567" },
                     { type: 'GS1-128', value: "12123456" },
                     { type: 'POSTNET', value: "12345" }
                    ],
                    change: setOptions,
                    dataTextField: "type",
                    dataValueField: "type"
                });

                $('#value,#checksum,#text').change(setOptions);
            });
        </script>
        <style type="text/css">
            #barcode {
                width: 300px;
                margin: 20px auto;
            }
        </style>
    </div>

</asp:Content>