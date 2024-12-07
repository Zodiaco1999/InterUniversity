using System;
using System.Collections.Generic;

namespace UniversityApi.DataAccess;

public partial class MateriaProfesor
{
    public int MateriaId { get; set; }

    public int ProfesorId { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();

    public virtual Materia Materia { get; set; } = null!;

    public virtual Profesor Profesor { get; set; } = null!;
}
