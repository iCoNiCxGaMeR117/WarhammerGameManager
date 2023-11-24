$(function () {
    $('.basicDataTable').each(function () {
        $(this).DataTable({
            "paging": false,
        });
    });

    $('#LoadingIconSection').fadeOut('fast', function () {
        $('#MainContent').fadeIn('fast');
    });
});

$('.loadButton').on('click', function () {
    $('#MainContent').fadeOut('fast', function () {
        $('#LoadingIconSection').fadeIn('fast');
    });
});