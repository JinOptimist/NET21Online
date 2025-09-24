$(document).ready(function () {

    const baseUrl = 'https://localhost:7210';
    $(".product-block").click(function () {

        const selectedProduct = $(this);
        selectedProduct.toggleClass('marked-to-remove');

        const anyProductSelected = $('.product-block.marked-to-remove')
        const showButtonForRemoving = $(".hidden-button");

        if (anyProductSelected.length > 0) {
            showButtonForRemoving.addClass("show-button")
        }
        else {
            showButtonForRemoving.removeClass("show-button");
        }

    });

    function removeSelectedElementsAndHideRemoveNutton() {

        const ids = $(".product-block.marked-to-remove").map(function () {
            return $(this).attr('data-id');
        }).get();

        const url = `${baseUrl}/Tourism/RemoveToursInShop`;

        $.post(url, { ids: ids });

        $(".product-block.marked-to-remove").remove();
        $(".hidden-button.show-button").removeClass("show-button");
    };

    $(".remove-action").click(function () {
        removeSelectedElementsAndHideRemoveNutton()
    })

    $(document).on("keyup", function (event) {
        // keyCode == 46 for delete key
        if (event.keycode == 46) {
            removeSelectedElementsAndHideRemoveNutton()
        }
    }
    );

})