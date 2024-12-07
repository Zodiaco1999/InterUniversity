import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
  HttpErrorResponse
} from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CustomErrorResponse } from '../models/custom-error-response';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(public authService: AuthService, private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const jwt = this.authService.getJwtToken()
    if (jwt) {
      request = this.addToken(request, jwt);
    }

    return next.handle(request)
      .pipe(catchError(error => {
        // if (error instanceof HttpErrorResponse && error.status === 401) {
        //   return this.handle401Error(request, next);
        // }
        return throwError(() => error.error as CustomErrorResponse)
      }));
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  // private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
  //   if (this.authService.expirationToken()) {
  //     return this.handleLogout();
  //   }

  //   return next.handle(request)
  //     .pipe(catchError(error => {
  //       if (error instanceof HttpErrorResponse && error.status === 401) {
  //         return this.handleLogout();
  //       }
  //       return throwError(() => error.error as CustomErrorResponse)
  //     }));
  // }

  handleLogout() {
    this.authService.logout();
    return throwError(() => Error('Su sesión expiro, inicie sesión nuevamente'));
  }

  protected normalizeRequestHeaders(request: HttpRequest<any>): HttpRequest<any> {
    let modifiedHeaders = new HttpHeaders();
    modifiedHeaders = request.headers.set('Cache-Control', 'no-cache')
      .set('Expires', 'Sat, 01 Jan 2000 00:00:00 GMT');
    return request.clone({
      headers: modifiedHeaders
    });
  }
}
