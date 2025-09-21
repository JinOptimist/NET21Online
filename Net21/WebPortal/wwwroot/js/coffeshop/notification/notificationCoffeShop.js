$(document).ready(function(){
    const url = `${baseUrl}/hubs/notifaction/CoffeShop`
    const hub = new signalR.HubConnectionBuilder().withUrl(url).build();

    hub.on("NewNotificationCoffeShop",function(message){
         console.log(message)
    })

    hub.start();

    $('#cart-icons').click(function(){
        hub.invoke("NotifyAllCoffeShop","Someone wants to make a purchase");
    })
})