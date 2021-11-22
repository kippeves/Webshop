// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$("#cart").load("/User/Cart").hide();

$("#cartFlip").on('click', function () {
    $('#cart').toggle();
});

$(document).on('click', 'a.addremove', function (event) {
    var id = this.id.replace("add", "");
    if (this.id.match(/add[\d]+/)) {
        var id = this.id.replace("add", "");
      $.get("/User/Cart", { id: id, handler: "Add" }).done(function (data) {
          $("#amt" + id).html(data.amount);
          $("#cart").load("/User/Cart");
          $('#addToast').toast('show')
        });
    } else if (this.id.match(/rem[\d]+/)) {
        var id = this.id.replace("rem", "");
      $.get("/User/Cart", { id: id, handler: "Remove" }).done(function (data) {
          $("#amt" + id).html(data.amount);
          $("#cart").load("/User/Cart");
          $('#removeToast').toast('show')
        });
    }
    event.preventDefault();
});