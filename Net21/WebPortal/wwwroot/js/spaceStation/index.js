$(document).ready(function () {
    const baseUrl = 'https://localhost:7210';
    const spaceNewsBaseUrl = 'https://localhost:7158';

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/hubs/spacenews")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.start().catch(err => console.error(err.toString()));

    connection.on("ReceiveNewsNotification", function (title, id) {
        console.log("Received notification:", title, id);
        location.reload();
    });

    initIssData();

    $(document).on('dblclick', '.news-image', function (e) {
        e.stopPropagation();

        // Create overlay with CSS class
        var overlay = $('<div>').addClass('image-overlay');

        var fullImage = $('<img>').attr('src', $(this).attr('src'));

        overlay.append(fullImage);
        $('body').append(overlay);

        // Close by clicking on overlay
        overlay.on('click', function () {
            $(this).remove();
        });

        // Close by pressing Esc
        $(document).on('keydown.imageViewer', function (e) {
            if (e.keyCode === 27) { // Esc key
                overlay.remove();
                $(document).off('keydown.imageViewer');
            }
        });
    });

    // News Removing selection
    //$(document).on('click', '.news-item', function (e) {
    //    // Skip selection if clicking on these elements
    //    if ($(e.target).is('.news-image, a, button, input, form, form *')) {
    //        return;
    //    }

    //    $(this).toggleClass('marked-to-remove');
    //});

    init();

    $('.view.mode').click(function () {
        const titleBlock = $(this)
            .closest('.news-text');

        const initialTitleName = titleBlock.find('h3').text();
        titleBlock.find('.edit').val(initialTitleName);

        titleBlock.find('.mode')
            .toggleClass('hidden');
    });
    $('.mode.edit').on('keyup', function (e) {
        if (e.keyCode === 13) { // Enter key
            const titleBlock = $(this)
                .closest('.news-text');

            const newTitleName = $(this).val();
            titleBlock.find('h3').text(newTitleName);

            const id = titleBlock.attr('data-id');
            const url = `${baseUrl}/SpaceStation/UpdateTitleName?id=${id}&title=${newTitleName}`;
            $.get(url);

            titleBlock.find('.mode')
                .toggleClass('hidden');
        }
    });

    // Removing by pressing Delete
    $(document).on('keyup', function (e) {
        if (e.keyCode === 46) { // Delete key
            $('.marked-to-remove').remove();
        }
    });

    function init() {
        const url = `${spaceNewsBaseUrl}/GetSpaceNews`;
        $.get(url)
            .done(function (spaceNews) {
                spaceNews.forEach(item => {
                    const SpaceNewsTag = $('.news.from-min-Api .news-item.template').clone();
                    SpaceNewsTag.removeClass('template');

                    SpaceNewsTag.find('.news-title').text(item.title);
                    SpaceNewsTag.find('.news-content').text(item.content);
                    SpaceNewsTag.find('.news-image').attr('src', decodeURIComponent(item.imageUrl));

                    $('.news.from-min-Api').append(SpaceNewsTag);
                });
            })
    }
    function initIssData() {
        $.get(`${baseUrl}/SpaceStation/GetIssPosition`)
            .done(function (data) {
                $('#iss-latitude').text(data.latitude.toFixed(4));
                $('#iss-longitude').text(data.longitude.toFixed(4));
                $('#iss-altitude').text(data.altitude.toFixed(2));
                $('#iss-velocity').text(Math.round(data.velocity));
            })
            .fail(function () {
                console.error('Ошибка загрузки данных МКС');
            });
    }

    $('#get-location').click(function () {
        getCurrentLocation();
    });

    function getCurrentLocation() {
        const statusElement = $('#location-status');
        statusElement.text('Определяем местоположение...').removeClass('error success');

        if (!navigator.geolocation) {
            statusElement.text('Геолокация не поддерживается вашим браузером').addClass('error');
            return;
        }

        navigator.geolocation.getCurrentPosition(
            function (position) {
                const latitude = position.coords.latitude;
                const longitude = position.coords.longitude;

                statusElement.text(`Местоположение определено!`).addClass('success');

                // Автоматически рассчитываем расстояние
                calculateDistanceToIss(latitude, longitude);
            },
            function (error) {
                let errorMessage = 'Ошибка определения местоположения: ';
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        errorMessage += 'Доступ к геолокации запрещен';
                        break;
                    case error.POSITION_UNAVAILABLE:
                        errorMessage += 'Информация о местоположении недоступна';
                        break;
                    case error.TIMEOUT:
                        errorMessage += 'Время ожидания истекло';
                        break;
                    default:
                        errorMessage += 'Неизвестная ошибка';
                        break;
                }
                statusElement.text(errorMessage).addClass('error');
            },
            {
                enableHighAccuracy: true,
                timeout: 10000,
                maximumAge: 60000
            }
        );
    }

    function calculateDistanceToIss(latitude, longitude) {
        $.get(`${baseUrl}/SpaceStation/GetIssDistance`, {
            latitude: latitude,
            longitude: longitude
        })
            .done(function (data) {
                $('#distance-result').text(data.distanceKilometers.toFixed(2));
            })
            .fail(function () {
                $('#location-status').text('Ошибка расчета расстояния').addClass('error');
            });
    }

    setInterval(initIssData, 30000);
});
