$(document).ready(function () {
    const baseUrl = 'https://localhost:7210';

    // $(".girl").click(function () {
    //     const currentGirl = $(this);
    //     currentGirl.toggleClass('marked-to-remove');
    // });

    $('.view.mode').click(function () {
        const nameBlock = $(this)
            .closest('.name');

        const initialGirlName = nameBlock.find('.view').text();
        const input = nameBlock.find('.edit');
        input.val(initialGirlName);

        nameBlock.find('.mode')
            .toggleClass('hidden');
        input.focus();
    });

    $('.mode.edit').on("keyup", function (event) {
        // keyCode == 13 for Enter key
        if (event.keyCode == 13) {
            const nameBlock = $(this)
                .closest('.name');

            const newName = $(this).val();

            if (!newName) {
                nameBlock.find('.empty-name').removeClass('hidden')
                return;
            } else {
                nameBlock.find('.empty-name').addClass('hidden')
            }

            nameBlock.find('.view').text(newName);

            const id = $(this).closest('.girl').attr('data-id');
            const url = `${baseUrl}/Girl/UpdateName?id=${id}&name=${newName}`;
            $.get(url);

            nameBlock.find('.mode')
                .toggleClass('hidden');
        }
    })

    $(document).on("keyup", function (event) {
        // keyCode == 46 for delete key
        if (event.keyCode == 46) {
            $(".girl.marked-to-remove").remove();
        }
    });

});