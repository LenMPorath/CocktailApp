using System.Threading.Tasks;
using CocktailApp.Models;
using CocktailAppBackend;
using Microsoft.EntityFrameworkCore;

namespace CocktailAppBackend.Services
{
    public interface ITagService
    {
        Task AddTagAsync(string title);
        Task UpdateTagAsync(int id, string newTitle);
        Task DeleteTagAsync(int id);
        Task<List<ATag>> GetAllTagsAsync();
        Task<ATag?> GetTagAsync(int id);
    }
    public class TagService : ITagService
    {
        private readonly CocktailAppDBContext _dbContext;

        public TagService(CocktailAppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddTagAsync(string title)
        {
            var tag = new Tag { Title = title };
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(int id, string newTitle)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag == null) {
                throw new ArgumentException($"Tag with id {id} not found");
            }
            tag.Title = newTitle;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int id)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                throw new Exception($"Tag with ID {id} wasn't found!");
            }
            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ATag>> GetAllTagsAsync()
        {
            var allTags = await _dbContext.Tags.ToListAsync();
            var result = new List<ATag>();

            foreach (var tag in allTags)
            {
                var aTag = new ATag
                {
                    Id = tag.Id,
                    Title = tag.Title,
                    RecipeList = new List<int>(),
                };

                if (tag.Recipes != null && tag.Recipes.Any())
                {
                    foreach (var recipe in tag.Recipes)
                    {
                        aTag.RecipeList.Add(recipe.Id);
                    }
                }
                result.Add(aTag);
            }
            return result;
        }

        public async Task<ATag?> GetTagAsync(int id)
        {
            var tag = await _dbContext.Tags.FindAsync(id);
            if (tag == null)
            {
                throw new Exception($"Tag with ID {id} wasn't found!");
            }
            var aTag = new ATag
            {
                Id = tag.Id,
                Title = tag.Title,
                RecipeList = new List<int>(),
            };

            if (tag.Recipes != null && tag.Recipes.Any())
            {
                foreach (var recipe in tag.Recipes)
                {
                    aTag.RecipeList.Add(recipe.Id);
                }
            }

            return aTag;
        }
    }
}
