namespace MusicPlayer.Api.Models;

public class PaginationRequest(int page = 1, int pageSize = 10)
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}
