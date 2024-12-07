import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Estudiante } from 'src/app/models/estudiante';
import { AuthService } from 'src/app/services/auth.service';
import { EstudianteService } from 'src/app/services/estudiante.service';
import { FormBuilderService } from 'src/app/shared/services/form-builder.service';
import { ToastService } from 'src/app/shared/services/toast.service';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.scss']
})
export class UsuarioFormComponent implements OnInit {
  form: FormGroup;
  delete = false;
  id = 0;
  numeroIdentificacion = '';

  constructor(
    private estudianteService: EstudianteService,
    private toast: ToastService,
    private authService: AuthService,
    private route: ActivatedRoute,
    public formBuilder: FormBuilderService) {
    this.builForm();
    this.form = formBuilder.formGroup;
  }

  ngOnInit(): void {
    const idStr = this.route.snapshot.paramMap.get('id') ?? 0;
    this.id = +idStr;

    this.getEstudiante();
  }

  getEstudiante() {
    this.estudianteService.getEstudiante(this.id).subscribe({
      next: (res) => {
        this.numeroIdentificacion = res.numeroIdentificacion ?? '';
        const fechaNacimiento = res.fechaNacimiento?.toString().split('T')[0];
        const fechaInscrito = res.fechaInscrito?.toString().split('T')[0];
        this.form.reset({
          ...res,
          fechaNacimiento,
          fechaInscrito
        });
      },
      error: (err) => this.toast.showError('No se pudo consultar el usuario', err.message)
    })
  }

  guardar()  {
    const estudiante: Estudiante = this.form.value;
    estudiante.estudianteId = this.id;

    this.estudianteService.update(estudiante).subscribe({
        next: () => this.toast.showSuccess('Datos Actualizados', '¡Tu información se actualizo correctamente!'),
        error: (err) => this.toast.showError('No se lograron actualizar tus datos', err.message)
      });
  }

  eliminar() {
    this.estudianteService.delete(this.id).subscribe({
      next: () => {
        this.toast.showInfo('Usuario eliminado', '');
        this.authService.logout()
      },
      error: (err) => this.toast.showError('Fallo la elimanación', err.message)
    })
  }

  resetIdentificacion() {
    this.delete = true;
    this.form.reset();
  }

  resetForm() {
    this.delete = false;
    this.getEstudiante();
  }


  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'numeroIdentificacion',
        name: 'Identificación',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6,
            pattern: '(^[0-9]+$)',
            validationKey: 'pattern',
            validationMessage: 'Solo debe contar con numeros'
          }
        ]
      },
      {
        id: 'nombres',
        name: 'Nombres',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 3
          }
        ]
      },
      {
        id: 'apellidos',
        name: 'Apellidos',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 4
          }
        ]
      },
      {
        id: 'fechaNacimiento',
        name: 'Fecha Nacimiento',
        formState: '',
        validatorOrOpts: [
          {
            required: true
          }
        ]
      },
      {
        id: 'fechaInscrito',
        name: 'Fecha inscripción',
        formState: ''
      },
      {
        id: 'creditos',
        name: 'creditos',
        formState: ''
      }
    ];

    this.formBuilder.initFormGroup();
  }

}
