import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Clase } from 'src/app/models/clase';
import { ClaseService } from 'src/app/services/clase.service';

@Component({
  selector: 'app-clase-view',
  templateUrl: './clase-view.component.html',
  styleUrls: ['./clase-view.component.scss']
})
export class ClaseViewComponent {
  clase: Clase = {};

  constructor(
    private route: ActivatedRoute,
    private claseService: ClaseService
  ) {}

  ngOnInit(): void {
    const materiaId = this.route.snapshot.paramMap.get('materiaId') ?? '';
    const profesorId = this.route.snapshot.paramMap.get('profesorId') ?? '';

    if (materiaId && profesorId) {
      this.claseService.getClase(+profesorId, +materiaId).subscribe(res => this.clase = res)
    }
  }

}
