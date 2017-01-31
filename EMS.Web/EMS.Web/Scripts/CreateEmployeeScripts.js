

$(document).ready(function () {
    $("#conEndDateTime").on("dp.change", function (e) {

        var $endDate = $("#conEndDateTime").data("DateTimePicker").date();

        if (!$endDate) {
            $("#main_reasonForLeaving").prop('disabled', 'disabled');
        }
        else {
            $("#main_reasonForLeaving").removeProp('disabled');
        }

    });

    $("#ftDateTimeTerm").on("dp.change", function (e) {

        var $endDate = $("#ftDateTimeTerm").data("DateTimePicker").date();

        if (!$endDate) {
            $("#main_reasonForLeaving").prop('disabled', 'disabled');
        }
        else {
            $("#main_reasonForLeaving").removeProp('disabled');
        }

    });

    $("#ptDateTimeTerm").on("dp.change", function (e) {

        var $endDate = $("#ptDateTimeTerm").data("DateTimePicker").date();

        if (!$endDate) {
            $("#main_reasonForLeaving").prop('disabled', 'disabled');
        }
        else {
            $("#main_reasonForLeaving").removeProp('disabled');
        }

    });

})