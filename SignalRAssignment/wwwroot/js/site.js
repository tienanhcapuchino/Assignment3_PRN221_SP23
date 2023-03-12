// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    LoadPostData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();

    connection.on("LoadPosts", function () {
        LoadPostData();
    })
    LoadPostData();

    function LoadPostData() {
        var tr = '';
        $.ajax({
            url: '/Posts/GetPost',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr +=
                        `<tr>
                    <td>${v.Title}</td>
                    <td>${v.Content}</td>
                    <td>${v.AppUsers.FullName}</td>
                    <td>${v.PostCategories.CategoryName}</td>
                    <td>${v.CreatedDate}</td>
                    <td>${v.UpdatedDate}</td>
                    <td>
                    <span class="${v.PublishStatus == 1 ? "text text-success" : "text text-danger"}">
                    ${v.PublishStatus == 1 ? "Published" : "Not published"}
                    </span>
                    </td>
                    <td>
                        <a class="btn btn-primary" href="/Posts/OnDelete/${v.PostID}">Delete</a>
                        <a class="btn btn-primary" href="/Posts/Update/${v.PostID}">Edit</a>
                    </td>
                </tr > `
                })
                $("#test").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
})