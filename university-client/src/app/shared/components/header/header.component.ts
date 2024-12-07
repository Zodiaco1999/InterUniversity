import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { EstudianteService } from 'src/app/services/estudiante.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  mostrar = true;
  islogged = false;
  myDataVisible = false;

  constructor(
    public authService: AuthService,
    private estudianteService: EstudianteService
    )
  {}

  ngOnInit() {
    this.islogged = this.authService.isLoggedIn();
    this.estudianteService.getEstudiante(this.authService.getUserId()).subscribe
      (() => this.myDataVisible = true);
    this.authService.myDataVisible$
      .subscribe((reslut) => this.myDataVisible = reslut);
  }

  @Output() mostrarMenu: EventEmitter<boolean> = new EventEmitter<boolean>();

  activar(est: boolean) {
    this.mostrarMenu.emit(est);
  }

  logout() {
    this.authService.logout();
  }
}
