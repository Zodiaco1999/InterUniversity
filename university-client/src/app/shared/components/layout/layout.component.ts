import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  router = inject(Router);
  date = new Date().getFullYear();

  mostrarMenu = true;

  activar(est: boolean) {
    this.mostrarMenu = est;
  }

  logout() {
    this.router.navigateByUrl('/');
  }
}
