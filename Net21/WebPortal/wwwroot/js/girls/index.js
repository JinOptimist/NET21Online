$(document).ready(function () {
    

    // $(".girl").click(function () {
    //     const currentGirl = $(this);
    //     currentGirl.toggleClass('marked-to-remove');
    // });

    init();

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

    function init() {
        const url = `${idolBaseUrl}/GetIdols`;
        $.get(url)
            .done(function (idols) {
                idols.forEach(idol => {
                    const girlTag = $('.from-minimal-api .girl.template').clone();

                    girlTag.removeClass('template');
                    girlTag.find('.name').text(idol.name);
                    
                    girlTag.find('.girl-image').attr('src', decodeURIComponent(idol.url));

                    $('.girls.from-minimal-api').append(girlTag);
                });
            });
    }

});