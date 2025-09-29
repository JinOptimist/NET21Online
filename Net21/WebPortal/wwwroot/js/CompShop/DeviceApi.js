$(document).ready(function () {

    const url = `${deviceBaseUrl}/GetDevice`;
    $.get(url)
        .done(function (devices)
        {
            devices.forEach(device =>
            {
                const deviceTag = $('.from-minimal-api .comp.template').clone();

                deviceTag.removeClass('template');
               
                deviceTag.find('.name').text(device.name);
                deviceTag.find('.type').text(device.categoryName);
                deviceTag.find('.cost').find('.spanCost').text(device.price + " Byn");
                deviceTag.find('.btn.delete').attr('href', '/compshop/delete?id=_ID_'.replace('_ID_', device.id));

                deviceTag.find('.imgDevice').attr('src', decodeURIComponent(device.urlImage));

                $('.from-minimal-api').append(deviceTag);
            });
        });


    $(".selecter").click(function ()
    {
        const filters_sidebar = $('.filters-sidebar');
        filters_sidebar.toggleClass('active');
    });

});