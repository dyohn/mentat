$('#registerModal').modal('show');

$(document).ready(function () {
    $('#registerModal').on('show.bs.modal', function (event) {
        let button = $(event.relatedTarget);
        let url = button.data('url');
        let modal = $(this);
        $.get(url, function (data) {
            modal.find('.modal-body').html(data); 
        });
    });
});

// Literally the exact same code for the login modal:

$('#loginModal').modal('show');

$(document).ready(function () {
    $('#loginModal').on('show.bs.modal', function (event) {
        let button = $(event.relatedTarget);
        let url = button.data('url');
        let modal = $(this);
        $.get(url, function (data) {
            modal.find('.modal-body').html(data);
        });
    });
});
