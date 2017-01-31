$(function () {
    $("#dateTimeDOB").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years',

    });

    $("#dateTimeDOB input").click(function () {
        $("#dateTimeDOB").data("DateTimePicker").show();
    });

    $("#ftDateTimeTerm").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years'
    });

    $("#ftDateTimeHire").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years'
    });

    $("#ptDateTimeHire").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years'
    });

    $("#ptDateTimeTerm").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years'
    });

    $("#seasonYearDateTime").datetimepicker({
        ignoreReadonly: true,
        format: 'YYYY',
        viewMode: 'years'
    });

    $("#conStartDateTime").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years'
    });

    $("#conEndDateTime").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'years'
    });




    $("#seasonYearDateTime input").click(function () {
        $("#seasonYearDateTime").data("DateTimePicker").show();
    });
});

