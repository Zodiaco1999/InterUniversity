using System;
using System.Collections.Generic;

namespace UniversityApi.DataAccess;

public partial class Materia
{
    public int MateriaId { get; set; }

    public string Titulo { get; set; } = null!;

    public byte Creditos { get; set; }

    public virtual ICollection<MateriaProfesor> MateriaProfesors { get; set; } = new List<MateriaProfesor>();
}
