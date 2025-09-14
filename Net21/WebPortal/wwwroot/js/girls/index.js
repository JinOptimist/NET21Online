$(document).ready(function () {

    $(".girl").click(function () {
        const currentGirl = $(this);
        currentGirl.toggleClass('marked-to-remove');
    });

    $(document).on("keyup", function (event) {
        // keyCode == 46 for delete key
        if (event.keyCode == 46) {
            $(".girl.marked-to-remove").remove();
        }
    })

});