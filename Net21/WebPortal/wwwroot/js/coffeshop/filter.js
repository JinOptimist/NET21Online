$(function () {
    let sortAsc = true;

    $('#searchBox').on('keyup', function () {
        const value = $(this).val().toLowerCase();
        $('#endpointsTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    $('#endpointsTable th[data-col]').on('click', function () {
        const table = $('#endpointsTable');
        const tbody = table.find('tbody');
        const index = $(this).data('col');
        const rows = tbody.find('tr').toArray();

        sortAsc = !$(this).hasClass('asc');
        $('#endpointsTable th').removeClass('asc desc');
        $(this).addClass(sortAsc ? 'asc' : 'desc');

        rows.sort(function (a, b) {
            const A = $(a).children('td').eq(index).text().toLowerCase();
            const B = $(b).children('td').eq(index).text().toLowerCase();
            if (A < B) return sortAsc ? -1 : 1;
            if (A > B) return sortAsc ? 1 : -1;
            return 0;
        });

        $.each(rows, function (i, row) {
            tbody.append(row);
        });
    });
});