using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "Este campo {0} es requerido.")]

        public string NomrbreCompleto { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Este campo {0} es requerido.")]    

        public int? IdDepartamento { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Este campo {0} es requerido.")]
        public int Sueldo { get; set; }

        public DateTime? FechaContrato { get; set; }

        public DepartamentoDTO Departamento { get; set; }
        
    }
}
