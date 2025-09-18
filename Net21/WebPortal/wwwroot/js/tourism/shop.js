$(document).ready(function(){

     $(".product-block").click(function () {

        const selectedProduct = $(this);
        selectedProduct.toggleClass('marked-to-remove');

        const anyProductSelected = document.querySelector('.product-block.marked-to-remove') 
        const showButtonForRemoving = $(".hidden-button");
        
        if(anyProductSelected !== null){
        showButtonForRemoving.addClass("show-button")
        }
        else{
        showButtonForRemoving.removeClass("show-button");
        }

    });


     $(".remove-action").click(function (event) {

        $(".product-block.marked-to-remove").remove();
        $(".hidden-button.show-button").removeClass("show-button");
        console.log(event);
    });
})