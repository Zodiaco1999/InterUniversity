import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Materia } from '../models/materia';

@Injectable({
  providedIn: 'root'
})
export class MateriaService {
  API_URL = `${environment.API_URL}/materia`;

  constructor(
    private http: HttpClient
  ) { }

  getMaterias() {
    return this.http.get<Materia[]>(this.API_URL)
  }

}
