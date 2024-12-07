import { HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Subject } from 'rxjs/internal/Subject';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/internal/operators/tap';
import { debounceTime } from 'rxjs/internal/operators/debounceTime';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import { inject } from '@angular/core';
import { ToastService } from '../services/toast.service';
import { SearchState } from './search-state';
import { SearchResult } from './search-result';

export abstract class DataTableServiceBase<T> {
  toast = inject(ToastService);

  public displayedColumns: string[] = [];

  protected _searchState: SearchState = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };

  protected httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  protected _loading$ = new BehaviorSubject<boolean>(true);
  public _search$ = new Subject<void>();
  protected _data$ = new BehaviorSubject<T[]>([]);
  protected _totalRecords$ = new BehaviorSubject<number>(0);

  public abstract getSearch(): Observable<SearchResult<T>>;

  get loading$() { return this._loading$.asObservable(); }
  get data$() { return this._data$.asObservable(); }
  get totalRecords$() { return this._totalRecords$.asObservable(); }

  constructor() {
    this.Init();
  }

  get page() { return this._searchState.page; }
  set page(page: number) { this._set({ page }); }
  get pageSize() { return this._searchState.pageSize; }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  get recordNumber() { return ((this._searchState.page - 1) * this._searchState.pageSize) + 1; }
  get recordMaxPage() {
    let recordMaxPage = ((this._searchState.page) * this._searchState.pageSize);
    recordMaxPage = recordMaxPage > this._totalRecords$.value ? this._totalRecords$.value : recordMaxPage;
    return recordMaxPage;
  }

  protected _set(patch: Partial<SearchState>) {
    Object.assign(this._searchState, patch);
    this._search$.next();
  }

  public Search() {
    this._search$.next();
  }

  protected Init() {
    this._search$.pipe(
      tap(() => this._loading$.next(true)),
      debounceTime(500),
      switchMap(() => this.getSearch()),
      tap(() => this._loading$.next(false)),
    ).subscribe({
      next: (result) => {
        this._data$.next(result.results);
        this._totalRecords$.next(result.rowsCount);
      },
      error: (err) => {
        this._loading$.next(false);
        this.toast.showError('Error en la consulta', err.message);
      }
    });
  }

  getHttpParams() {
    let params = new HttpParams()
      .set('pagina', this.page)
      .set('registrosPorPagina', this.pageSize)

    return params;
  }
}


