using AutoMapper;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BirdClubAPI.DataAccessLayer.Repositories.Newsfeed
{
    public class NewsfeedRepository : INewsfeedRepository
    {
        private readonly BirdClubContext _context;
        private readonly IMapper _mapper;

        public NewsfeedRepository(BirdClubContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<NewsfeedResponseModel> GetNewsfeeds()
        {
            var response = _mapper.ProjectTo<NewsfeedResponseModel>(_context.Newsfeeds
                .Include(e => e.Owner)
                    .ThenInclude(e => e.User)
                .Include(e => e.Blog)
                .Include(e => e.Record)
                    .ThenInclude(e => e.Bird)).ToList();
            return response;
        }
    }
}
