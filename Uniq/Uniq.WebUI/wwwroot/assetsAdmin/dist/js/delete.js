$(document).ready(function () {

});



//KATEGORİ DELETE JS
function confirmDeleteCategory(id) {
    swal({
        title: "Silmek İstediğine Emin misin?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            deleteItemCategory(id);
        }
    });
}

function deleteItemCategory(id) {
    event.preventDefault();

    var data = {
        id: id
    };

    $.ajax({
        url: '/admin/kategoriler/sil',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response == "Ok") {
                swal({
                    title: "Silme İşlemi Başarılı",
                    icon: "success",
                    buttons: {
                        confirm: {
                            text: "Tamam",
                            value: true,
                            visible: true,
                            className: "",
                            closeModal: true
                        }
                    }
                }).then((value) => {
                    if (value) {
                        location.href = "/admin/kategoriler";
                    }
                    else {
                        location.href = "/admin/kategoriler";
                    }
                });
            } else {
                alert(response);
            }
        },
        error: function (xhr, status, error) {
            swal("Error", "An error occurred: " + error, "error");
        }
    });
}
//KATEGORİ DELETE JS
