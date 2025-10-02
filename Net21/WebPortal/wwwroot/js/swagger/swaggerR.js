$(document).ready(function () {
    $('.method-header').on('click', function () {
        var $header = $(this);
        var $details = $header.next('.method-details');

        // Переключаем текущий
        if ($header.hasClass('active')) {
            $header.removeClass('active');
            $details.slideUp();
        } else {
            $header.addClass('active');
            $details.slideDown();
        }
    });
});