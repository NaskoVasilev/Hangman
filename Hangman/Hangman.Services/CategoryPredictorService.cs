using Hangman.Shared.Prediction;
using Microsoft.ML;

namespace Hangman.Services
{
    public class CategoryPredictorService : ICategoryPredictorService
    {
        private const string ModelFile = "../WordsCategoryModel.zip";

        public string PredictCategory(string word)
        {
            var context = new MLContext();
            var model = context.Model.Load(ModelFile, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<WordModel, WordModelPrediction>(model);
            var prediction = predictionEngine.Predict(new WordModel { Word = word });
            return prediction.Category;
        }
    }
}

