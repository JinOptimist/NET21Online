$(document).ready(function () {

    let debounceTimer;

    $('#UserName').on("focusout", function (e) {
        clearTimeout(debounceTimer);

        debounceTimer = setTimeout(() => {
            const name = $('#UserName').val();
            const statusIcon = $('#username-status');

            statusIcon.removeClass('status-checking status-available status-taken status-error');

            statusIcon.addClass('status-checking');

            const baseUrl = 'http://localhost:5062';
            const url = `${baseUrl}/AuthNotes/IsNameUniq?name=${name}`;

            $.get(url)
                .done(function (response) {
                    statusIcon.removeClass('status-checking');

                    if (response) {
                        statusIcon.addClass('status-available');
                    } else {
                        statusIcon.addClass('status-taken');
                    }
                })
                .fail(function() {
                    statusIcon.removeClass('status-checking');
                    statusIcon.addClass('status-error');
                    console.error('Username is not unique');
                });
        }, 500);
    });
});