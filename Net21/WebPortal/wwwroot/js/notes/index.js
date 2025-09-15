$(document).ready(function () {
    $(".toggle-note").click(function (e) {
        e.preventDefault();

        const currentNote = $(this);
        let text = currentNote.siblings(".note-text");
        const show = currentNote.data("show");
        const hide = currentNote.data("hide");

        text.toggleClass("expanded");
        currentNote.text(text.hasClass("expanded") ? hide : show);
    });
});