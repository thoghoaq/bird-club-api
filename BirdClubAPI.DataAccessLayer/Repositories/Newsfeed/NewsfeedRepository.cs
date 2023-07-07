using AutoMapper;
using AutoMapper.QueryableExtensions;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.Domain.DTOs.Response.Blog;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.Response.Newsfeed;
using BirdClubAPI.Domain.Entities;
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

        public Domain.Entities.Newsfeed? Create(Domain.Entities.Newsfeed newsfeed)
        {
            try
            {
                var row = _context.Add(newsfeed);
                _context.SaveChanges();
                return row.Entity;
            }
            catch
            {
                return null;
            }
        }

        public Like? GetBlog(int memberId, int newsfeedId)
        {
            var request = _context.Likes.FirstOrDefault(e => e.Owner.UserId == memberId && e.ReferenceNavigation.NewsfeedId == newsfeedId);
            return request;
        }

        public BlogDetailResponseModel? GetBlogs(int id)
        {
            var response = _context.Newsfeeds
                .Where(x => x.Id == id)
                .Select(e => new BlogDetailResponseModel
                {
                    Id = e.Id,
                    PublicationTime = e.PublicationTime,
                    Title = e.Blog != null ? e.Blog.Title : string.Empty,
                    Content = e.Blog != null ? e.Blog.Content : null,
                    Owner = new MemberResponseModel
                    {
                        Avatar = e.Owner.Avatar,
                        DisplayName = e.Owner.User.DisplayName,
                        MemberId = e.OwnerId
                    }
                }).FirstOrDefault();
            return response;
        }

        public List<NewsfeedResponseModel> GetNewsFeed(int memberid)
        {
            var response = _context.Newsfeeds
                .Where(e => e.OwnerId == memberid)
                .Include(e => e.Owner)
                    .ThenInclude(e => e.User)
                .Include(e => e.Blog)
                .Include(e => e.Record)
                    .ThenInclude(e => e.Bird)  
                   .OrderBy(e => e.Id)
                .ToList();
            return _mapper.Map<List<NewsfeedResponseModel>>(response);
        }

        public int GetNewsFeedCount()
        {
            return _context.Newsfeeds.Count();
        }

        public List<NewsfeedResponseModel> GetNewsfeeds(int page, int size)
        {
            var response = _mapper.ProjectTo<NewsfeedResponseModel>(_context.Newsfeeds
                .Skip(page > 0 ? (page - 1) * size : 0)
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

        public Like? PostLiked(int memberId, int newsfeedId)
        {
            try
            {
                var result = _context.Likes.Add(new Like
                {
                    OwnerId = memberId,
                    ReferenceId = newsfeedId,
                    Type  = "Like"
                 
                });
                _context.SaveChanges();
                return result.Entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Unliked(Like alreadyLiked)
        {
            try
            {
                var result = _context.Likes.Remove(alreadyLiked);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateBlog(Blog blog)
        {
            try
            {
                _context.Update(blog);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

       
    }
}
