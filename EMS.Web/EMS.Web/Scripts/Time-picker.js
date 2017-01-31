$(function () {




    $("#TimeCardStart").datetimepicker({
        ignoreReadonly: true,
        format: 'LT',

    });

    $("#TimeCardStart").on("dp.change", function (e) {
        $('#TimeCardEnd').data("DateTimePicker").minDate(e.date); //Wed Apr 20 2016 23:40:35
        var EndTime = e.date;
        EndTime.hour(23);
        EndTime.minute(59)
        $('#TimeCardEnd').data("DateTimePicker").maxDate(EndTime);
    });

    $("#TimeCardStart").on("dp.hide", function (e) {
        var StartTime = $("#TimeCardStart").data("DateTimePicker").date();
        var EndTime = $("#TimeCardEnd").data("DateTimePicker").date();

        var TimeWorkedHours = 0;
        var TimeWorkedMinutes = 0;
        var HoursWorkString = "Time Worked";
        if (StartTime != '' && EndTime != '') {
            var StartTimeHour = StartTime.hour();
            var EndTimeHour = EndTime.hour();
            var StartTimeMin = StartTime.minute();
            var EndTimeMin = EndTime.minute();
            if (EndTimeMin >= StartTimeMin)     // This is for if the times are 1:33  and 2:15 to make sure that it take an extra hour away to make it zero
            {
                TimeWorkedHours = EndTimeHour - StartTimeHour;
                TimeWorkedMinutes = EndTimeMin - StartTimeMin;
            }
            else {
                TimeWorkedHours = EndTimeHour - StartTimeHour - 1;
                TimeWorkedMinutes = (60 - StartTimeMin) + EndTimeMin;
            }
            HoursWorkString = HoursWorkString + " " + TimeWorkedHours.toString() + ":" + TimeWorkedMinutes.toString() + "";
            document.getElementById('HoursWorked').innerHTML = HoursWorkString;
        }

    });

    $("#TimeCardEnd").datetimepicker({
        ignoreReadonly: true,
        format: 'LT',


    });

    $("#TimeCardEnd").on("dp.change", function (e) {
        $('#TimeCardStart').data("DateTimePicker").maxDate(e.date);
        var StartTime = e.date;
        StartTime.hour(0);
        StartTime.minute(0)
        $('#TimeCardStart').data("DateTimePicker").minDate(StartTime);
    });

    $("#TimeCardEnd").on("dp.hide", function (e) {
        var StartTime = $("#TimeCardStart").data("DateTimePicker").date();
        var EndTime = $("#TimeCardEnd").data("DateTimePicker").date();

        var TimeWorkedHours = 0;
        var TimeWorkedMinutes = 0;
        var HoursWorkString = "Time Worked";
        if (StartTime != null && EndTime != null) {
            var StartTimeHour = StartTime.hour();
            var EndTimeHour = EndTime.hour();
            var StartTimeMin = StartTime.minute();
            var EndTimeMin = EndTime.minute();
            if (EndTimeMin >= StartTimeMin)     // This is for if the times are 1:33  and 2:15 to make sure that it take an extra hour away to make it zero
            {
                TimeWorkedHours = EndTimeHour - StartTimeHour;
                TimeWorkedMinutes = EndTimeMin - StartTimeMin;
            }
            else {
                TimeWorkedHours = EndTimeHour - StartTimeHour - 1;
                TimeWorkedMinutes = (60 - StartTimeMin) + EndTimeMin;
            }
        }
        HoursWorkString = HoursWorkString + " Hours " + TimeWorkedHours.toString() + " Minutes" + TimeWorkedMinutes.toString() + "";
        document.getElementById('HoursWorked').innerHTML = HoursWorkString;
    });

    moment.locale('en', {
        week: {
            dow: 1
        } // Monday is the first day of the week
    });

    $("#TimeCardDWeek234").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'days',
        calendarWeeks: true,
        useCurrent: true,
        defaultDate: new Date()
    });
    $("#TimeCardDate").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'days',
        calendarWeeks: true,
    });
    $("#TimeCardDWeek").datetimepicker({
        ignoreReadonly: true,
        format: 'MM/DD/YYYY',
        viewMode: 'days',
        calendarWeeks: true,


    });
    $("#TimeCardDWeek").on("dp.hide", function (e) {

        var DaySelected = e.date;
        var Day = DaySelected.day();
        var StartDay = e.date;
        var EndDay = e.date;
        $('#TimeCardDate').data("DateTimePicker").minDate(false);
        $('#TimeCardDate').data("DateTimePicker").maxDate(false);

        StartDay.date(StartDay.date() + 2);
        if (Day != 0) {
            StartDay.date(DaySelected.date()-(Day+1));
            $('#TimeCardDate').data("DateTimePicker").minDate(StartDay);
            EndDay.date(DaySelected.date() +6);
            $('#TimeCardDate').data("DateTimePicker").maxDate(EndDay);
        }
        else 
        {
            EndDay.date(DaySelected.date()-2);
            $('#TimeCardDate').data("DateTimePicker").maxDate(EndDay);
            StartDay.date(DaySelected.date()  -8);
            $('#TimeCardDate').data("DateTimePicker").minDate(StartDay);
        }

        $("#TimeCardDWeek2").datetimepicker({
            ignoreReadonly: true,
            format: 'MM/DD/YYYY',
            viewMode: 'days',
            calendarWeeks: true,


        });


    });

});
