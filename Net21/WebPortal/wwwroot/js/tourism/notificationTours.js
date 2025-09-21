$(document).ready(function () {
    const liveTimeForNotification = 5 * 1000;
    const url = `${baseUrl}/hubs/notification/tourism`;
    const hub = new signalR.HubConnectionBuilder().withUrl(url).build();

    hub.on("NewTourNotification", function (tourName, autorName) {

        const message = `New tour: "${tourName}" by ${autorName}`;
        const notifactionTag = $('.notification.template').clone();

        notifactionTag.removeClass('template');
        notifactionTag.text(message);
        notifactionTag.click(onNotificationClick);


        $('.notification-container').append(notifactionTag);

        setTimeout(() => {
            removeNotification(notifactionTag);
        }, liveTimeForNotification);

        console.log(message);
    });

    function onNotificationClick() {
        removeNotification($(this));
    }

    function removeNotification(notificationTag) {
        notificationTag.remove();
    }

    hub.start();

    $('.notify-button').click(function () {
        const notifyAboutSelectedTour = $(this).closest(".product-block");
        const autorName = notifyAboutSelectedTour.attr('data-author');
        const tourName = notifyAboutSelectedTour.attr('data-tour');

        hub.invoke("NotifyAllAboutTourCreation", tourName, autorName)
            .then(() => console.log(`Notification send "${tourName}" by ${autorName}`))
            .catch(err => console.error("Error:", err));
    });
});