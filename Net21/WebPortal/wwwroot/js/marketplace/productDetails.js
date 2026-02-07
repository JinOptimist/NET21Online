function addToCart(productId) {
    document.dispatchEvent(new CustomEvent('marketplace:add-to-cart', { detail: { productId } }));
}
