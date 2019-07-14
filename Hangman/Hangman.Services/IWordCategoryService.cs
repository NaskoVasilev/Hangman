using Hangman.Models;
using Hangman.Shared.InputModels.WordCategory;
using Hangman.Shared.ResponseModels.WordCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangman.Services
{
    public interface IWordCategoryService
    {
        Task<WordCategory> Create(WordCategoryCreateInputModel model);

        IEnumerable<WordCategoryResponseModel> All();

        string GetNameById(int id);
    }
}
