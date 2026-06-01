using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using DevBlog.Core.Helpers;
using DevBlog.Core.Interfaces;

using DevBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;
        public PostRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Post> GetByIdAsync(Guid id, CancellationToken token)
        {
            return await _context.Posts
                         .Include(p => p.Author)
                         .Include(p => p.Tags)
                         .Include(p => p.LikedByUsers)
                         .FirstOrDefaultAsync(p => p.Id == id, token);
        }

        public async Task<IEnumerable<Post>> GetAllAsync(CancellationToken token)
        {
            return await _context.Posts
                         .Include(p => p.Author)
                         .Include(p => p.Tags)
                         .ToListAsync(token);

        }

        public async Task AddAsync(Post entity, CancellationToken token)
        {
            await _context.Posts.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(Post entity, CancellationToken token)
        {
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(Post entity, CancellationToken token)
        {
            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync(token);
        }

        public async Task<List<Post>> GetPostsByAuthorAsync(Guid authorId, CancellationToken token)
        {
            return await _context.Posts
                        .Include(p => p.Author)
                        .Include(p => p.Tags)
                        .Where(p => p.AuthorId == authorId)
                        .ToListAsync(token);
        }

        public async Task<PagedResult<Post>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize, CancellationToken token)
        {
            var totalPosts = await _context.Posts.CountAsync(token);
            var posts = await _context.Posts
                                .Include(p => p.Author)
                                .Include(p => p.Tags)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(token);
            return new PagedResult<Post>
            {
                Items = posts,
                TotalRecords = totalPosts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize)
            };
        }

        public async Task<List<Post>> SearchPostsAsync(string searchTerm, CancellationToken token)
        {
            return await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Tags)
                .Where(p => EF.Functions.Like(p.Title, $"%{searchTerm}%") || EF.Functions.Like(p.Content, $"%{searchTerm}%"))
                .ToListAsync(token);
        }

        public async Task LikePostAsync(Post post, ApplicationUser user, CancellationToken token)
        {
            post.LikedByUsers.Add(user);
            await _context.SaveChangesAsync(token);

        }

        public async Task UnlikePostAsync(Post post, ApplicationUser user, CancellationToken token)
        {
            post.LikedByUsers.Remove(user);
            await _context.SaveChangesAsync(token);

        }

    }
}
