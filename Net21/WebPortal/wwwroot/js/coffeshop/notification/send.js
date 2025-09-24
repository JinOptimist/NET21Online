$(document).ready(function () {
    $('.send').click(function () {
        const message = $('[name=messagenotification]').val().trim();

        $.post(`/CoffeShop/SendNotification/`, { message: message })
            .done(() => {
                console.log("Notification has been sent");
                $('[name=messagenotification]').val('');
            })
            .fail((xhr) => {
                console.error("Failed to send notification:", xhr.responseText);
                alert('Error: ' + xhr.responseText);
            });
    });
});