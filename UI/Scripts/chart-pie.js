// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';

jQuery.extend({
    getValues: function (url) {
        var result = null;
        $.ajax({
            url: url,
            type: 'get',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    }
});


// Pie Chart Dispositivos por Tipo
var ctx = document.getElementById("TipoDispositivo");
var dispData=$.getValues("/Home/TipoDispositivos")
var TipoDispositivo = new Chart(ctx, {
    type: 'doughnut',
    data: dispData, 
 

    options: {
        maintainAspectRatio: false,
        tooltips: {
            backgroundColor: "rgb(255,255,255)",
            bodyFontColor: "#858796",
            borderColor: '#dddfeb',
            borderWidth: 1,
            xPadding: 5,
            yPadding: 5,
            displayColors: true,
            caretPadding: 10,
        },
        legend: {
            display: false
        },
        cutoutPercentage: 70,
    },
});



