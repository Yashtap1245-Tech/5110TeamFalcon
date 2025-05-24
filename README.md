# Falcon Movie Management Web Application

A web interface for managing and discovering movies with advanced features including sentiment analysis, voting, and comprehensive search capabilities.

## 🎬 Features

- **Movie Grid Display**: Clean, responsive grid layout showcasing movie collections
- **Advanced Search**: Find movies quickly with intelligent search functionality
- **Filtering System**: Filter movies by various criteria (genre, year, rating, etc.)
- **CRUD Operations**: Complete Create, Read, Update, Delete functionality for movie management
- **Dark Mode**: Toggle between light and dark themes for better user experience
- **Comment System**: Users can leave comments on movies
- **Sentiment Analysis**: AI-powered sentiment analysis for movie comments using Azure Cognitive Services
- **Voting System**: Rate and vote on movies
- **Responsive Design**: Optimized for desktop and mobile devices

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 7.0
- **Frontend**: Razor Pages with modern CSS/JavaScript
- **AI Services**: Azure.AI.TextAnalytics for sentiment analysis
- **Testing**: 
  - bUnit for Razor component testing
  - NUnit for unit testing
  - Integration testing implementation
- **Mocking**: Moq framework for TextAnalytics service mocking

## 📋 Prerequisites

Before running this application, ensure you have:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- Azure Cognitive Services account (for sentiment analysis features)

## 🚀 Getting Started

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Yashtap1245-Tech/5110TeamFalcon.git
   cd 5110TeamFalco
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure Azure Text Analytics**
   - Create an Azure Cognitive Services resource
   - Update `appsettings.json` with your Azure credentials:
   ```json
   {
     "AzureTextAnalytics": {
       "Endpoint": "your-azure-endpoint",
       "ApiKey": "your-api-key"
     }
   }
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Development Setup

```bash
# Install development tools
dotnet tool install --global dotnet-ef

# Build the project
dotnet build

# Run in development mode
dotnet run --environment Development
```

## 🧪 Testing

Can also use Rider or Visual Studio IDE to run it. 

The application includes comprehensive testing coverage:

### Running Unit Tests
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Test Structure
- **Unit Tests**: NUnit framework for business logic testing
- **Component Tests**: bUnit for Razor component testing
- **Integration Tests**: Full application workflow testing
- **Mocking**: Moq framework for external service dependencies

### Test Categories
```bash
# Run only unit tests
dotnet test --filter Category=Unit

# Run only integration tests
dotnet test --filter Category=Integration

# Run component tests
dotnet test --filter Category=Component
```

## 🎯 Key Components

### Movie Management
- **Grid View**: Responsive movie display with thumbnails
- **Search**: Real-time search across movie titles, genres, and descriptions
- **Filters**: Multi-criteria filtering system
- **CRUD Operations**: Full movie lifecycle management

### User Interaction
- **Comments**: User feedback system with sentiment analysis
- **Voting**: Rating system for movies
- **Dark Mode**: Theme switching capability

### AI Integration
- **Sentiment Analysis**: Azure Text Analytics integration for comment sentiment scoring
- **Smart Insights**: AI-powered movie recommendations based on user interactions
```
