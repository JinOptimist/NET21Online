$(document).ready(function () {
    $('.send').click(function () {
        const message = $('[name=Message]').val();
        const url = `${baseUrl}/api/NotificationNotes/SendMessageToAll`;

        $.ajax({
            url: url,
            type: "POST",
            data: { message: message }, // отправляем как form-data
            success: function (response) {
                console.log("Уведомление отправлено:", response);
            },
            error: function (err) {
                console.error("Ошибка:", err);
            }
        });
    });
});