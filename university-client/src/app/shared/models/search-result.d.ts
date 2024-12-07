export interface SearchResult<T> {
  results: T[];
  rowsCount: number;
  currentPage: number;
  pageCount: number;
  pageSize: number;
}
