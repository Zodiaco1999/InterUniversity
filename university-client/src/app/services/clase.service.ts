import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Clase } from '../models/clase';

@Injectable({
  providedIn: 'root'
})
export class ClaseService {
  API_URL = `${environment.API_URL}/clase`;

  constructor(
    private http: HttpClient
  ) { }

  create(clases: Clase[]) {
    return this.http.post(this.API_URL, clases);
  }

  getClases(estudianteId: number) {
    return this.http.get<Clase[]>(`${this.API_URL}/${estudianteId}`);
  }

  getClase(profesorId: number, materiaId: number) {
    return this.http.get<Clase>(`${this.API_URL}/${profesorId}/${materiaId}`);
  }
}
