using Kahoot.NET.Client;

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
        /// <summary>
        /// Standard quiz mode (default) usually by Kahoot!
        /// </summary>
        /// <remarks>
        /// Answer the overload with a number in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string Quiz = "quiz";

        /// <summary>
        /// The question can have multiple answers, if necessary
        /// </summary>
        /// <remarks>
        /// Answer the question with an array in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string MultipleSelect = "multiple_select_quiz";

        /// <summary>
        /// A text input question
        /// </summary>
        /// <remarks>
        /// Answer the question with an string in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string OpenEnded = "open_ended";

        /// <summary>
        /// Text input poll
        /// </summary>
        /// <remarks>
        /// Answer the question with a string in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string WordCloud = "word_cloud";

        /// <summary>
        /// A multiple select poll
        /// </summary>
        /// <remarks>
        /// Answer the question with a number in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string Survey = "survey";

        /// <summary>
        /// Multiple select poll with 1 or more choices
        /// </summary>
        /// <remarks>
        /// Answer the question with an array in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string MultipleSelectPoll = "multiple_select_poll";

        /// <summary>
        /// A puzzle
        /// </summary>
        /// <remarks>
        /// Answer the question with a array in <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string Jumble = "puzzle";

        /// <summary>
        /// The question is just content, could be a video, no action is needed, prefer null during
        /// </summary>
        /// <remarks>
        /// Don't use any overload for <see cref="IKahootClient.AnswerAsync(QuizQuestionData, int?, int[], string?)"/>
        /// </remarks>
        public const string Content = "content";

        /// <summary>
        /// Parses the string representation, into an enumeration for easier code flow
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
