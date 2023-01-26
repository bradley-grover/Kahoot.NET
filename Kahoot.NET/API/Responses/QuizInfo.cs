namespace Kahoot.NET.API.Responses;

internal struct QuizInfo
{
    [JsonPropertyName("quizInfo")]
    public string Title { get; set; }
}
