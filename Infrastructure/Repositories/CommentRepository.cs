using DevBlog.Core.Entities;
using DevBlog.Core.Interfaces;
using DevBlog.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace DevBlog.Infrastructure.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _context.Comments.Include(c => c.Author).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.Author).ToListAsync();
        }

        public async Task AddAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment entity)
        {
            _context.Comments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment entity)
        {
            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _context.Comments
                        .Include(c => c.Author)
                        .Where(c => c.PostId == postId)
                        .ToListAsync();
        }

    }
}
