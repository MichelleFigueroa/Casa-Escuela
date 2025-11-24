// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var ActionsUI = {
    init: function (pAction) {
        $("*[data-disabledui_" + pAction +"]").attr("disabled", "disabled");
        $("*[data-readonlyui_" + pAction + "]").attr("readonly", "readonly");
    },
    create: function (pAction,pFunc) {
        if (pAction == "crear") {
            pFunc();
        }
    },
    edit: function (pAction, pFunc) {
        if (pAction == "modificar") {
            pFunc();
        }
    },
    delete: function (pAction, pFunc) {
        if (pAction == "eliminar") {
            pFunc();
        }
    },
    detail: function (pAction, pFunc) {
        if (pAction == "ver") {
            pFunc();
        }
    },
     allEx: function (pAction,pActionEx, pFunc) {
         if (pAction != pActionEx) {
            pFunc();
        }
    }, 

     // NUEVOS MÉTODOS GLOBALES
    alertSuccess: function (message) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: message
        });
    },

    alertError: function (message) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: message
        });
    },

    alertConfirmDelete: function (nombre, onConfirm) {
        Swal.fire({
            title: '¿Estás seguro?',
            text: `Estás a punto de eliminar a ${nombre}. Esta acción no se puede deshacer.`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                onConfirm();
            }
        });
    },
};
