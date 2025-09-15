$(document).ready(function () {
    const $dropdown = $(".profile .dropdown");
    const $userInfo = $(".profile .user-info");
    const $profile = $(".profile");

    const menuLinks = [
        {element: $(".profile .dropdown a[href*='Profile']"), key: "1"},
        {element: $(".profile .dropdown a[href$='/Notes/Add']"), key: "2"},
        {element: $(".profile .dropdown a[href$='/Categories/Add']"), key: "3"},
        {element: $(".profile .dropdown a[href$='/Tags/Add']"), key: "4"},
        {element: $(".profile .dropdown a[href*='logout']"), key: "5"}
    ];

    function toggleDropdown() {
        $dropdown.toggleClass("open");
    }

    function closeDropdown() {
        $dropdown.removeClass("open");
    }

    function isFormInput(element) {
        return $(element).is("input, textarea, select");
    }

    function handleMenuShortcut(key, e) {
        const link = menuLinks.find(item => item.key === key);
        if (link && link.element.length) {
            e.preventDefault();
            link.element[0].click();
        }
    }

    $userInfo.on("click", function (e) {
        e.stopPropagation();
        toggleDropdown();
    });

    $(document).on("keydown", function (e) {
        const isInput = isFormInput(document.activeElement);
        const isDropdownOpen = $dropdown.hasClass("open");

        if (!isInput && e.code === "Space") {
            e.preventDefault();
            toggleDropdown();
            return;
        }

        if (e.code === "Escape") {
            closeDropdown();
            return;
        }

        if (isDropdownOpen && /^[1-5]$/.test(e.key)) {
            handleMenuShortcut(e.key, e);
        }
    });

    $(document).on("click", function (e) {
        if (!$(e.target).closest($profile).length) {
            closeDropdown();
        }
    });
});