$(document).ready(function () {
    $('.coffee-img').on('click', function () {
        var src = $(this).data('src');
        $('#modalImage').attr('src', src);
        $('#imageLink')
            .attr('href', src)
            .text(src);
    });
});