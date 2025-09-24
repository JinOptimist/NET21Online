$(document).ready(function () {
    const url = `${baseUrl}/hubs/notifaction/CoffeShop`;
    const hub = new signalR.HubConnectionBuilder().withUrl(url).build();

    hub.on("NewNotificationCoffeShop", function (message) {
        const notificationTag = $(".messagenotification.templates").clone();
        notificationTag.removeClass("templates");   // убираем скрывающий класс
        notificationTag.text(message);

        $(".notificationcoffe-container").append(notificationTag);

        setTimeout(() => notificationTag.addClass("show"), 10);

        setTimeout(() => {
            hideNotification(notificationTag);
        }, 5000);

        notificationTag.click(() => hideNotification(notificationTag));
    });

    function hideNotification(el) {
        el.removeClass("show");
        setTimeout(() => el.remove(), 500); 
    }

    hub.start();

    $("#cart-icons").click(function () {
        hub.invoke("NotifyAllCoffeShop", "wants to make a purchase");
    });
});
