namespace Kahoot.NET.API.Shared;

/// <summary>
/// The types that can be included in the <see cref="Data.Type"/> field
/// </summary>
public static class Types
{
    /// <summary>
    /// Data type used for when the user is logging in/authenticating to the game
    /// </summary>
    public const string Login = "login";

    /// <summary>
    /// The response received when the login info is received, like the name
    /// </summary>
    public const string LoginResponse = "loginResponse";

    /// <summary>
    /// Represents a general message for various data types
    /// </summary>
    public const string Message = "message";

    /// <summary>
    /// Status updates about certain events
    /// </summary>
    public const string Status = "status";

    /// <summary>
    /// Status type for when the player is now active in the kahoot
    /// </summary>
    public const string Active = "ACTIVE";

    /// <summary>
    /// Represents errors that have occured contained within the data
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Represents when the user input is bad, like a duplicate user name
        /// </summary>
        public const string UserInput = "USER_INPUT";

        /// <summary>
        /// When the user tries to authenticate to a locked quiz
        /// </summary>
        public const string Locked = "LOCKED";

        /// <summary>
        /// Represents when the game has a large amount of members and the client has to wait in a queue
        /// </summary>
        public const string Queue = "QUEUE";
    }

    /// <summary>
    /// Represents data types for when hosting a game
    /// </summary>
    public static class Hosting
    {
        /// <summary>
        /// Represents that the host has created the game succesfully
        /// </summary>
        public const string Started = "started";
    }

    /// <summary>
    /// Represents types for questions
    /// </summary>
    public static class Question
    {
        public const string Quiz = "quiz";
        public const string MultipleSelect = "multiple_select_quiz";
        public const string OpenEnded = "open_ended";
        public const string WordCloud = "word_cloud";
        public const string Survey = "survey";
        public const string MultipleSelectPoll = "multiple_select_poll";
        public const string Jumble = "puzzle";
        public const string Content = "content";

        public static QuestionType GetQuestionType(string type)
        {
            return type switch
            {
                Quiz => QuestionType.Quiz,
                MultipleSelect => QuestionType.MultipleSelect,
                OpenEnded => QuestionType.OpenEnded,
                WordCloud => QuestionType.WordCloud,
                Survey => QuestionType.Survey,
                MultipleSelectPoll => QuestionType.MultipleSelectPoll,
                Jumble => QuestionType.Jumble,
                Content => QuestionType.Content,
                _ => QuestionType.Content // return Content for invalid so we can just ignore it
            };
        }
    }
}
