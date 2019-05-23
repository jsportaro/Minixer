"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/repositoryhub").build();
var viewerConnection = new signalR.HubConnectionBuilder().withUrl("/viewerhub").withHubProtocol(new MessagePackHubProtocol()).build();

//var protocol = new signalR.protocols.msgpack.MessagePackHubProtocol();
//var viewerConnection = new signalR.HubConnection(
//    "/viewerhub",
//    {
//        transport: signalR.TransportType.WebSocket,
//        protocol: protocol
//    }
//);

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

viewerConnection.start().then(function () {
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

function setupCanvas(canvas) {
    // Get the device pixel ratio, falling back to 1.
    var dpr = window.devicePixelRatio || 1;
    // Get the size of the canvas in CSS pixels.
    var rect = canvas.getBoundingClientRect();
    // Give the canvas pixel dimensions of their CSS
    // size * the device pixel ratio.
    canvas.width = rect.width * dpr;
    canvas.height = rect.height * dpr;
    var ctx = canvas.getContext('2d');
    // Scale all drawing operations by the dpr, so you
    // don't have to worry about the difference.
    ctx.scale(dpr, dpr);
    return ctx;
}
var updates = 0;
var viewer = {
    connect: function (cid) {
        //viewerConnection.invoke('OpenVirtualMachine',  id ).then(function () {
        //    console.log('Invocation of NewContosoChatMessage succeeded');

        //    viewerConnection.on("IncomingPixels", function (x, y, h, w, pixels) {
        //        console.log('Invoming Pixels x: ' + x + ' y: ' + y + ' h: ' + h + ' w: ' + w);
        //    });
        //}).catch(function (error) {
        //    console.log('Invocation of NewContosoChatMessage failed. Error: ' + error);
        //});
        var c = document.getElementById("viewer");
        var ctx = setupCanvas(c);// c.getContext("2d");

        viewerConnection.stream("OpenVirtualMachineChannel", cid)
            .subscribe({
                next: (item) => {
                    let desktopWidth = 720,
                        recWidth = item.rectangle.width,
                        recHeight = item.rectangle.height,
                        recX = item.rectangle.x,
                        recY = item.rectangle.y,
                        p = recY * desktopWidth + recX;

                    //var id = ctx.createImageData(recWidth, recHeight);
                    //var d = id.data; 
                    var d = [1, 2, 3, 4];
                    let offset = 720 - recWidth,
                        row = 0,
                        totalWrites = 0;
                    var pixelData = item.pixelData;
                    for (var y = 0, ty = recY; y < recHeight; ++y, ++ty) {
                        row = y * recWidth;
                        for (var x = 0, tx = recX; x < recWidth; ++x, ++tx) {
                            var pixel = pixelData[row + x];
                            d[totalWrites + 0] = pixel.red;
                            d[totalWrites + 1] = pixel.green;
                            d[totalWrites + 2] = pixel.blue;
                            d[totalWrites + 3] = 255;
                            totalWrites = totalWrites + 4;
                        }
                        p += offset;
                    }
                    //ctx.putImageData(id, recX, recY);
                    console.log("Update #" + updates + " Total writes: " + totalWrites);

                    updates++;
                },
                complete: () => {
                    console.log("done");
                },
                error: (err) => {
                    console.log("shit: " + err);
                }
            });
    }
};
