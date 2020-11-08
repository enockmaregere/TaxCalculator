$(document).ready(function () {

    const SERVER_ADDRESS = "https://localhost:44365/api/";

    $.ajax({
        type: "GET",
        url: SERVER_ADDRESS + "tax/tax-calculation-types",
        success: function (data) {
            populatePostalCodesDropDown(data);            
        },
        error: function (error) {
            $('#errorMessage').text(error.responseJSON.Message);
        }
    }); 

    $("#submit").click(function () {
        
        let calculationType = $("#postalCodesDropdown option:selected").val();
        let postalCode = $("#postalCodesDropdown option:selected").text();
        let annualIncome = $("#income").val();

        if (!isAnnualIncomeValid(annualIncome)) {
            return;
        }

        $('#errorMessage').text('');

        $.ajax({
            type: "GET",
            url: SERVER_ADDRESS + `tax/tax-payable?calculationType=${calculationType}&postalCode=${postalCode}&annualIncome=${annualIncome}`,
            success: function (response) {
                $("#taxPayable").text(response);
            },
            error: function (error) {
                $('#errorMessage').text(error.responseJSON.Message);
            }
        });      
    });
});

function populatePostalCodesDropDown(data) {
    let s = '<option value="-1">Select Postal Code</option>';

    for (let i = 0; i < data.length; i++) {
        s += '<option value="' + data[i].calculationType + '">' + data[i].postalCode + '</option>';
    }

    $("#postalCodesDropdown").html(s);
}

function isAnnualIncomeValid(income) {
    let annualIncome = parseInt(income);
   
    if (annualIncome > 99999999999999) {
        $('#errorMessage').text('Income is too large');
        return false;
    }
    return true;
}
   

