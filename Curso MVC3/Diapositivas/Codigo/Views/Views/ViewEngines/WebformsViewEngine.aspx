<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Views.Models.Post>>" %>
<%// ReSharper disable Asp.NotResolved%>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>WebformsViewEngine</title>
</head>
<body>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table>
        <tr>
            <th></th>
            <th>
                Titulo
            </th>
            <th>
                Contenido
            </th>
        </tr>
    
    <% foreach (var post in Model) { %>
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id = post.Id })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id = post.Id })%>
            </td>
            <td>
                <%: post.Titulo %>
            </td>
            <td>
                <%: post.Contenido %>
            </td>
        </tr>  
    <% } %>
    
    </table>
</body>
</html>
<%:// ReSharper restore Asp.NotResolved%>
