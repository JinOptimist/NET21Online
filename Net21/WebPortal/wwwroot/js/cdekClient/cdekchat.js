$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl(`${baseUrl}/cdekchat`)
        .build()

    hub.on("ReceiveMessage", function (user, message) {
        const msg = $("<div>").text(user + ": " + message);
        $(".chat-messages").append(msg);
    });

    hub.start();

    $(".chat-send").click(function () {
        const message = $(".chat-input").val();
        hub.invoke("SendMessage", "Client", message);
        $(".chat-input").val("");
    });
});