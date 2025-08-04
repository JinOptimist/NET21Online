let search = document.querySelector('.search-box');

document.querySelector('#search-icon').onclick = () =>{
    search.classList.toggle('active');
    navbar.classList.remove('active');
}

let navbar = document.querySelector('.navbar');

document.querySelector('#menu-icons').onclick = () =>{
    navbar.classList.toggle('active');
    search.classList.remove('active');
}

window.onscroll = ()=>{
    search.classList.remove('active');
    navbar.classList.remove('active');
}


let headers = document.querySelector('header');

window.addEventListener('scroll',()=>{
    headers.classList.toggle('shadow',window.scrollY > 0);
})