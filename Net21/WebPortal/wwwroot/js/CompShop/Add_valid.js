$(document).ready(function () {

    $('#UserName').on("focusout", function () {

        const name = $('#UserName').val();

        if (name.length < 3) {
            $('#UserName').css('border-color', 'red');
            return;
        }

        const baseUrl = 'https://localhost:7210';
        const url = `${baseUrl}/CompShop/IsNameUniq?name=${name}`;

        $('#UserName').css('border-color', 'orange');
        $.get(url)
            .done(function (response) {
                if (response) {
                    $('#UserName').css('border-color', 'green');
                } else {
                    $('#UserName').css('border-color', 'red');
                }

            });
    });

});