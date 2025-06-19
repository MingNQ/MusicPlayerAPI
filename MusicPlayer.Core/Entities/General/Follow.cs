using MusicPlayer.Core.Enums;

namespace MusicPlayer.Core.Entities.General;

public class Follow
{
    public Guid Id { get; set; } = default!;

    public Guid FollowerId { get; set; } = default!;
    public Guid TargetId { get; set; } = default!;
    public User Follower { get; set; } = default!;
    public FollowType TargetType { get; set; }
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
