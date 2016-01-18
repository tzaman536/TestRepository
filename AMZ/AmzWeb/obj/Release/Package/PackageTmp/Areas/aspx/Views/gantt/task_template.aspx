<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" %>

<%@ Import Namespace="Kendo.Mvc.Examples.Models.Gantt" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <script id="task-template" type="text/x-kendo-template">
        # if (resources[0]) { #
        <div class="template" style="background-color: #= resources[0].color #;">
            <img class="resource-img" src="../../content/web/gantt/resources/#:resources[0].id#.jpg" alt="#: resources[0].id #" />
            <div class="wrapper">
                <strong class="title">#= title # </strong>
                <span class="resource">#= resources[0].name #</span>
            </div>
            <div class="progress" style="width:#= (100 * parseFloat(percentComplete)) #%"> </div>
        </div>
        # } else { #
        <div class="template">
            <div class="wrapper">
                <strong class="title">#= title # </strong>
                <span class="resource">no resource assigned</span>
            </div>
            <div class="progress" style="width:#= (100 * parseFloat(percentComplete)) #%"> </div>
        </div>
        # } #
    </script>

    <%: Html.Kendo().Gantt<TaskViewModel, DependencyViewModel>()
        .Name("gantt")
        .Columns(columns =>
        {
            columns.Bound("title").Editable(true).Sortable(true);
            columns.Resources("resources").Editable(true).Title("Assigned Resources");
        })
        .Views(views =>
        {
            views.DayView();
            views.WeekView(weekView => weekView.Selected(true));
        })
        .Height(700)
        .TaskTemplateId("task-template")
        .RowHeight(62)
        .ShowWorkHours(false)
        .ShowWorkDays(false)
        .Snap(false)
        .DataSource(d => d
            .Model(m =>
            {
                m.Id(f => f.TaskID);
                m.ParentId(f => f.ParentID);
                m.OrderId(f => f.OrderId);
                m.Field(f => f.Expanded).DefaultValue(true);
            })
            .Read("ReadTasks", "Gantt")
            .Create("CreateTask", "Gantt")
            .Destroy("DestroyTask", "Gantt")
            .Update("UpdateTask", "Gantt")
        )
        .DependenciesDataSource(d => d
            .Model(m =>
            {
                m.Id(f => f.DependencyID);
                m.PredecessorId(f => f.PredecessorID);
                m.SuccessorId(f => f.SuccessorID);
                m.Type(f => f.Type);
            })
            .Read("ReadDependencies", "Gantt")
            .Create("CreateDependency", "Gantt")
            .Destroy("DestroyDependency", "Gantt")
            .Update("UpdateDependency", "Gantt")
        )
        .Resources(r => r
            .Field("resources")
            .DataColorField("Color")
            .DataTextField("Name")
            .DataSource(d => d
                .Custom()
                .Schema(s => s
                    .Model(m => m.Id("ID"))
                    .Data("Data")
                )
                .Transport(t =>
                {
                    t.Read("ReadResources", "Gantt");
                })
            )
        )
        .Assignments<ResourceAssignmentViewModel>(a => a
            .DataTaskIdField("TaskID")
            .DataResourceIdField("ResourceID")
            .DataValueField("Units")
            .DataSource(d => d
                .Model(m => 
                {
                    m.Id(f => f.ID);
                })
                .Read("ReadAssignments", "Gantt")
                .Create("CreateAssignment", "Gantt")
                .Destroy("DestroyAssignment", "Gantt")
                .Update("UpdateAssignment", "Gantt")
            )
        )
        %>

	<style type="text/css">

        /*center treelist cell content vertically*/
        .k-gantt .k-treelist td
        {
            vertical-align: middle;
        }

        /*hide the resource labels, as they are present in the task template*/
        .k-gantt .k-resource
        {
            display: none;
        }

        /*style the task template*/
        .k-task-template {
            height: 100%;
            padding: 0 !important;
        }

        .template {
            height: 100%;
            overflow: hidden;
        }

        .resource-img {
            float: left;
            width: 32px;
            height: 32px;
            border-radius: 50%;
            margin: 8px;
        }

        .wrapper {
            padding: 8px;
            color: #fff;
        }

        .k-task-template .wrapper > * {
            display: block;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .title {
            font-weight: bold;
            font-size: 13px;
        }

        .resource {
            text-transform: uppercase;
            font-size: 9px;
            margin-top: .5em;
        }

        .progress
        {
            position: absolute;
            left: 0;
            bottom: 0;
            width: 0%;
            height: 4px;
            background: rgba(0, 0, 0, .3);
        }
    </style>

</asp:Content>
