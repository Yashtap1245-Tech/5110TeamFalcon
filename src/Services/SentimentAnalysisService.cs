using System;
using System.Collections.Generic;
using Azure;
using Azure.AI.TextAnalytics;

namespace ContosoCrafts.WebSite.Services;

public class SentimentAnalysisService
{
    
        // This example requires environment variables named "LANGUAGE_KEY" and "LANGUAGE_ENDPOINT"
        // static string languageKey = Environment.GetEnvironmentVariable("C1ZJKTGttyHCfv0P1XVIvk2NIX1J0N489hYghZfq0EleEsJRteINJQQJ99AKACYeBjFXJ3w3AAAaACOGm1I4");
        // static string languageEndpoint = Environment.GetEnvironmentVariable("https://sentimentanalysisforfalcon.cognitiveservices.azure.com/");
        

        // Example method for detecting opinions text. 
        // static void SentimentAnalysisWithOpinionMiningExample(TextAnalyticsClient client)
        public (string, double, double, double) SentimentAnalysis(TextAnalyticsClient client, string str)
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

            // foreach (AnalyzeSentimentResult review in reviews)
            // {
            string sentiment = reviews[0].DocumentSentiment.Sentiment.ToString();
            double positive = reviews[0].DocumentSentiment.ConfidenceScores.Positive;
            double negative = reviews[0].DocumentSentiment.ConfidenceScores.Negative;
            double neutral = reviews[0].DocumentSentiment.ConfidenceScores.Neutral;
            return (sentiment, positive, negative, neutral);
                // Console.WriteLine($"Document sentiment: {review.DocumentSentiment.Sentiment}\n");
                // Console.WriteLine($"\tPositive score: {review.DocumentSentiment.ConfidenceScores.Positive:0.00}");
                // Console.WriteLine($"\tNegative score: {review.DocumentSentiment.ConfidenceScores.Negative:0.00}");
                // Console.WriteLine($"\tNeutral score: {review.DocumentSentiment.ConfidenceScores.Neutral:0.00}\n");
                // foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences)
                // {
                //     Console.WriteLine($"\tText: \"{sentence.Text}\"");
                //     Console.WriteLine($"\tSentence sentiment: {sentence.Sentiment}");
                //     Console.WriteLine($"\tSentence positive score: {sentence.ConfidenceScores.Positive:0.00}");
                //     Console.WriteLine($"\tSentence negative score: {sentence.ConfidenceScores.Negative:0.00}");
                //     Console.WriteLine($"\tSentence neutral score: {sentence.ConfidenceScores.Neutral:0.00}\n");
                //
                //     foreach (SentenceOpinion sentenceOpinion in sentence.Opinions)
                //     {
                //         Console.WriteLine($"\tTarget: {sentenceOpinion.Target.Text}, Value: {sentenceOpinion.Target.Sentiment}");
                //         Console.WriteLine($"\tTarget positive score: {sentenceOpinion.Target.ConfidenceScores.Positive:0.00}");
                //         Console.WriteLine($"\tTarget negative score: {sentenceOpinion.Target.ConfidenceScores.Negative:0.00}");
                //         foreach (AssessmentSentiment assessment in sentenceOpinion.Assessments)
                //         {
                //             Console.WriteLine($"\t\tRelated Assessment: {assessment.Text}, Value: {assessment.Sentiment}");
                //             Console.WriteLine($"\t\tRelated Assessment positive score: {assessment.ConfidenceScores.Positive:0.00}");
                //             Console.WriteLine($"\t\tRelated Assessment negative score: {assessment.ConfidenceScores.Negative:0.00}");
                //         }
                //     }
                // }
                // Console.WriteLine($"\n");
            // }

            // return ("No Value", 0, 0, 0);
        }

        // static void Main(string[] args)
        // {
        //     var client = new TextAnalyticsClient(endpoint, credentials);
        //     SentimentAnalysis(client);
        //
        //     Console.Write("Press any key to exit.");
        //     Console.ReadKey();
        // }
}