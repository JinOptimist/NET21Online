$(document).ready(function () {
    // клик по строке — переключаем галочку
    $(".callrequest").click(function (e) {
        if (!$(e.target).hasClass("select-request") &&
            !$(e.target).hasClass("view") &&
            !$(e.target).hasClass("edit")) {
            const checkbox = $(this).find(".select-request");
            checkbox.prop("checked", !checkbox.prop("checked"));
        }
    });

    // Delete / Backspace — удаление выделенных
    $(document).on("keyup", function (event) {
        if (event.keyCode == 46 || event.keyCode == 8) {
            $(".select-request:checked").closest(".callrequest").remove();
        }
    });

    // Редактирование имени по клику
    $(document).on('click', '.callrequest .view.mode', function (e) {
        e.stopPropagation();
        const nameBlock = $(this).closest('.name');
        const input = nameBlock.find('.edit');
        input.val(nameBlock.find('.view').text().trim());
        nameBlock.find('.mode').toggleClass('hidden');
        input.focus();
    });
    
    // Клик по input — чтобы не переключался чекбокс
    $(document).on('click', '.callrequest .edit.mode', function(e){
        e.stopPropagation();
    });

    // Сохранение имени по Enter / отмена по Esc
    $(document).on('keyup', '.callrequest .edit.mode', function (event) {
        const nameBlock = $(this).closest('.name');

        if (event.keyCode === 13) { // Enter
            const newName = $(this).val().trim();
            if (!newName) {
                nameBlock.find('.empty-name').removeClass('hidden');
                return;
            } else {
                nameBlock.find('.empty-name').addClass('hidden');
            }

            const id = $(this).closest('.callrequest').attr('data-id');

            $.post("/AdminCdekProject/UpdateName", { id: id, name: newName })
                .done(function(response){
                    if (!response) {
                        alert('Ошибка: не удалось обновить имя.');
                        location.reload();
                    }
                })
                .fail(function(){
                    alert('Ошибка сети.');
                    location.reload();
                });

            nameBlock.find('.view').text(newName);
            nameBlock.find('.mode').toggleClass('hidden');
        }
        else if (event.keyCode === 27) { // Esc
            nameBlock.find('.mode').toggleClass('hidden');
        }
    });

    // Чат Бокс
    $(document).ready(function () {
        const hub = new signalR.HubConnectionBuilder()
            .withUrl(`${baseUrl}/cdekchat`)
            .build();

        hub.on("ReceiveMessage", function (id, user, message) {
            const msgDiv = $("<div>")
                .addClass("chat-message")
                .addClass(user === "Client" ? "you" : "other")
                .text(user + ": " + message);

            $(".chat-messages").append(msgDiv);

            // автопрокрутка вниз
            $(".chat-messages").scrollTop($(".chat-messages")[0].scrollHeight);
        });

        hub.start().catch(err => console.error(err.toString()));

        $(".chat-send").click(function () {
            const message = $(".chat-input").val();
            if (message.trim() === "") 
            {
                return;
            }

            hub.invoke("SendMessage", "Admin", message); // для клиента
            $(".chat-input").val("");
        });
    });
});