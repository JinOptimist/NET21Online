$(document).ready(function () {
    const baseUrl = 'http://localhost:5062';
    let debounceTimer;

    $('#UserName').on("focusout", function (e) {
        clearTimeout(debounceTimer);

        debounceTimer = setTimeout(() => {
            const name = $('#UserName').val();
            const statusIcon = $('#username-status');

            statusIcon.removeClass('status-checking status-available status-taken status-error');
            statusIcon.addClass('status-checking');

            const url = `${baseUrl}/AuthNotes/IsNameUniq?name=${encodeURIComponent(name)}`;

            $.get(url)
                .done(function (response) {
                    statusIcon.removeClass('status-checking');

                    if (response) {
                        statusIcon.addClass('status-available');
                    } else {
                        statusIcon.addClass('status-taken');
                    }
                })
                .fail(function () {
                    statusIcon.removeClass('status-checking');
                    statusIcon.addClass('status-error');
                    console.error('Username is not unique');
                });
        }, 500);
    });
});