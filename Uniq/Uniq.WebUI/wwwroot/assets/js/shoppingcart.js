(function ($) {

    getCartCounter();
})(jQuery);

function addCart(productid) {
    var istenenmiktar = parseInt($(".inputQuantity").val())
    $.ajax({
        url: "/sepetim/ekle",
        type: "POST",
        data: { productid: productid, quantity: istenenmiktar },
        success: function (data) {
            if (data != "") {
                getCartCounter();
            }
        }
    });
}

//function getCartCounter() {
//    $.ajax({
//        url: "/sepetim/sayiver",
//        type: "GET",
//        success: function (data) {
//            $(".cartCounter").text(data);
//        }
//    });
//}

//function removeCart(productid) {
//    $.ajax({
//        url: "/sepetim/sil",
//        type: "POST",
//        data: { productid: productid },
//        success: function (data) {
//            if (data == "OK") location.href = "/sepetim";
//        }
//    });
//} 