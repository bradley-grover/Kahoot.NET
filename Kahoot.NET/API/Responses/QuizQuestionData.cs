namespace Kahoot.NET.API.Responses;

#nullable disable

public class QuizQuestionData
{
    [JsonPropertyName("video")]
    public Video Video { get; set; }

    [JsonPropertyName("gameBlockIndex")]
    public int GameBlockIndex { get; set; }

    [JsonPropertyName("totalGameBlockCount")]
    public int TotalGameBlockCount { get; set; }

    [JsonPropertyName("layout")]
    public string Layout { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("timeRemaining")]
    public int TimeRemaining { get; set; }

    [JsonPropertyName("timeAvailable")]
    public int TimeAvailable { get; set; }

    [JsonPropertyName("numberOfAnswersAllowed")]
    public int NumberOfAnswersAllowed { get; set; }

    [JsonPropertyName("currentQuestionAnswerCount")]
    public int CurrentQuestionAnswerCount { get; set; }

    [JsonPropertyName("numberOfChoices")]
    public int NumberOfChoices { get; set; }

    [JsonPropertyName("questionIndex")]
    public int QuestionIndex { get; set; }

    [JsonPropertyName("timeLeft")]
    public int TimeLeft { get; set; }

    [JsonIgnore] // used from the result of parsing Type
    public QuestionType QuestionType { get; set; }
}
