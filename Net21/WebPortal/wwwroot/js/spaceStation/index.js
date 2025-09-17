$(document).ready(function () {
    const baseUrl = 'https://localhost:7210';
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
});