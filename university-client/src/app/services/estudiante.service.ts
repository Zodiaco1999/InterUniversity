import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Programa } from '../models/programa';
import { Estudiante } from '../models/estudiante';
import { DataTableServiceBase } from '../shared/models/data-table-service-base';
import { Observable } from 'rxjs/internal/Observable';
import { SearchResult } from '../shared/models/search-result';

@Injectable({
  providedIn: 'root'
})
export class EstudianteService extends DataTableServiceBase<Estudiante> {
  API_URL = `${environment.API_URL}/estudiante`;

  constructor(
    private http: HttpClient
  ) {
    super();
  }

  public getSearch(): Observable<SearchResult<Estudiante>> {
    return this.http.get<SearchResult<Estudiante>>(this.API_URL, {params: this.getHttpParams()});
  }

  getEstudiante(id: number) {
    return this.http.get<Estudiante>(`${this.API_URL}/${id}`);
  }

  create() {
    return this.http.post(this.API_URL, {});
  }

  update(estudiante: Estudiante) {
    return this.http.put(this.API_URL, estudiante);
  }

  getPrograma() {
    return this.http.get<Programa>(`${this.API_URL}/getprograma`);
  }

  delete(id: number) {
    return this.http.delete(`${this.API_URL}/${id}`);
  }
}
