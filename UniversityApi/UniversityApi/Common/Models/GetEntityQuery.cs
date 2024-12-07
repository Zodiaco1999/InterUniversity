namespace UniversityApi.Common.Models;

public class GetEntityQuery
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
