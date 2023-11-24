$(function () {
    let gameId = $('#RollDiceGameId').val();
    $.get('../GameTracker/GetRollData', { gameId }).done(function (view) {
        $('#DiceDataSection').html(view);
        $('#DiceDataSection').fadeIn('fast');
    });
});