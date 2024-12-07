import { Component, OnInit } from '@angular/core';
import { Clase } from 'src/app/models/clase';
import { AuthService } from 'src/app/services/auth.service';
import { ClaseService } from 'src/app/services/clase.service';
import { ToastService } from 'src/app/shared/services/toast.service';

@Component({
  selector: 'app-clase-list',
  templateUrl: './clase-list.component.html',
  styleUrls: ['./clase-list.component.scss']
})
export class ClaseListComponent implements OnInit{
  clases: Clase[] = [];

  constructor(
    private toastService: ToastService,
    private authService: AuthService,
    private claseService: ClaseService
  ) {}

  ngOnInit(): void {
    const id  = this.authService.getUserId();

    this.claseService.getClases(id).subscribe({
      next: (res) => this.clases = res,
      error: (err) => this.toastService.showError('Erro consultando clases', err.message)
    })
  }
}
