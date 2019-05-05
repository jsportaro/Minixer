"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/repositoryhub").build();



connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("ReceiveMessage", function (pullRequest) {
    var msg = pullRequest.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("messagesList").appendChild(li);
});