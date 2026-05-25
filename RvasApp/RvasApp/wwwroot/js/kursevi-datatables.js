// DataTables: pretraga po nazivu, sort po kolonama, filter polaznika, 5 po stranici
$(function () {
    var $table = $('#kurseviTable');
    if (!$table.length) return;

    var table = $table.DataTable({
        pageLength: 5,
        lengthChange: false,
        order: [],
        dom: 'rtip',
        language: {
            paginate: {
                first: 'Prva',
                last: 'Poslednja',
                next: 'Sledeca',
                previous: 'Prethodna'
            },
            info: 'Prikazano _START_-_END_ od _TOTAL_ kurseva',
            infoEmpty: 'Nema kurseva za prikaz',
            zeroRecords: 'Nema kurseva koji odgovaraju kriterijumima',
            emptyTable: 'Nema kurseva za prikaz'
        }
    });

    $('#kurseviSearch').on('keyup', function () {
        table.column(0).search(this.value).draw();
    });

    $('#kurseviFilterPolaznici').on('change', function () {
        table.column(2).search(this.value, false, false).draw();
    });
});
