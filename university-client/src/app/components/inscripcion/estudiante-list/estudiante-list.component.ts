import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Estudiante } from 'src/app/models/estudiante';
import { EstudianteService } from 'src/app/services/estudiante.service';

@Component({
  selector: 'app-estudiante-list',
  templateUrl: './estudiante-list.component.html',
  styleUrls: ['./estudiante-list.component.scss']
})
export class EstudianteListComponent implements OnInit {
  data$: Observable<Estudiante[]>;

  constructor(public estudianteService: EstudianteService) {
    this.data$ = estudianteService.data$;
  }

  ngOnInit(): void {
    this.estudianteService.Search();
  }
}
