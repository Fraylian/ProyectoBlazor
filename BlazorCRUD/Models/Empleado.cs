using System;
using System.Collections.Generic;

namespace BlazorCRUD.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string NomrbreCompleto { get; set; } = null!;

    public int? IdDepartamento { get; set; }

    public int Sueldo { get; set; }

    public DateTime? FechaContrato { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }
}
