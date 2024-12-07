using System;
using System.Collections.Generic;

namespace UniversityApi.DataAccess;

public partial class Clase
{
    public int ProfesorId { get; set; }

    public int EstudianteId { get; set; }

    public int MateriaId { get; set; }

    public virtual Estudiante Estudiante { get; set; } = null!;

    public virtual MateriaProfesor MateriaProfesor { get; set; } = null!;
}
