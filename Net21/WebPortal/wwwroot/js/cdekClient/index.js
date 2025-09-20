// Чат Бокс
$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl(`${baseUrl}/cdekchat`)
        .build();

    hub.on("ReceiveMessage", function (id, user, message) {
        const msgDiv = $("<div>")
            .addClass("chat-message")
            .addClass(user === "Client" ? "you" : "other")
            .text(user + ": " + message);

        $(".chat-messages").append(msgDiv);

        // автопрокрутка вниз
        $(".chat-messages").scrollTop($(".chat-messages")[0].scrollHeight);
    });

    hub.start().catch(err => console.error(err.toString()));

    $(".chat-send").click(function () {
        const message = $(".chat-input").val();
        if (message.trim() === "") return;

        hub.invoke("SendMessage", "Client", message); // для клиента
        $(".chat-input").val("");
    });
});