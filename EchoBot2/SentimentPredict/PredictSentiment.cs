using EchoBot2ML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot2.SentimentPredict
{
    public class PredictSentiment
    {
        private string _sentiment;
        public PredictSentiment(string sentiment)
        {
            _sentiment = sentiment;
        }
        public string Predict()
        {
            // Add input data
            var input = new ModelInput
            {
                Sentiment_Text = _sentiment,
            };

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);

            return result.Prediction;

        }
        

    }
}
