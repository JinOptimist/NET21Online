//Vanila js
// let search = document.querySelector('.search-box');

// document.querySelector('#search-icon').onclick = () =>{
//     search.classList.toggle('active');
//     navbar.classList.remove('active');
// }

// let navbar = document.querySelector('.navbar');
// document.querySelector('#menu-icons').onclick = () =>{
//     navbar.classList.toggle('active');
//     search.classList.remove('active');
// }

// window.onscroll = ()=>{
//     search.classList.remove('active');
//     navbar.classList.remove('active');
// }

// let headers = document.querySelector('header');
// window.addEventListener('scroll',()=>{
//     headers.classList.toggle('shadow',window.scrollY > 0);
// })

//JQuery
$(document).ready(function () {
    let $search = $(".search-box");
    let $navbar = $(".navbar");
    let $header = $("header");
    let $copyright = $(".copyright");
    let $homeImg = $(".home-img");

    initCoffe();

    $("#search-icon").on("click", function () {
        $search.toggleClass("active");
        $navbar.removeClass("active");
    });

    $("#menu-icons").on("click", function () {
        $navbar.toggleClass("active");
        $search.removeClass("active");
    });

    $(window).on("scroll", function () {
        $search.removeClass("active");
        $navbar.removeClass("active");
        $header.toggleClass("shadow", $(window).scrollTop() > 0);
    });

    // $(document).on("keydown", function (e) {
    //   if (e.code === "Space") {
    //     e.preventDefault(); 
    //     $search.removeClass("active");
    //     $navbar.removeClass("active");
    //   }
    // });

    $copyright.on("click", function () {
        $(this).toggleClass("active");
    });

    $(document).on("keydown", function (e) {

        let tag = (e.target && e.target.tagName || "").toLowerCase();
        if (tag === "input" || tag === "textarea" || e.target.isContentEditable) {
            return;
        }

        if (e.code === "Space" || e.code === "" || e.code === "Spacebar") {
            var $active = $(".copyright.active");
            if ($active.length) {
                e.preventDefault();
                $active.hide();
                $active.removeClass("active");
            }

        }
    });

    $homeImg.on("dblclick", function () {
        $(this).fadeOut(400);
    });

    $(document).on("keydown", function (e) {
        if (e.code === "ShiftLeft" || e.code === "ShiftRight") {
            $homeImg.stop().fadeIn(400);
        }
    });

    //function initCoffe() {

    //    const url = `${coffeProductBaseUrl}/GetProduct`;
    //    $.get(url)
    //        .done(function (urls) {
    //            urls.forEach(url => {
    //                const coffeTags = $('.coffe-minimal-api .box.templatescoffe').clone();

    //                coffeTags.removeClass('templatescoffe');
    //                coffeTags.find('.box.editable-title span').text("Arabics");
    //                coffeTags.find('.products-container img').attr('src', url);

    //                $('.products-container.coffe-minimal-api').append(coffeTags);
    //            });
    //        })
    //}

    function initCoffe() {
        const url = `${coffeProductBaseUrl}/GetNameCoffe`; 

        $.get(url).done(function (coffeProducts) {
            coffeProducts.forEach(coffeProduct => {
                const coffeTags = $('.coffe-minimal-api .box.templatescoffe').clone();

                coffeTags.removeClass('templatescoffe');
                coffeTags.find('.editable-title span').text(coffeProduct.name);
                coffeTags.find('img').attr('src', decodeURIComponent(coffeProduct.url)); 

                $('.products-container.coffe-minimal-api').append(coffeTags);
            });
        });
    }

});
