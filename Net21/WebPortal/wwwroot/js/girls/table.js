// JavaScript for Girl Table page
document.addEventListener('DOMContentLoaded', function () {
    // Handle edit mode for names
    const editableNames = document.querySelectorAll('.view-mode');
    editableNames.forEach(nameElement => {
        nameElement.addEventListener('click', function () {
            const row = this.closest('tr');
            const viewMode = row.querySelector('.view-mode');
            const editMode = row.querySelector('.edit-mode');

            if (viewMode && editMode) {
                viewMode.classList.add('hidden');
                editMode.classList.remove('hidden');
                editMode.focus();
                editMode.select();
            }
        });
    });

    // Handle save on Enter key or blur
    const editInputs = document.querySelectorAll('.edit-mode');
    editInputs.forEach(input => {
        input.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                saveEdit(this);
            }
        });

        input.addEventListener('blur', function () {
            saveEdit(this);
        });
    });

    function saveEdit(input) {
        const row = input.closest('tr');
        const viewMode = row.querySelector('.view-mode');
        const editMode = row.querySelector('.edit-mode');
        const validationMessage = row.querySelector('.validation-message');

        const newName = input.value.trim();

        if (newName === '') {
            validationMessage.classList.remove('hidden');
            return;
        }

        validationMessage.classList.add('hidden');
        viewMode.textContent = newName;
        viewMode.classList.remove('hidden');
        editMode.classList.add('hidden');

        // Here you could add AJAX call to save the name to server
        console.log('Saving name:', newName, 'for row:', row.dataset.id);
    }

    // Handle action buttons
    document.querySelectorAll('.like-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const row = this.closest('tr');
            console.log('Like clicked for row:', row.dataset.id);
            // Add like functionality here
        });
    });

    document.querySelectorAll('.dislike-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const row = this.closest('tr');
            console.log('Dislike clicked for row:', row.dataset.id);
            // Add dislike functionality here
        });
    });

    document.querySelectorAll('.delete-btn').forEach(btn => {
        btn.addEventListener('click', function (e) {
            if (!confirm('Вы уверены, что хотите удалить эту запись?')) {
                e.preventDefault();
            }
        });
    });


    $('.girls-table th').click(function () {
        const fieldName = $(this).attr('data-name');
        if (!fieldName) {
            return;
        }

        const currentSortedFieldName = getFieldNameValueFromQueryParamete('fieldName');
        if (fieldName == currentSortedFieldName) {
            const currentSortedFieldName = getFieldNameValueFromQueryParamete('sortDirection');
            if (!currentSortedFieldName || currentSortedFieldName == '1') {
                document.location.href = updateQueryStringParameter(document.location.href, 'sortDirection', '2')
                return;
            } 
            document.location.href = updateQueryStringParameter(document.location.href, 'sortDirection', '1')
            return;
        }

        document.location.href = updateQueryStringParameter(document.location.href, 'fieldName', fieldName)
    });

    function updateQueryStringParameter(uri, key, value) {
        var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
        else {
            return uri + separator + key + "=" + value;
        }
    }

    function getFieldNameValueFromQueryParamete(paramName) {
        var re = new RegExp("([?&])" + paramName + "=.*?(&|$)", "i");
        var regExResult = document.location.href.match(re);
        if (!regExResult) {
            return undefined;
        }

        return regExResult[0].split('=')[1].replace('&', '');
    }
});
