"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameUpdateHub").build();

connection.on("ReceiveRollsUpdates", function (receivedGameId) {
    let gameId = $('#RollDiceGameId').val();
    console.log('received signalR message');
    if (gameId == receivedGameId) {
        $('#DiceDataSection').fadeOut('fast', function () {
            $.get('../GameTracker/GetRollData', { gameId }).done(function (view) {
                $('#DiceDataSection').html(view);
                $('#DiceDataSection').fadeIn('fast');
            });
        });
    }
});

connection.on('ReceiveUpdatedPoints', function (pointValues, receivedGameId) {
    let gameId = $('#RollDiceGameId').val();
    console.log('received signalR message');
    if (gameId == receivedGameId) {
        $(pointValues).each(function () {
            $('#playerData_' + this.id).val(this.points);
        });
    }
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});