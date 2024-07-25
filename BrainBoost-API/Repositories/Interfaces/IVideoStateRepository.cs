using BrainBoost_API.DTOs.video;
using BrainBoost_API.Models;


namespace BrainBoost_API.Repositories.Inplementation
{
    public interface IVideoStateRepository : IRepository<VideoState>
    {
        public IEnumerable<VideoStateDTO> GetVideoState(IEnumerable<VideoState> videoState, IEnumerable<Video> video);
    }
}
