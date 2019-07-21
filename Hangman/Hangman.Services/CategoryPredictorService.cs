using Hangman.Shared.Prediction;
using Microsoft.Extensions.ML;

namespace Hangman.Services
{
    public class CategoryPredictorService : ICategoryPredictorService
    {
        private readonly PredictionEnginePool<WordModel, WordModelPrediction> predictionEngine;

        public CategoryPredictorService(PredictionEnginePool<WordModel, WordModelPrediction> predictionEngine)
        {
            this.predictionEngine = predictionEngine;
        }

        public string PredictCategory(string word)
        {
            var prediction = predictionEngine.Predict(new WordModel { Word = word });
            return prediction.Category;
        }
    }
}

