$(function () {
    $('#RollDiceResultsDiv').html($('#LoadingIconSection').html());
});

$('#RollDiceRequestForm').on('submit', function (event) {
    event.preventDefault();

    
    let formAction = $('#RollDiceRequestForm').attr('action');

    if ($('#RollDiceGameId').length) {
        $('#RollDiceRequestForm').append($('#RollDiceGameId'))
    }

    let formData = $('#RollDiceRequestForm').serialize();

    $('#RollDiceRequestDiv').fadeOut('fast', function () {
        $('#RollDiceResultsDiv').fadeIn('fast');
        $.post(formAction, formData).done(function (returnData) {
            $('#RollDiceResultsDiv').fadeOut('fast', function () {
                $('#RollDiceResultsDiv').html(returnData);
                $('#RollDiceResultsDiv').fadeIn('fast');
            });
        });
    });
});

$('#RollDiceModal').on('hidden.bs.modal', function () {
    $('#RollDiceResultsDiv').html($('#LoadingIconSection').html());
    $('#RollDiceResultsDiv').hide();
    $('#RollDiceRequestDiv').show();
    $('#RollDiceRequestForm')[0].reset();
});