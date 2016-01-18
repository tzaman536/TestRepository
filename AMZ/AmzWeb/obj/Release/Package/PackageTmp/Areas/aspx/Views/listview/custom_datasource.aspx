﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master"
Inherits="System.Web.Mvc.ViewPage<IEnumerable<Kendo.Mvc.Examples.Models.ProductViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="demo-section k-content wide">
    <a class="k-button k-button-icontext k-add-button" href="#"><span class="k-icon k-add"></span>Add new record</a>
<%= Html.Kendo().ListView<Kendo.Mvc.Examples.Models.ProductViewModel>()
    .Name("listView")
    .TagName("div")
    .ClientTemplateId("template")
    .DataSource(dataSource => dataSource
        .Custom()
        .Schema(schema => schema
        .Model(model => model.Id("ProductID")))
        .PageSize(4)
        .Batch(true)
        .Transport(transport => transport
            .Create(create => create.Url("http://demos.telerik.com/kendo-ui/service/Products/Create")
                .DataType("jsonp"))
            .Read(read => read.Url("http://demos.telerik.com/kendo-ui/service/Products")
                .DataType("jsonp"))
            .Update(update => update.Url("http://demos.telerik.com/kendo-ui/service/Products/Update")
                .DataType("jsonp"))
            .Destroy(destroy => destroy.Url("http://demos.telerik.com/kendo-ui/service/Products/Destroy")
                .DataType("jsonp"))
            .ParameterMap("parameterMap")
        )
    )
    .Pageable()
    .Editable()
%>
</div>

<script type="text/x-kendo-tmpl" id="template">
    <div class="product-view k-widget">
        <dl>
            <dt>Product Name</dt>
            <dd>#:ProductName#</dd>
            <dt>Unit Price</dt>
            <dd>#:kendo.toString(UnitPrice, "c")#</dd>
            <dt>Units In Stock</dt>
            <dd>#:UnitsInStock#</dd>
            <dt>Discontinued</dt>
            <dd>#:Discontinued#</dd>
        </dl>
        <div class="edit-buttons">
            <a class="k-button k-edit-button" href="\\#"><span class="k-icon k-edit"></span></a>
            <a class="k-button k-delete-button" href="\\#"><span class="k-icon k-delete"></span></a>
        </div>
    </div>
</script>



<script>
    function parameterMap(options, operation) {
        if (operation !== "read" && options.models) {
            return { models: kendo.stringify(options.models) };
        }
    }

    $(function () {
        var listView = $("#listView").data("kendoListView");

        $(".k-add-button").click(function (e) {
            listView.add();
            e.preventDefault();
        });
    });
</script>

<style>
    .product-view
    {
        float: left;
        width: 50%;
        height: 300px;
        box-sizing: border-box;
        border-top: 0;
        position: relative;
    }
    .product-view:nth-child(even) {
        border-left-width: 0;
    }
    .product-view dl
    {
        margin: 10px 10px 0;
        padding: 0;
        overflow: hidden;
    }
    .product-view dt, dd
    {
        margin: 0;
        padding: 0;
        width: 100%;
        line-height: 24px;
        font-size: 18px;
    }
    .product-view dt
    {
        font-size: 11px;
        height: 16px;
        line-height: 16px;
        text-transform: uppercase;
        opacity: 0.5;
    }
    
    .product-view dd
    {
        height: 46px;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;

    }
    
    .product-view dd .k-widget,
    .product-view dd .k-textbox {
        font-size: 14px;
    }
    .k-listview
    {
        border-width: 1px 0 0;
        padding: 0;
        overflow: hidden;
        min-height: 298px;
    }
    .edit-buttons
    {
        position: absolute;
        bottom: 0;
        left: 0;
        right: 0;
        text-align: right;
        padding: 5px;
        background-color: rgba(0,0,0,0.1);
    }
    .k-pager-wrap
    {
        border-top: 0;
    }
    span.k-invalid-msg
    {
        position: absolute;
        margin-left: 6px;
    }
    
    .k-add-button {
        margin-bottom: 2em;
    }
    
    @media only screen and (max-width : 620px) {
    
        .product-view
        {
            width: 100%;
        }
        .product-view:nth-child(even) {
            border-left-width: 1px;
        }
    }
</style>
</asp:Content>