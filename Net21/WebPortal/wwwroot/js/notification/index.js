$(document).ready(function () {
    $('.send').click(function () {
        const message = $('[name=message]').val();
        const url = `${baseUrl}/api/Notification/SendMessageToAll`;
        const data = { message };
        $.post(url, data);
    });
});