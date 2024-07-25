using AutoMapper;
using BrainBoost_API.DTOs.video;
using BrainBoost_API.Models;
﻿using BrainBoost_API.Models;

namespace BrainBoost_API.Repositories.Inplementation
{
    public class VideoStateRepository : Repository<VideoState> , IVideoStateRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper mapper;

        public VideoStateRepository(ApplicationDbContext context ,IMapper mapper) : base(context)
        {
            this.Context = context;
            this.mapper = mapper;
        }
        public IEnumerable<VideoStateDTO>GetVideoState(IEnumerable<VideoState> videoStates, IEnumerable<Video> video)
        {
            // Step 1: Use the mapper to map common properties
            var videoDTOs = mapper.Map<IEnumerable<VideoStateDTO>>(video);

            // Step 2: Create a dictionary for quick lookup of VideoState by VideoId
            var videoStateDict = videoStates.ToDictionary(vs => vs.VideoId);

            // Step 3: Iterate through the mapped VideoStateDTOs and manually set the State property
            foreach (var videoDTO in videoDTOs)
            {
                if (videoStateDict.TryGetValue(videoDTO.Id, out var videoState))
                {
                    videoDTO.State = videoState.State;
                }
            }

            return videoDTOs;
        }
    }
}
