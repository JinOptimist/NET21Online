$(document).ready(function () {

    const baseUrl = 'https://localhost:7210';
    const selectedProduct = $(this).closest(".product-block");

    $(".title-view.mode").click(function () {

        const selectedProduct = $(this).closest(".product-block");

        const initialTourname = selectedProduct.find(".title-view").text();
        const input = selectedProduct.find(".edit");

        input.val(initialTourname);

        selectedProduct.find(".mode").toggleClass("hidden");
        input.focus();

    });

    $(".edit.mode").on("keyup", function (event) {
        // keyCode == 13 for Enter key
        if (event.keyCode == 13) {

            const selectedProduct = $(this).closest(".product-block");
            const newTitle = $(this).val();

            selectedProduct.find(".title-view").text(newTitle);

            const id = selectedProduct.attr('data-id');
            const url = `${baseUrl}/Tourism/UpdateTourName?id=${id}&name=${newTitle}`;

            $.get(url);

            selectedProduct.find(".mode").toggleClass("hidden");
        }
    });

})