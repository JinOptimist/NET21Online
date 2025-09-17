$(document).ready(function(){
    const liveTimeForNotificatoin = 5 * 1000;
    const url = `${baseUrl}/hubs/notifaction`
    const hub = new signalR.HubConnectionBuilder().withUrl(url).build();

    // When server send message for us
    hub.on("NewNotification", function(id, message){
        const notifactionTag = $('.notification.template').clone();
        notifactionTag.removeClass('template');
        notifactionTag.text(message);
        notifactionTag.click(onNotificationClick);
        notifactionTag.attr('data-id', id);

        $('.notification-container').append(notifactionTag);

        setTimeout(() => {
            removeNotification(notifactionTag);
        }, liveTimeForNotificatoin);

        console.log(message);
    });

    function onNotificationClick(){
        removeNotification($(this));
    }

    function removeNotification(notificationTag){
        notificationTag.remove();
        const notificationId = notificationTag.attr('data-id');
        const url = `${baseUrl}/api/notification/viewedByMe?notificationId=${notificationId}`
        $.get(url);
    }

    // open web socket to server
    hub.start();

    $('footer').click(function(){
        // when we send message to server
        hub.invoke("NotifyAll", "I click to footer");
    });
});