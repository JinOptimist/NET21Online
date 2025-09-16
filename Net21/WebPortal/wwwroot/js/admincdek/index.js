$(document).ready(function () {
    // При клике на строку заявки выделяем её
    $(".callrequest").click(function () {
        const currentRow = $(this);
        currentRow.toggleClass('marked-to-remove');
    });

    // При нажатии Delete удаляем выделенные строки
    $(document).on("keyup", function (event) {
        if (event.keyCode == 46 || event.keyCode == 8) { // 46 = Delete, 8 = Backspace
            $(".callrequest.marked-to-remove").remove();
        }
    });
});