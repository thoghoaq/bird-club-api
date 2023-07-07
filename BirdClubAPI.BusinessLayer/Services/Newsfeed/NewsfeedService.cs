using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Newsfeed;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Blog;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Blog;
using BirdClubAPI.Domain.Entities;
using BirdClubAPI.Domain.DTOs.Response.Comment;
using BirdClubAPI.Domain.DTOs.Request.Newsfeed.Comment;
using BirdClubAPI.DataAccessLayer.Repositories.Comment;
using BirdClubAPI.DataAccessLayer.Repositories.Like;

namespace BirdClubAPI.BusinessLayer.Services.Newsfeed
{
    public class NewsfeedService : INewsfeedService
    {
        private readonly INewsfeedRepository _newsfeedRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;

        public NewsfeedService(INewsfeedRepository newsfeedRepository, IMapper mapper, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            _newsfeedRepository = newsfeedRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
        }

        public KeyValuePair<MessageViewModel, BlogViewModel?> CreateBlog(CreateBlogRequestModel requestModel)
        {
            Domain.Entities.Newsfeed blog = new Domain.Entities.Newsfeed
            {
                OwnerId = requestModel.OwnerId,
                PublicationTime = DateTime.UtcNow.AddHours(7),
                Blog = new Blog
                {
                    Content = requestModel.Content,
                    Title = requestModel.Title,
                },
            };
            var result = _newsfeedRepository.Create(blog);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                    new MessageViewModel
                    {
                        Message = "Internal error, can not post this blog",
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    }, null
                    );
            }
            return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                new MessageViewModel
                {
                    Message = string.Empty,
                    StatusCode = System.Net.HttpStatusCode.OK,
                },
                _mapper.Map<BlogViewModel>(_newsfeedRepository.GetBlogs(result.Id))
                );
        }

        public KeyValuePair<MessageViewModel, BlogViewModel?> GetBlog(int id)
        {
            var blog = _newsfeedRepository.GetBlogs(id);
            if (blog == null)
            {
                return new KeyValuePair<MessageViewModel, BlogViewModel?>
                    (
                        new MessageViewModel
                        {
                            StatusCode = System.Net.HttpStatusCode.NotFound,
                            Message = "Not found this blog"
                        },
                        null
                    );
            }
            var response = _mapper.Map<BlogViewModel>(blog);
            return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );

        }

        public NewsfeedViewModel GetNewsFeed(int memberid)
        {

            var newsfeed =_newsfeedRepository.GetNewsFeed(memberid);
            var response = new NewsfeedViewModel
            {
                Total = newsfeed.Count,
                Newsfeeds = newsfeed
            };
            return response;
        }

        public NewsfeedViewModel GetNewsfeeds(int page, int size, int? memberId = null)
        {
            var newsfeeds = _newsfeedRepository.GetNewsfeeds(page, size);
            if (newsfeeds != null)
            {
                foreach (var item in newsfeeds)
                {
                    if (item.Blog != null)
                    {
                        item.Blog.Comments = item.Blog.Comments.OrderByDescending(e => e.PublicationTime).ToList();
                        var likes = _likeRepository.GetLikes(item.Id);
                        item.Blog.LikeCount = likes.Count;
                        item.Blog.IsLiked = memberId != null && likes.Any(e => e.OwnerId == memberId);
                    }
                }
            }
            var response = new NewsfeedViewModel
            {
                Total = _newsfeedRepository.GetNewsFeedCount(),
                Newsfeeds = newsfeeds
            };
            return response;
        }

        public CommentRm? PostComment(NewsfeedCommentRequest request)
        {
            var newsfeed = _newsfeedRepository.GetNewsFeedById(request.NewsfeedId);
            var type = newsfeed?.Blog != null ? "BLOG" : "RECORD";
            Comment comment = new Comment
            {
                Content = request.Content,
                OwnerId = request.OwnerId,
                PublicationTime = DateTime.UtcNow.AddHours(7),
                ReferenceId = request.NewsfeedId,
                Type = type,
            };
            var result = _commentRepository.Create(comment);
            if (result != null)
            {
                return _mapper.Map<CommentRm>(result);
            } else
            {
                throw new InvalidDataException("Create comment fail");
            }
        }

        public MessageViewModel PostLiked(int memberId, int newsfeedId)
        {
            var alreadyLiked = _newsfeedRepository.GetLike(memberId, newsfeedId);
            if (alreadyLiked == null)
            {
                var like = _newsfeedRepository.PostLiked(memberId, newsfeedId);
                if (like != null)
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Like blog success"
                    };
                }
                else
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "Error when like blog"
                    };
                }
            }
            else
            {
                bool unLiked = _newsfeedRepository.Unliked(alreadyLiked);
                if (unLiked)
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Unlike blog success"
                    };
                }
                else
                {
                    return new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = "Error when unlike blog"
                    };
                }
            }
        }



        public KeyValuePair<MessageViewModel, BlogViewModel?> UpdateBlog(int id, UpdateBlogRm request)
        {
            var blog = _newsfeedRepository.GetBlogs(id);
            if (blog == null)
            {
                return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Not found this blog"
                },
                null
                );
            }
            var entity = _mapper.Map<Blog>(blog);
            if (request.Title != null)
            {
                entity.Title = request.Title;
            }

            if (request.Content != null)
            {
                entity.Content = request.Content;
            }

            var result = _newsfeedRepository.UpdateBlog(entity);
            if (result == false)
            {
                return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = "Update blog fail"
                },
                null
                );
            }

            return new KeyValuePair<MessageViewModel, BlogViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                    Message = "Update blog success"
                },
                null
                );
        }
    }
}
