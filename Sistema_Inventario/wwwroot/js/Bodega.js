let datatable

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    datatable = $('#tb').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url":"/Admin/bodega/ObtenerBodegas"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "40%" },
            {
                "data": "estado", "whidth": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div>
                        <a href="Bodega/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a onclick=Delete("Bodega/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="bi bi-trash"></i>
                        </a>
                    </div>
                    `
                }, 
                "width": "20%"
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "¿Estas Seguro?",
        text: "Una vez eliminado, no podras recuperar este registro",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        } 
    });
}