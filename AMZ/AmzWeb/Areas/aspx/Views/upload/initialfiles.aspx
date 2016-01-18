<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/aspx/Views/Shared/Web.Master" 
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Kendo.Mvc.Examples.Models.UploadInitialFile>>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="box">
        <h4>Information</h4>
        <p>
            This example show how to persist the successfully uploaded files
            in the list and display them again when the page is reloaded. 
            Please upload some files and refresh the page.
        </p>
    </div>

    <div class="demo-section k-content">
        <%= Html.Kendo().Upload()
              .Name("files")
              .Async(a => a
                  .Save("SaveAndPersist", "Upload")
                  .Remove("RemoveAndPersist", "Upload")
                  .AutoUpload(true)
              )
              .Files(files =>
              {
                  foreach (var f in Model)
                  {
                      files.Add().Name(f.Name).Extension(f.Extension).Size(f.Size);
                  }
              })
         %>
    </div>
    
</asp:Content>
