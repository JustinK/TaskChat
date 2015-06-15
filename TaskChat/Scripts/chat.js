//initialize chat component
$(function () {

    setScreen(false);
    var chatHub = $.connection.chatHub;

    registerClientMethods(chatHub);

    $.connection.hub.start().done(function () {

        registerEvents(chatHub);

    });
});

//Initialize card update panel component
$(function () {
    var notificationHub = $.connection.notificationHub;

    notificationHub.client.receiveNotification = function (message) {
        $("#droppable").html(message);
        utilities.updateDisplay();
    };

    $("#droppable").on("change",function () {
        notificationHub.server.sendNotifications($("#droppable").html());
    });

    $("#divChat").on("click", function () {
        notificationHub.server.connect();
    });

    $.connection.hub.start();

   
});

//Switch to home screen when user logs in
function setScreen(isLogin) {

    if (!isLogin) {

        $("#divChat").hide();
        $("#divLogin").show();
    }
    else {

        $("#divChat").show();
        $("#divLogin").hide();
        $("#divChat").trigger('click');
    }

}
//Register event handlers for click and key press events
function registerEvents(chatHub) {

    $("#btnStartChat").click(function () {

        var name = $("#txtNickName").val();
        if (name.length > 0) {
            factory.refreshCards();
            chatHub.server.connect(name);
        }
        else {
            alert("Please enter name");
        }

    });


    $('#btnSendMsg').click(function () {

        var msg = $("#txtMessage").val();
        if (msg.length > 0) {

            var userName = $('#hdUserName').val();
            chatHub.server.sendMessageToAll(userName, msg);
            $("#txtMessage").val('');
        }
    });


    $("#txtNickName").keypress(function (e) {
        if (e.which === 13) {
            $("#btnStartChat").click();
        }
    });

    $("#txtMessage").keypress(function (e) {
        if (e.which === 13) {
            $('#btnSendMsg').click();
        }
    });


}

function registerClientMethods(chatHub) {

    //Initialize data when chat hub connection is made
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {

        setScreen(true);

        $('#hdId').val(id);
        $('#hdUserName').val(userName);
        $('#spanUser').html(userName);

        //Add all users to chat user list
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
        }

        //Populate chat window with existing messages
        for (i = 0; i < messages.length; i++) {

            AddMessage(messages[i].UserName, messages[i].Message);
        }


    }; //Add newly signed on user to chat user list
    chatHub.client.onNewUserConnected = function (id, name) {

        AddUser(chatHub, id, name);
    };

    //Remove user from chat user list when disconnected
    chatHub.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();

        var ctrId = 'private_' + id;
        $('#' + ctrId).remove();

        var disc = $('<div class="alert alert-warning">"' + userName + '" logged off.</div>');

        $(disc).hide();
        $('#divusers').prepend(disc);
        $(disc).fadeIn(200).delay(2000).fadeOut(200);

    };

    //Add new messages from chat hub to chat window
    chatHub.client.messageReceived = function (userName, message) {

        AddMessage(userName, message);
    };
}

//Add new user to chat user list
function AddUser(chatHub, id, name) {

    var code = $('<div id="' + id +'" class="loginUser">' + name + '</div>');

    $("#divusers").append(code);

}

//Add new messages in chat window
function AddMessage(userName, message) {
    $('#divChatWindow').append('<div class="message"><span class="userName">' + userName + '</span>: ' + message + '</div>');

    var height = $('#divChatWindow')[0].scrollHeight;
    $('#divChatWindow').scrollTop(height);
}
