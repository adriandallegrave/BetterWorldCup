namespace Better.Application.Dtos;

public class SetResultDto
{
    public Guid GameId { get; set; }
    public int HomeScored { get; set; }
    public int AwayScored { get; set; }
}
