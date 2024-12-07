import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { LoginCommand } from '../models/login-command';
import { LoginResponse } from '../models/login-response';
import { tap } from 'rxjs/internal/operators/tap';
import { map } from 'rxjs/internal/operators/map';
import { Router } from '@angular/router';
import { RegisterCommand } from '../models/register-command';
import { UserLogin } from '../models/user-login';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  API_URL = `${environment.API_URL}/auth`;
  private readonly JWT_TOKEN = 'JWT_TOKEN';
  private readonly USER = 'USER';
  myDataVisible$ = new BehaviorSubject<boolean>(false);

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  login(loginCommand: LoginCommand) {
    return this.http.post<LoginResponse>(this.API_URL, loginCommand)
      .pipe(
        tap(response => this.storeTokens(response))
      );
  }

  register(registerCommand: RegisterCommand) {
    return this.http.post<LoginResponse>(`${this.API_URL}/register`, registerCommand)
    .pipe(
      tap(response => this.storeTokens(response))
    );
  }

  private storeTokens(loginRepsonse: LoginResponse) {
    const user = JSON.stringify(loginRepsonse.user);
    localStorage.setItem(this.JWT_TOKEN, loginRepsonse.accessToken);
    localStorage.setItem(this.USER, user);
  }

  getJwtToken() {
    const jwt = localStorage.getItem(this.JWT_TOKEN);
    return jwt ?? '';
  }

  getUserName(): string {
    const user = localStorage.getItem(this.USER);
    return user ? JSON.parse(user).nombre : '';
  }

  getUserId(): number {
    const user = localStorage.getItem(this.USER);
    return user ? JSON.parse(user).usuarioId : '';
  }

  logout() {
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }

  isLoggedIn() {
    const jwt = localStorage.getItem(this.JWT_TOKEN);
    return !!jwt;
  }
}
