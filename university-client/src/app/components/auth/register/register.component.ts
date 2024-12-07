import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilderService } from 'src/app/shared/services/form-builder.service';
import { ToastService } from 'src/app/shared/services/toast.service';
import { RegisterCommand } from 'src/app/models/register-command';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  form: FormGroup;
  visible = false;
  visible2 = false;

  constructor(
    private authService: AuthService,
    private toast: ToastService,
    private router: Router,
    public formBuilder: FormBuilderService) {
    this.builForm();
    this.form = formBuilder.formGroup;
  }

  register() {
    const registerCommand: RegisterCommand = this.form.value;
    if (registerCommand.contrasena !== registerCommand.contrasenaRepetida) {
      this.toast.showError('Contraseñas invalidas', 'Las contraseñas no coinciden')
    } else {
      this.authService.register(registerCommand).subscribe({
        next: () => {
          this.router.navigateByUrl('/')
          this.toast.showSuccess('Registro Exitoso', '');
        },
        error: (err) => this.toast.showError('Registro fallido', err.message)
      });
    }
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
        id: 'contrasena',
        name: 'Contraseña',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            validationKey: 'pattern',
            pattern: '^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$',
            validationMessage: 'Al menos 8 caracteres, incluyendo una mayúscula, una minúscula, un número o un carácter especial'
          }
        ]
      },
      {
        id: 'contrasenaRepetida',
        name: 'Contraseña Repetida',
        formState: '',
        validatorOrOpts: [
          {
            required: true
          }
        ]
      },

    ];

    this.formBuilder.initFormGroup();
  }
}
