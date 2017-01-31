
/*
    * Filename: userBtnHandler.js
    *
    * Description:
    * Script to hold user button handlers
    *
    * Authors:
    * Kyle Marshall
    * Kyle Kreutzer
    *  Wes Thompson
    * Colin Mills
    *
    * Date: 2016-04-21
    
*/


// assignBtnHandler
//
// Assigns button handlers to all buttons within the table.
//
// Grab and assign the button handlers.
//
// 
$(document).ready(function ()
{
    $('tr>td>button.button-hidden').each(function () {
        $(this).click(function (event) {

            var id = $(this).parent().parent().attr('id');
            var userName = $("#" + id).children()[1].innerText;
            RemoveUser(userName);
       })
    })
        
})


// RemoveUser
//
// Removes a user based on a name.
//
// Parameters:
// name: The name of the user that we are removing.
//
function RemoveUser(name)
{
    $.ajax({
        type: "POST",
        url: "Users.aspx/DeleteUser",
        data: '{username: "' + name + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data)
        {
            $("#errorLabel").text(data.d);
        }
    });
}
