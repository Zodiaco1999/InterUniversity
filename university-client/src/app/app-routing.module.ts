import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { authGuard } from './shared/guards/auth.guard';
import { InscripcionProgramaComponent } from './components/inscripcion/inscripcion-programa/inscripcion-programa.component';
import { RegistroMateriasComponent } from './components/inscripcion/registro-materias/registro-materias.component';
import { EstudianteListComponent } from './components/inscripcion/estudiante-list/estudiante-list.component';
import { ClaseListComponent } from './components/clase/clase-list/clase-list.component';
import { ClaseViewComponent } from './components/clase/clase-view/clase-view.component';
import { UsuarioFormComponent } from './components/auth/usuario-form/usuario-form.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivateChild: [authGuard],
    children: [
      {
        path: '',
        component: InscripcionProgramaComponent
      },
      {
        path: 'usuario/:id',
        component: UsuarioFormComponent
      },
      {
        path: 'registromaterias',
        component: RegistroMateriasComponent
      },
      {
        path: 'estudiantesregistrados',
        component: EstudianteListComponent
      },
      {
        path: 'clases',
        component: ClaseListComponent
      },
      {
        path: 'clases/:materiaId/:profesorId',
        component: ClaseViewComponent
      }

    ]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
