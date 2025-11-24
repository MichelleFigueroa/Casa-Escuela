    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace CasaEscuela.BL.DTOs.PermisosDTOs
    {
        public class PermisoDTO
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "El usuario es obligatorio.")]
            public int IdUsuario { get; set; }

            [Required(ErrorMessage = "La vista es obligatoria.")]
            [StringLength(100, ErrorMessage = "La vista no puede superar los 100 caracteres.")]
            public string Vista { get; set; }

            [Required(ErrorMessage = "Debe indicar si el permiso puede ver.")]
            public bool PuedeVer { get; set; }

            [Required(ErrorMessage = "Debe indicar si el permiso puede crear.")]
            public bool PuedeCrear { get; set; }

            [Required(ErrorMessage = "Debe indicar si el permiso puede editar.")]
            public bool PuedeEditar { get; set; }

            [Required(ErrorMessage = "Debe indicar si el permiso puede eliminar.")]
            public bool PuedeEliminar { get; set; }
        }
    }
