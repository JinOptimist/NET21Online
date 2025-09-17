$(document).ready(function () {
  const baseUrl = "https://localhost:7210";

  $(".view.mode").click(function () {
    const nameBlockCoffe = $(this).closest(".box");

    const initialCoffeName = nameBlockCoffe.find(".view").text();
    nameBlockCoffe.find(".edit").val(initialCoffeName);

    nameBlockCoffe.find(".mode").toggleClass("hidden");
  });

  $(".edit.mode").on("keyup", function (event) {
    if (event.keyCode === 13) {
      const nameBlockCoffe = $(this).closest(".box");

      const newName = $(this).val();
      const span = nameBlockCoffe.find(".view");
      span.text(newName);

      const id = nameBlockCoffe.data("id");

      $.post(`${baseUrl}/CoffeShop/EditCoffeName`, { id: id, name: newName })
        .done(function (response) {
          console.log("Сохранено успешно:", response);
        })
        .fail(function (xhr, status, error) {
          console.error("Ошибка сохранения:", error);
        });

      nameBlockCoffe.find(".mode").toggleClass("hidden");
    }
  });

  //////Dont touch fixing
$(".delete-btn").click(function (e) {
  e.stopPropagation(); 

  const nameBlockCoffe = $(this).closest(".box");
  const id = nameBlockCoffe.data("id");
  const productName = nameBlockCoffe.find(".view").text();

if (confirm(`Вы уверены, что хотите удалить "${productName}"?`)) {
    $.post(`${baseUrl}/CoffeShop/DeleteCoffee`, { id: id })
      .done(function (response) {
        if (response.success) {
          nameBlockCoffe.fadeOut(300, function () {
            $(this).remove();
          });
        } else {
          console.error("Ошибка удаления:", response.message);
        }
      })
      .fail(function (xhr, status, error) {
        console.error("Ошибка сервера при удалении:", error);
      });
  }
});
});
