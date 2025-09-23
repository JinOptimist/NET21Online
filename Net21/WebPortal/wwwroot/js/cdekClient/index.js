// Чат Бокс
$(document).ready(function () {
    // Создаём подключение к SignalR хабу
    const hub = new signalR.HubConnectionBuilder()
        .withUrl(`${baseUrl}/cdekchat`) // URL хаба
        .build(); // строим подключение

    // Подписка на событие получения сообщения
    hub.on("ReceiveMessage", function (id, userName, message) {
        // Создаём новый div для сообщения
        const msgDiv = $("<div>")
            .addClass("chat-message") // общий класс для всех сообщений
            .addClass(userName === currentUserName ? "you" : "other")
            // если автор сообщения — текущий пользователь, ставим "you", иначе "other"
            .text(userName + ": " + message);
        // текст сообщения: имя автора + сообщение

        // Добавляем сообщение в чат
        $(".chat-messages").append(msgDiv);

        // Автопрокрутка вниз, чтобы показывать последнее сообщение
        $(".chat-messages").scrollTop($(".chat-messages")[0].scrollHeight);
    });

    // Стартуем подключение к SignalR
    hub.start().catch(err => console.error(err.toString()));

    // Обработчик клика по кнопке "Отправить"
    $(".chat-send").click(function () {
        const message = $(".chat-input").val().trim(); // берём текст из input и убираем пробелы
        if (!message) return; // если пустое сообщение, выходим

        // Отправка POST запроса на сервер
        $.post(`${baseUrl}/api/CdekChat/SendMessageToUser`, { message: message })
            .fail(err => console.error("Ошибка при отправке сообщения:", err));

        // Очищаем поле ввода после отправки
        $(".chat-input").val("");
    });

    // Отправка сообщения по Enter
    $(".chat-input").keydown(function (e) {
        if (e.key === "Enter" && !e.shiftKey) {
            e.preventDefault(); // отменяем перенос строки
            $(".chat-send").click(); // вызываем клик на кнопку отправки
        }
    });
});