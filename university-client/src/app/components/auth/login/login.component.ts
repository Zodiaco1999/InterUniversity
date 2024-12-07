import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { LoginCommand } from 'src/app/models/login-command';
import { FormBuilderService } from 'src/app/shared/services/form-builder.service';
import { ToastService } from 'src/app/shared/services/toast.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  isLoading = false;
  visible = false;

  constructor(
    private authService: AuthService,
    private toast: ToastService,
    private router: Router,
    public formBuilder: FormBuilderService) {
    this.builForm();
    this.loginForm = formBuilder.formGroup;
  }

  login() {
    this.isLoading = true;
    const loginCommand: LoginCommand = this.loginForm.value;
    this.authService.login(loginCommand).subscribe({
      next: () =>  this.router.navigateByUrl('/'),
      error: (err) => {
        this.toast.showError('Autenticación erronea', err.message);
        this.isLoading = false;
      },
    });
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'numeroIdentificacion',
        name: 'Usuario',
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
            required: true
          }
        ]
      }
    ];

    this.formBuilder.initFormGroup();
  }
}
