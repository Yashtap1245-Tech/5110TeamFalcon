using System;
using Azure;
using Azure.AI.TextAnalytics;
using ContosoCrafts.WebSite.Services;
using NUnit.Framework;

namespace UnitTests.Services
{
    [TestFixture]
    public class SentimentAnalysisServiceTests
    {

        [Test]
        public void SentimentAnalysis_Should_Return_Sentiment_And_PositiveScore()
        {
            // Arrange
            
            string languageKey = "C1ZJKTGttyHCfv0P1XVIvk2NIX1J0N489hYghZfq0EleEsJRteINJQQJ99AKACYeBjFXJ3w3AAAaACOGm1I4";
            string languageEndpoint = "https://sentimentanalysisforfalcon.cognitiveservices.azure.com/";

            AzureKeyCredential credentials = new AzureKeyCredential(languageKey);
            Uri endpoint = new Uri(languageEndpoint);

            var client = new TextAnalyticsClient(endpoint, credentials);
            SentimentAnalysisService saService = new SentimentAnalysisService();

            var newComment = "The movie is really good";
            (var sentimentType, var positive) = saService.SentimentAnalysis(client, newComment);

            // Assert
            Assert.That(sentimentType, Is.EqualTo("Positive"));
            Assert.That(positive, Is.GreaterThan(50)); // 80% positive + 10% neutral / 2 = 90%
        }
    }
}
