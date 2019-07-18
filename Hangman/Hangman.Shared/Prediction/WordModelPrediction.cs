using Microsoft.ML.Data;

namespace Hangman.Shared.Prediction
{
    public class WordModelPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Category { get; set; }
    }
}
