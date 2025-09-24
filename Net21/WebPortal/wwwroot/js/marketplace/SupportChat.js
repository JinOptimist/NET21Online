function initializeSupportChat() {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/supportChatHub")
        .build();

    connection.on("ReceiveMessage", (user, message, timestamp) => {
        const messageTime = new Date(timestamp).toLocaleTimeString();
        const messageClass = user === "Поддержка" ? "support-message" : "user-message";

        $("#chatMessages").append(`
            <div class="message ${messageClass}">
                <div><strong>${user}:</strong> ${message}</div>
                <div class="message-time">${messageTime}</div>
            </div>
        `);
        $("#chatMessages").scrollTop($("#chatMessages")[0].scrollHeight);
    });

    connection.start().then(() => {
        $("#connectionStatus").html('<i class="fas fa-circle online"></i> Online');
    });

    $("#sendButton").click(() => {
        const message = $("#messageInput").val();
        if (message.trim() !== "") {
            connection.invoke("SendMessage", "Пользователь", message);
            $("#messageInput").val("");
        }
    });

    $("#messageInput").keypress((e) => {
        if (e.which === 13) $("#sendButton").click();
    });
}

$(document).ready(initializeSupportChat);