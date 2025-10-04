$(document).ready(function () {
    $('.send').click(function () {
        const message = $('[name=Message]').val();
        const url = `${baseUrl}/api/NotificationNotes/SendMessageToAll`;
        const data = { message };
        $.post(url, data);
    });
});