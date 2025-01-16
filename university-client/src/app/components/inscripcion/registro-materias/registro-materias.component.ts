import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Clase } from 'src/app/models/clase';
import { Materia } from 'src/app/models/materia';
import { AuthService } from 'src/app/services/auth.service';
import { ClaseService } from 'src/app/services/clase.service';
import { EstudianteService } from 'src/app/services/estudiante.service';
import { MateriaService } from 'src/app/services/materia.service';
import { ToastService } from 'src/app/shared/services/toast.service';

@Component({
  selector: 'app-registro-materias',
  templateUrl: './registro-materias.component.html',
  styleUrls: ['./registro-materias.component.scss']
})
export class RegistroMateriasComponent implements OnInit {
  creditos = 0;
  materias: Materia[] = [];
  materiasSeleccionadas: Materia[] = [];
  loading = false;
  sinCreditos = false;

  constructor(
    private toast: ToastService,
    private router: Router,
    private authService: AuthService,
    private estudianteServce: EstudianteService,
    private materiaService: MateriaService,
    private claseService: ClaseService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    const id = this.authService.getUserId();

    this.estudianteServce.getEstudiante(id).subscribe({
      next: (res) => {
        this.creditos = res.creditos!;
        this.sinCreditos = this.creditos <= 0;
        if (this.creditos > 0) {
          this.getMaterias();
        }
        this.loading = false;
      },
      error: (err) => this.toast.showError('Error al consultar estudiante', err.message)
    });
  }

  getMaterias() {
    this.materiaService.getMaterias().subscribe({
      next: (res) => {
        this.materias = res
      }
    });
  }

  seleccionarMateria(materia: Materia) {
    const materiaAgregada = this.materiasSeleccionadas.some(m => m.materiaId === materia.materiaId);
    const mismoProfesor = this.materiasSeleccionadas.some(m => m.profesorId === materia.profesorId);

    if (this.creditos <= 0 || materia.creditos! > this.creditos )
      this.toast.showWarning('Advertencia', 'Sr. usuario ya no cuenta con créditos suficientes para inscribir la materia');
    else if (materiaAgregada)
      this.toast.showWarning('Advertencia', 'Sr. usuario ya inscribió esta materia');
    else if (mismoProfesor)
      this.toast.showWarning('Advertencia', 'No puede inscribir otra materia con el mismo profesor');
    else {
      this.materiasSeleccionadas.push(materia);
      const index = this.materias.indexOf(materia);
      this.materias.splice(index, 1);
      this.creditos -= materia.creditos!;
    }
  }

  eliminarMateria(materia: Materia) {
    const index = this.materiasSeleccionadas.indexOf(materia);
    this.materiasSeleccionadas.splice(index, 1);
    this.materias.unshift(materia);
    this.creditos += materia.creditos!;
  }

  inscribirMaterias() {
    const clases = this.materiasSeleccionadas.map(m =>
       <Clase> {
        profesorId: m.profesorId,
        materiaId: m.materiaId
      }
    );

    this.claseService.create(clases).subscribe({
      next: () => {
        this.toast.showSuccess('Registro satisfactorio', 'Se registraron las materias correctamente')
        this.router.navigateByUrl('/clases')
      },
      error: (err) => this.toast.showError('No lograron inscribir las materias', err.message)
    })
  }
}
