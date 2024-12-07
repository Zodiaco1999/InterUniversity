import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Programa } from 'src/app/models/programa';
import { AuthService } from 'src/app/services/auth.service';
import { EstudianteService } from 'src/app/services/estudiante.service';
import { ToastService } from 'src/app/shared/services/toast.service';

@Component({
  selector: 'app-inscripcion-programa',
  templateUrl: './inscripcion-programa.component.html',
  styleUrls: ['./inscripcion-programa.component.scss']
})
export class InscripcionProgramaComponent implements OnInit {
  programa: Programa = {};
  invalid = false;

  constructor(
    private router: Router,
    private toast: ToastService,
    private authService: AuthService,
    private estudianteService: EstudianteService)
    {}

  ngOnInit(): void {
    this.estudianteService.getPrograma().subscribe({
      next: (res) => this.programa = res,
      error: (err) => {
        this.toast.showWarning('Inscripcion invalida', err.message);
        this.invalid = true
      }
    });
  }

  inscribir() {
    this.estudianteService.create().subscribe({
      next: () => {
        this.toast.showSuccess('¡Se realizo la inscrpcion correctamente!', '');
        this.authService.myDataVisible$.next(true);
        this.router.navigateByUrl('/registromaterias')
      },
      error: (err) => {
        this.toast.showError('Inscripcion fallida', err.message)
      }
    })
  }


}
