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
    const materiaAgregada = this.materiasSeleccionadas.find((m) => m.materiaId === materia.materiaId);

    if (!materiaAgregada) {
      if (this.creditos > 0) {
        if (!this.materiasSeleccionadas.some(m => m.profesorId === materia.profesorId)) {
          this.materiasSeleccionadas.push(materia);
          const index = this.materias.indexOf(materia);
          this.materias.splice(index, 1);
          this.creditos -= materia.creditos!;
        } else {
          this.toast.showWarning('Advertencia', 'No pude inscribir otra materia con el mismo profesor');
        }
      } else
        this.toast.showWarning('Advertencia', 'Sr. usuario ya no cuenta con mas créditos para inscribir materias');
    }
  }

  eliminarMateria(materia: Materia) {
    const index = this.materiasSeleccionadas.indexOf(materia);
    this.materiasSeleccionadas.splice(index, 1);
    this.materias.unshift(materia);
    this.creditos += materia.creditos!;
  }

  inscribirMaterias() {
    const clases = this.materiasSeleccionadas.map(m => {
      const clase: Clase = {
        profesorId: m.profesorId,
        materiaId: m.materiaId
      }
      return clase;
    });

    this.claseService.create(clases).subscribe({
      next: () => {
        this.toast.showSuccess('Registro satisfactorio', 'Se registraron las materias correctamente')
        this.router.navigateByUrl('/clases')
      },
      error: (err) => this.toast.showError('No lograron inscribir las materias', err.message)
    })
  }
}
