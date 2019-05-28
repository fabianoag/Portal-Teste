

setTimeout(datapicker,300)

function datapicker() {
    $('.input-group.date').datepicker({
        format: "dd/mm/yyyy",
        language: "pt-BR",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: true,
        beforeShowMonth: function (date) {
            if (date.getMonth() == 8) {
                return false;
            }
        },
        toggleActive: true
    });
}