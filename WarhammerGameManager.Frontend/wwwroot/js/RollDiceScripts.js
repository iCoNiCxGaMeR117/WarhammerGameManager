$(function () {
    $.get('../RollDice/GenerateRollDiceForm').done(function (view) {
        $('#RollDiceForms').html(view);
        $('#RollDiceForms').fadeIn('fast');
        $('#RollDiceResultsDiv').html($('#LoadingIconSection').html());
    });
});

$(document).on('submit', '#RollDiceRequestForm', function (event) {
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

$(document).on('submit', '#RollDiceBasicRequestForm',function (event) {
    event.preventDefault();


    let formAction = $('#RollDiceBasicRequestForm').attr('action');

    if ($('#RollDiceGameId').length) {
        $('#RollDiceBasicRequestForm').append($('#RollDiceGameId'))
    }

    let formData = $('#RollDiceBasicRequestForm').serialize();

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

$(document).on('hidden.bs.modal', '#RollDiceModal', function () {
    $('#RollDiceResultsDiv').html($('#LoadingIconSection').html());
    $('#RollDiceResultsDiv').hide();
    $('#RollDiceRequestDiv').show();
    $('#RollDiceRequestForm')[0].reset();
    $('#RollDiceBasicRequestForm')[0].reset();
});