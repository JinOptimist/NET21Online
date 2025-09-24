$(document).ready(function () {
    console.log('Catalog script loaded');
    console.log('Edit buttons count:', $('.edit-product').length);

    $('.add-to-cart').click(function (e) {
        e.preventDefault();
        const productId = $(this).data('id');
        alert(`Товар ID: ${productId} добавлен в корзину`);
    });

    $('.edit-product').click(function (e) {
        e.preventDefault();
        const productId = $(this).data('id');
        const productType = $(this).data('product-type');

        console.log('Editing product:', productId, productType);

        $.get('/Marketplace/Edit', { id: productId, productType: productType })
            .done(function (data) {
                console.log('Modal data loaded successfully');

                $('#editProductModalContainer').html(data);

                $('#editProductModal').modal('show');

                $('#editProductForm').on('submit', function (e) {
                    e.preventDefault();

                    $.ajax({
                        url: $(this).attr('action'),
                        type: 'POST',
                        data: $(this).serialize(),
                        success: function (response) {
                            if (response.success) {
                                $('#editProductModal').modal('hide');
                                showAlert('success', response.message);
                                setTimeout(() => location.reload(), 1000);
                            } else {
                                if (response.errors) {
                                    displayValidationErrors(response.errors);
                                } else {
                                    showAlert('danger', response.message);
                                }
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error('Save error:', error);
                            showAlert('danger', 'Ошибка при сохранении');
                        }
                    });
                });
            })
            .fail(function (xhr, status, error) {
                console.error('Error loading modal:', error);
                console.log('Status:', status);
                console.log('Response:', xhr.responseText);
                showAlert('danger', 'Ошибка при загрузке формы редактирования');
            });
    });
    $('.delete-product').click(function (e) {
        e.preventDefault();
        const productId = $(this).data('id');
        const productType = $(this).data('product-type');
        const productName = $(this).data('name');

        if (confirm(`Вы уверены, что хотите удалить товар "${productName}"?`)) {
            $.ajax({
                url: '/Marketplace/DeleteAjax',
                type: 'POST',
                data: { id: productId, productType: productType },
                success: function (response) {
                    if (response.success) {
                        $(`#product-${productId}`).remove();
                        showAlert('success', response.message);

                        if ($('.product-card').length === 0) {
                            $('.container').append(`
                                <div class="alert alert-info">
                                    <h4 class="alert-heading">Товары не найдены</h4>
                                    <p>Каталог товаров пуст</p>
                                </div>
                            `);
                        }
                    } else {
                        showAlert('danger', response.message);
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Delete error:', error);
                    showAlert('danger', 'Ошибка при удалении товара');
                }
            });
        }
    });

    function showAlert(type, message) {
        $('.alert-dismissible').alert('close');

        const alertHtml = `
            <div class="alert alert-${type} alert-dismissible fade show" role="alert">
                ${message}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `;
        $('.container').prepend(alertHtml);
        setTimeout(() => {
            $('.alert-dismissible').alert('close');
        }, 5000);
    }

    function displayValidationErrors(errors) {
        $('.invalid-feedback').remove();
        $('.is-invalid').removeClass('is-invalid');
        for (var field in errors) {
            var input = $('[name="' + field + '"]');
            input.addClass('is-invalid');
            input.after('<div class="invalid-feedback">' + errors[field].join(', ') + '</div>');
        }
    }
});