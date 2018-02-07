(function () {

    $.connection.hub.start()
        .done(function () {

            console.log("it worked");
            var chatR = $.connection.chatRoom;
            $.connection.hub.logging = true;

            $('#btnConnect').on("click", function () {
                //acknowledge your arrival
                var result = prompt("Please enter your name to chat.");
                if (result != null) {
                    $('#myconnection').append("You are connected as " + result);
                    //paction
                    chatR.server.publish("¬¬NEW_CONNECTION¬¬¦" + result);
                    $('#btnSendMessage').on("click", function () {
                        chatR.server.publish($('#txtMessage').val());
                    });

                    return;
                }                
            }); 

        })
        .fail(function (err) { DisplayMessage("Error:" + err); });

    $.connection.chatRoom.client.displayWelcomeMessage = function (message) {
        $('#status').append("<br/>" + message);

       
        //$.connection.chatRoom.server.getServerDateTime()
        //    .done(function (data) {
        //        DisplayMessage("<br/>Connected at: " + data)
        //    })
        //    .fail(function (err) {
        //        DisplayMessage("Error:" + err);
        //    });
    }

    $.connection.chatRoom.client.displayClientMessage = function (message) {
        DisplayMessage(message);
    }

    var DisplayMessage = function (message) {
        $('#servermessages').append(message);
    };

})();