$(document).ready(function () {
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
    $(document).on('click', '.news-item', function (e) {
        // Skip selection if clicking on these elements
        if ($(e.target).is('.news-image, a, button, input, form, form *')) {
            return;
        }

        $(this).toggleClass('marked-to-remove');
    });

    // Removing by pressing Delete
    $(document).on('keyup', function (e) {
        if (e.keyCode === 46) { // Delete key
            $('.marked-to-remove').remove();
        }
    });
});