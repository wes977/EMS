
$(document).ready(function ()
{
    $('tr>td>button.button-hidden').each(function () {
        $(this).click(function (event) {

            var id = $(this).parent().parent().attr('id');
            var userName = $("#" + id).children()[1].innerText;
            Remove(userName);
       })
    })

})


function Remove(name)
{
    $.ajax({
        type: "POST",
        url: "Users.aspx/DeleteUser",
        data: '{username: "' + name + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}
