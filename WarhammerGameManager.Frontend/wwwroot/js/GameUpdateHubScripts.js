"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameUpdateHub").build();

//Disable the send button until connection is established.
document.getElementById("RollDiceButton").disabled = true;

connection.on("ReceiveUpdates", function (receivedGameId) {
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

connection.start().then(function () {
    document.getElementById("RollDiceButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});