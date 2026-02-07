$(document).ready(function () {
    function toggleProductFields() {
        $('.product-specs').hide();
        const selectedType = $('#productType').val();
        if (selectedType) {
            $(`#${selectedType.toLowerCase()}Fields`).show();
        }
    }

    $('#productType').change(toggleProductFields);
    toggleProductFields();

    if ($('#productType').val() === 'Laptop' &&
        ($('#Processor').hasClass('is-invalid') || $('#RAM').hasClass('is-invalid') || $('#OS').hasClass('is-invalid'))) {
        $('#laptopFields').show();
    }
});



