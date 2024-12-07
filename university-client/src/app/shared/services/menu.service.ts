import { Injectable } from '@angular/core';
import { Module } from '../models/module';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  modules: Module[] = [];

  loadModules() {
    const inscripcion = new Module();
    inscripcion.id = 1;
    inscripcion.name = 'Inscripción';
    inscripcion.subName = '';
    inscripcion.active = false;
    inscripcion.open = false;
    inscripcion.iconPrefix = 'fas';
    inscripcion.iconName = 'check-square';
    inscripcion.options = [
      {
        id: 1,
        name: 'Inscripción programa',
        active: false,
        url: '/',
      },
      {
        id: 2,
        name: 'Inscribir materias',
        active: false,
        url: '/registromaterias',
      },
      {
        id: 3,
        name: 'Estudiantes inscritos',
        active: false,
        url: '/estudiantesregistrados',
      },
    ];

    this.addModule(inscripcion);

    const cursos = new Module();
    cursos.id = 2;
    cursos.name = 'Clases';
    cursos.subName = '';
    cursos.active = false;
    cursos.open = false;
    cursos.iconPrefix = 'fas';
    cursos.iconName = 'cubes';
    cursos.options = [
      {
        id: 1,
        name: 'Mis clases',
        active: false,
        url: '/clases',
      },
    ];

    this.addModule(cursos);
    return this.modules;
  }

  addModule(module: Module) {
    if (!this.modules.some(m => m.id === module.id)) {
      this.modules.push(module);
    }
  }
}
