
/*
    * Filename: companyBtnHandler.js
    *
    * Description:
    * Script to hold company button handlers
    *
    * Authors:
    * Kyle Marshall
    * Kyle Kreutzer
    *  Wes Thompson
    * Colin Mills
    *
    * Date: 2016-04-21
    
*/

// AssignBtnHandlers 
// 
// Assigns the button handlers to all buttons within
// the table.
//
// Parameters: none

$(document).ready(function () {
    $('tr>td>button.button-hidden').each(function () {
        $(this).click(function (event) {

            var id = $(this).parent().parent().attr('id');
            var companyName = $("#" + id).children()[1].innerText;
            RemoveCompany(companyName);          
        })
    })

})


// RemoveCompany(companyName)
// 
// Removes a company based on a company name.
//
// Parameters: 
// companyName: The company name.
//
function RemoveCompany(companyName) {
    $.ajax({
        type: "POST",
        url: "Companies.aspx/DeleteCompany",
        data: '{companyString: "' + companyName + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
            {
                $("#errorLabel").text(data.d);
            }
        }
    });


}
