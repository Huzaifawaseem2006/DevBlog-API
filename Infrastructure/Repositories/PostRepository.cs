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
        public async Task<Post> GetByIdAsync(Guid id)
        {
            return await _context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.Author).ToListAsync();

        }

        public async Task AddAsync(Post entity)
        {
            await _context.Posts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post entity)
        {
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post entity)
        {
            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetPostsByAuthorAsync(Guid authorId)
        {
            return await _context.Posts
                        .Include(p => p.Author)
                        .Where(p => p.AuthorId == authorId)
                        .ToListAsync();
        }

        public async Task<PagedResult<Post>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize)
        {
            var totalPosts = await _context.Posts.CountAsync();
            var posts = await _context.Posts
                                .Include(p => p.Author)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            return new PagedResult<Post>
            {
                Items = posts,
                TotalRecords = totalPosts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalPosts / pageSize)
            };
        }
    }
}
