using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
           return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
        {
           var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
           var likes = _context.Likes.AsQueryable();

           if(likesParams.Predicate == "liked")
           {
               // will give us the information of the list of users that are liked by the currently logged in user
               //currentlu logged in user has liked
               likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
               users = likes.Select(like => like.LikedUser);
           }

           if(likesParams.Predicate == "likedBy")
           {
               // will give the information of the list of users that have liked the currently logged in user
               likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
               users = likes.Select(like => like.SourceUser);
           }

           var likedUsers =  users.Select(user => new LikeDto
           {

               Username = user.UserName,
               KnownAs =  user.KnownAs,
               Age = user.DateOfBirth.CalculateAge(),
               PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
               City = user.City,
               Id = user.Id
           });
           return await PagedList<LikeDto>
                        .CreateAsync(likedUsers, likesParams.PageNumber,likesParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                    .Include(x => x.LikedUsers)
                    .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}