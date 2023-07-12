$(document).ready(function () {
    getCartCounter();
});


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

function getCartCounter() {
    $.ajax({
        url: "/sepetim/sayiver",
        type: "GET",
        success: function (data) {
            $(".cartCounter").text(data);
        }
    });
}

function removeCart(productid) {
    $.ajax({
        url: "/sepetim/sil",
        type: "POST",
        data: { productid: productid },
        success: function (data) {
            if (data == "OK") location.href = "/sepetim";
        }
    });
} 


function minusquantity(productid) {
    $.ajax({
        url: "/sepetim/azalt",
        type: "POST",
        data: { productid: productid },
        success: function (data) {
            if (data != -1) {
                getCartCounter();
                location.href = "/sepetim";
            }
            else {
                getCartCounter();
                location.href = "/sepetim";
            }
        }
    });
}

function plusquantity(productid) {
    $.ajax({
        url: "/sepetim/arttir",
        type: "POST",
        data: { productid: productid },
        success: function (data) {
            if (data != -1) {
                getCartCounter();
                location.href = "/sepetim";
            }
            else {
                location.href = "/sepetim";
                getCartCounter();
            }
        }
    });
}
