$(document).ready(function () {

    $('#UserName').on("focusout", function (event) {
        // debounce

        // send request
        const name = $('#UserName').val();
        const baseUrl = 'https://localhost:7210';
        const url = `${baseUrl}/Auth/IsNameUniq?name=${name}`;
        $('#UserName').css('border-color', 'orange');
        $.get(url)
            .done(function (response) {
                if (response) {
                    // free name
                    $('#UserName').css('border-color', 'green');
                } else {
                    // Forbidden
                    $('#UserName').css('border-color', 'red');
                }

            });
    });

});