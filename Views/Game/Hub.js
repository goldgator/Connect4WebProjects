$(function () {
    //Declare proxy
    var lobby = $.connection.connectFourHub;

    //Create a function that the hub can call to broacast messages
    lobby.client.broadcastMessage = function (colString, pieceValue) {
        let text = "Game/UpdateBoard?column=" + colString + "&&pieceKey=" + pieceValue + "&&connectionID=" + $("#connectionIDHolder").val();
        $.get(text, {}, function (response) {
            $("#grid").html(response);
        });
    }

    lobby.client.getWin = function () {
        let text = "Game/WinBoard";

        $.get(text, {}, function (response) {
            $("#messageBox").html(response);
        });
    }

    lobby.client.getLose = function (winnerName) {
        let text = "Game/LoseBoard?winnerName=" + winnerName;
        $.get(text, {}, function (response) {
            $("#messageBox").html(response);
        });
    }

    lobby.client.setData = function (pieceValue, connectionID) {
        $("#pieceValueHolder").val(pieceValue);
        $("#connectionIDHolder").val(connectionID);
    }

    $.connection.hub.start().done(function () {
        $("#enterCol").click(function () {
            lobby.server.sendColGroup($("#txtColInput").val(), $("#pieceValueHolder").val());
        });

        $('#sendmessage').click(function () {
            // Call the Send method on the hub. 
            lobby.server.sendChatMessage($('#displayname').val(), $('#message').val());
            // Clear text box and reset focus for next comment. 
            $('#message').val('').focus();
        });
    });

    // Declare a proxy 
    //var chat = $.connection.connectFourHub;
    // Create a function so the hub can broadcast messages.
    lobby.client.broadcastChatMessage = function (name, message) {
        // Html encode display name and message. 
        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page. 
        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };
    // Get the user name and store it to prepend to messages.
    $('#displayname').val(prompt('Enter your name:', ''));
    // Set initial focus to message input box.  
    $('#message').focus();
});

    //$(function () {
    //    $("#enterCol").click(function () {
    //        let text = "UpdateBoard?column=" + $("#txtColInput").val();
    //        $.get(text, {}, function (response) {
    //            $("#grid").html(response);
    //        });
    //    });
    //});