$(function () {
    $('#PlayerDescTable').DataTable({
        "paging": false,
        "order": [[1, 'asc']],
        "columnDefs": [
            {
                "targets": 0,
                "orderable": false,
                "searchable": false
            },
            {
                "targets": 3,
                "orderable": false,
                "searchable": false
            }
        ]
    });

    $('.EditorFactionViewerTable').DataTable({
        "dom": 'Pfrti',
        searchPanes: {
            initCollapsed: true,
            layout: 'columns-1'
        },
        "paging": false,
        "order": [[3, 'asc']],
        "columnDefs": [
            {
                "targets": 0,
                "orderable": false,
                "searchable": false,
                searchPanes: {
                    show: false
                }
            },
            {
                "targets": 1,
                searchPanes: {
                    show: true
                }
            },
            {
                "targets": [2, 3],
                searchPanes: {
                    show: false
                }
            }
        ]
    });
});