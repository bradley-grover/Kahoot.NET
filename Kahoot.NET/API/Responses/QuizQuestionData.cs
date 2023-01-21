namespace Kahoot.NET.API.Responses;

/// <summary>
/// The question data received in the question message sent by the host
/// </summary>
public class QuizQuestionData
{
    /// <summary>
    /// The video possibly contained 
    /// </summary>
    [JsonPropertyName("video")]
    public Video? Video { get; set; }
#nullable disable

    /// <summary>
    /// The index in the quiz's game blocks
    /// </summary>
    [JsonPropertyName("gameBlockIndex")]
    public int GameBlockIndex { get; set; }

    /// <summary>
    /// The total game block amount
    /// </summary>
    [JsonPropertyName("totalGameBlockCount")]
    public int TotalGameBlockCount { get; set; }

    /// <summary>
    /// The layout of the question
    /// </summary>
    [JsonPropertyName("layout")]
    public string Layout { get; set; }

    /// <summary>
    /// The type of question
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// The amount of time remaining
    /// </summary>
    [JsonPropertyName("timeRemaining")]
    public int TimeRemaining { get; set; }

    /// <summary>
    /// The amount of time available
    /// </summary>
    [JsonPropertyName("timeAvailable")]
    public int TimeAvailable { get; set; }

    /// <summary>
    /// The number of answers allowed for the question
    /// </summary>
    [JsonPropertyName("numberOfAnswersAllowed")]
    public int NumberOfAnswersAllowed { get; set; }

    /// <summary>
    /// The current question answer count for the player
    /// </summary>
    [JsonPropertyName("currentQuestionAnswerCount")]
    public int CurrentQuestionAnswerCount { get; set; }

    /// <summary>
    /// The number of choices available
    /// </summary>
    [JsonPropertyName("numberOfChoices")]
    public int NumberOfChoices { get; set; }

    /// <summary>
    /// The index in the quiz questions
    /// </summary>
    [JsonPropertyName("questionIndex")]
    public int QuestionIndex { get; set; }

    /// <summary>
    /// The time left before the question finishes
    /// </summary>
    [JsonPropertyName("timeLeft")]
    public int TimeLeft { get; set; }

    /// <summary>
    /// The type of question received, this is parsed from <see cref="Type"/>
    /// </summary>
    [JsonIgnore] // used from the result of parsing Type
    public QuestionType QuestionType { get; set; }
}
