using System.Collections.Generic;
using System.Threading.Tasks;
using Hangman.Models;
using Hangman.Models.Enums;
using Hangman.Shared.InputModels.Word;

namespace Hangman.Services
{
	public interface IWordService
	{
		string GetRandomWord(WordDifficulty wordDifficulty, int categoryId);

        Task<Word> Create(WordCreateInputModel model);

        Task<Word> Edit(WordEditInputModel model);

        WordEditResponseModel GetWordWithAllCategories(int id);

        IEnumerable<WordResponseModel> GetAllOrderedByCategoryThenByDifficulty();

        Task UploadWords(string[] words);
    }
}
