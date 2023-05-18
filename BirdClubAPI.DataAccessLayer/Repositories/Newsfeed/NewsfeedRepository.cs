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

        public List<NewsfeedResponseModel> GetNewsfeeds(int limit, int page, int size)
        {
            var response = _mapper.ProjectTo<NewsfeedResponseModel>(_context.Newsfeeds
                .Take(limit > 0 ? limit : int.MaxValue)
                .Skip(page > 0 ? (page - 1) * size: 0)
                .Take(size)
                .OrderByDescending(e => e.PublicationTime)
                .Include(e => e.Owner)
                    .ThenInclude(e => e.User)
                .Include(e => e.Blog)
                .Include(e => e.Record)
                    .ThenInclude(e => e.Bird)
                )
                .ToList();
            return response;
        }
    }
}
