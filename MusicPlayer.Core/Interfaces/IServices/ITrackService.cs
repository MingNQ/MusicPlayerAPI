using MusicPlayer.Core.Entities.Business;

namespace MusicPlayer.Core.Interfaces.IServices;

public interface ITrackService
{
    Task<PaginationResponse<TrackViewModel>> GetAllTracksAsync(int page, int pageSize);
    Task<TrackViewModel> GetTrackByIdAsync(Guid trackId);
    Task<TrackViewModel> CreateTrackAsync(TrackCreateViewModel model);
    Task UpdateTrackAsync(Guid id, TrackUpdateViewModel model);
    Task DeleteTrackAsync(Guid id);
}
