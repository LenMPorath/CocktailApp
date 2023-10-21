using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public class TagService
    {
        private readonly CocktailAppDBContext _dbContext;

        public async Task<Tag> AddTagAsync(string title)
        {
            var tag = new Tag { Title = title };
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> UpdateTagAsync(int id, string newTitle)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag != null)
            {
                tag.Title = newTitle;
                await _dbContext.SaveChangesAsync();
            }
            return tag;
        }

        public async Task DeleteTagAsync(int id)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagAsync(int id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }
    }
}
