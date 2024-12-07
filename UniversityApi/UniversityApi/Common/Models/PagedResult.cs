namespace UniversityApi.Common.Models;

public class PagedResult<T>
{
    public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
    public int RowsCount { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}