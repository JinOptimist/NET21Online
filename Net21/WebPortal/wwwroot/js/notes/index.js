$(document).ready(function () {
    const baseUrl = 'http://localhost:5062';
    $(".toggle-note").click(function (e) {
        e.preventDefault();

        const currentNote = $(this);
        let text = currentNote.siblings(".note-text");
        const show = currentNote.data("show");
        const hide = currentNote.data("hide");

        text.toggleClass("expanded");
        currentNote.text(text.hasClass("expanded") ? hide : show);
    });

    $(".note .note-title .view").on("click", function () {
        const titleBlock = $(this).closest(".note-title");

        if (titleBlock.data("editable") === false) {
            return;
        }

        titleBlock.find(".view, .edit").toggleClass("hidden");
        titleBlock.find(".edit").focus();
    });

    $(".note .note-title .edit").on("keyup", function (event) {
        const titleBlock = $(this).closest(".note-title");

        if (titleBlock.data("editable") === false) {
            return;
        }

        // keyCode == 13 for Enter key
        if (event.keyCode === 13) {
            const newTitle = $(this).val().trim();

            if (!newTitle) {
                titleBlock.find(".empty-title").removeClass("hidden");
                return;
            } else {
                titleBlock.find(".empty-title").addClass("hidden");
            }

            const note = $(this).closest(".note");
            const id = note.attr("data-id");

            titleBlock.find(".view").text(newTitle);
            titleBlock.find(".view").removeClass("hidden");
            titleBlock.find(".edit").addClass("hidden");

            const url = `${baseUrl}/Notes/UpdateTitle?id=${id}&title=${encodeURIComponent(newTitle)}`;
            $.get(url);
        }
    });
});