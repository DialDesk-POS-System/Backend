using DialDesk.Server.DTOs.Watch;

namespace DialDesk.Server.DTOs;

public class SearchGroupResultDto
{
    public string Type { get; set; }

    public WatchOutDto? Item { get; set; }

    public List<WatchOutDto>? Items { get; set; }

    public WatchOutDto? FirstItem { get; set; }

}
