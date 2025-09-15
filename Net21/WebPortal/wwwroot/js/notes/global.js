$(document).ready(function () { 
    const dropdown = $(".profile .dropdown");
    const userInfo = $(".profile .user-info");
    const profileLink = $(".profile .dropdown a[href*='Profile']");
    const noteLink = $(".profile .dropdown a[href$='/Notes/Add']");
    const categoryLink = $(".profile .dropdown a[href$='/Categories/Add']");
    const tagLink = $(".profile .dropdown a[href$='/Tags/Add']");
    const logoutLink = $(".profile .dropdown a[href*='logout']");

    userInfo.click(function (e) {
        e.stopPropagation();
        dropdown.toggleClass("open");
    });

    $(document).keydown(function (e) {
        const active = document.activeElement;
        const isInput = $(active).is("input, textarea, select");

        if (!isInput) {
            if (e.code === "Space") {
                e.preventDefault();
                dropdown.toggleClass("open");
            }
        }

        if (e.code === "Escape") {
            dropdown.removeClass("open");
        }

        if (dropdown.hasClass("open")) {
            if (e.key === "1") {
                e.preventDefault();
                profileLink[0].click();
            }
            if (e.key === "2") {
                e.preventDefault();
                noteLink[0].click();
            }
            if (e.key === "3") {
                e.preventDefault();
                categoryLink[0].click();
            }
            if (e.key === "4") {
                e.preventDefault();
                tagLink[0].click();
            }
            if (e.key === "5") {
                e.preventDefault();
                logoutLink[0].click();
            }
        }
    });

    $(document).click(function (e) {
        if (!$(e.target).closest(".profile").length) {
            dropdown.removeClass("open");
        }
    });
});