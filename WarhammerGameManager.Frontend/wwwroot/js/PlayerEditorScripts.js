$(function () {
    $('#PlayerDescTable').DataTable({
        "paging": false,
        "columnDefs": [{
            "targets": 2,
            "orderable": false,
            "searchable": false
        }]
    });
});