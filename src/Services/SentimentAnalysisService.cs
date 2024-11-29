using System;
using System.Collections.Generic;
using Azure;
using Azure.AI.TextAnalytics;

namespace ContosoCrafts.WebSite.Services;

public class SentimentAnalysisService
{
    
        // Method for detecting opinions text. 
        // static void SentimentAnalysisWithOpinionMiningExample(TextAnalyticsClient client)
        public (string, int) SentimentAnalysis(TextAnalyticsClient client, string str)
        {
            string languageKey = "C1ZJKTGttyHCfv0P1XVIvk2NIX1J0N489hYghZfq0EleEsJRteINJQQJ99AKACYeBjFXJ3w3AAAaACOGm1I4";
            string languageEndpoint = "https://sentimentanalysisforfalcon.cognitiveservices.azure.com/";

            AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
            Uri endpoint = new Uri(languageEndpoint);
            
            var documents = new List<string>
            {
                // "The food and service were unacceptable. The concierge was nice, however."
                str
            };

            AnalyzeSentimentResultCollection reviews = client.AnalyzeSentimentBatch(documents, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            string sentiment = reviews[0].DocumentSentiment.Sentiment.ToString();
            double positive = reviews[0].DocumentSentiment.ConfidenceScores.Positive;
            double negative = reviews[0].DocumentSentiment.ConfidenceScores.Negative;
            double neutral = reviews[0].DocumentSentiment.ConfidenceScores.Neutral;
            
            Console.WriteLine($"Document sentiment: {sentiment}\n");
            Console.WriteLine($"\tPositive score: {positive:0.00}");
            Console.WriteLine($"\tNegative score: {negative:0.00}");
            Console.WriteLine($"\tNeutral score: {neutral:0.00}\n");
            
            // Calculation of Postive and Negetive score
            double mid = neutral / 2;
            int roundPositive = (int)Math.Round((positive + mid) * 100);
            int roundNegative = (int)Math.Round((negative + mid) * 100);
            Console.WriteLine($"Positive score: {roundPositive}");
            return (sentiment, roundPositive);
        }

}