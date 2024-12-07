import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgOptimizedImage } from '@angular/common'
import {
  FaIconLibrary,
  FontAwesomeModule,
} from '@fortawesome/angular-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { AppComponent } from './app.component';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { MenuComponent } from './shared/components/menu/menu.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { ToatsComponent } from './shared/components/toats/toats.component';
import '@angular/localize/init';
import { TokenInterceptor } from './shared/interceptors/token.interceptor';
import { InputComponent } from './shared/components/input/input.component';
import { PaginationComponent }  from './shared/components/pagination/pagination.component';
import { InscripcionProgramaComponent } from './components/inscripcion/inscripcion-programa/inscripcion-programa.component';
import { RegistroMateriasComponent } from './components/inscripcion/registro-materias/registro-materias.component';
import { EstudianteListComponent } from './components/inscripcion/estudiante-list/estudiante-list.component';
import { ClaseListComponent } from './components/clase/clase-list/clase-list.component';
import { ClaseViewComponent } from './components/clase/clase-view/clase-view.component';
import { UsuarioFormComponent } from './components/auth/usuario-form/usuario-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HeaderComponent,
    MenuComponent,
    LoginComponent,
    RegisterComponent,
    ToatsComponent,
    InputComponent,
    PaginationComponent,
    InscripcionProgramaComponent,
    RegistroMateriasComponent,
    EstudianteListComponent,
    ClaseListComponent,
    ClaseViewComponent,
    UsuarioFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    FontAwesomeModule,
    MatTooltipModule,
    MatMenuModule,
    MatCardModule,
    NgOptimizedImage
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    library.addIconPacks(fas);
    library.addIconPacks(fab);
  }
}
