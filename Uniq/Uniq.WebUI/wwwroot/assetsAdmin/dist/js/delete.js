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





//KATEGORİ DELETE JS
function confirmDeleteCommunication(id) {
    swal({
        title: "Silmek İstediğine Emin misin?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            deleteItemCommunication(id);
        }
    });
}

function deleteItemCommunication(id) {
    event.preventDefault();

    var data = {
        id: id
    };

    $.ajax({
        url: '/admin/iletisim/sil',
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
                        location.href = "/admin/iletisim";
                    }
                    else {
                        location.href = "/admin/iletisim";
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







//SOSYALMEDYA DELETE JS
function confirmDeleteSocialMedia(id) {
    swal({
        title: "Silmek İstediğine Emin misin?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            deleteItemSocialMedia(id);
        }
    });
}

function deleteItemSocialMedia(id) {
    event.preventDefault();

    var data = {
        id: id
    };

    $.ajax({
        url: '/admin/sosyalmedya/sil',
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
                        location.href = "/admin/sosyalmedya";
                    }
                    else {
                        location.href = "/admin/sosyalmedya";
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
//SOSYALMEDYA DELETE JS



//müsterihizmetlerikurumsal DELETE JS
function confirmDeleteCustomerServiceInstitutional(id) {
    swal({
        title: "Silmek İstediğine Emin misin?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            deleteItemCustomerServiceInstitutional(id);
        }
    });
}

function deleteItemCustomerServiceInstitutional(id) {
    event.preventDefault();

    var data = {
        id: id
    };

    $.ajax({
        url: '/admin/müsterihizmetlerikurumsal/sil',
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
                        location.href = "/admin/müsterihizmetlerikurumsal";
                    }
                    else {
                        location.href = "/admin/müsterihizmetlerikurumsal";
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
//müsterihizmetlerikurumsal DELETE JS