// Write your JavaScript code.
$(document).ready(function () {
    $("select").change(function () {

        var data = {
            'bandAColor': $('#drpFirstBand').val(),
            'bandBColor': $('#drpSecondBand').val(),
            'bandCColor': $('#drpThirdBand').val(),
            'bandDColor': $('#drpFourthBand').val()
        };

        // this ajax call returns the min and maximum resistance value
        $.ajax({
            type: "POST",
            url: "/Home/ResistanceValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (result) {
                //if error
                if (result.error != undefined) {
                    $("#divError").html(result.error);
                }
                else {
                    $("#divError").empty();
                    $("#txtMinResistance").val(result.min);
                    $("#txtMaxResistance").val(result.max);
                }
            }
        });
    });

})
