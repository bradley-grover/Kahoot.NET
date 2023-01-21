namespace Kahoot.NET.API;

/// <summary>
/// The identifier for the message data, used for <see cref="ContentData"/> purposes usually, some of these are not in use by the public client and are here for feature parity with Kahoot!
/// </summary>
/// <remarks>
/// This are subject to breaking changes if Kahoot! changes any of the values
/// </remarks>
public enum LiveEventId : uint
{
    /// <summary>
    /// A question is starting and the Kahoot! client should show a loading question screen
    /// </summary>
    GetReady = 1,

    /// <summary>
    /// The question has started and can now be answered
    /// </summary>
    QuestionStart = 2,

    /// <summary>
    /// The game block has ended
    /// </summary>
    GameBlockEnd = 3,

    /// <summary>
    /// The time is up for the question
    /// </summary>
    TimeUp = 4,

    /// <summary>
    /// The game has restarted, the client should prepare for the next game
    /// </summary>
    GameRestart = 5,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("SelectAnswer has never been in use but is here for feature parity, it serves no other purpose internally")]
    SelectAnswer = 6,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("AnswerResponse has never been in use but is here for feature parity, it serves no other purpose internally")]
    AnswerResponse = 7,

    /// <summary>
    /// The question has ended, data about this is contained in the payload
    /// </summary>
    QuestionEnd = 8,

    /// <summary>
    /// The quiz has started
    /// </summary>
    QuizStart = 9,

    /// <summary>
    /// The quiz has ended
    /// </summary>
    QuizEnd = 10,

    /// <summary>
    /// The message contains the feedback requested by <see cref="RequestFeedback"/>
    /// </summary>
    SendFeedback = 11,

    /// <summary>
    /// The host has requested for the game users to respond with <see cref="SendFeedback"/>
    /// </summary>
    RequestFeedback = 12,

    /// <summary>
    /// Reveals the current rank of the player
    /// </summary>
    RevealRank = 13,

    /// <summary>
    /// The sent name has been accepted by Kahoot!
    /// </summary>
    AcceptName = 14,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("DenyName has never been in use but is here for feature parity, it serves no other purpose internally")]
    DenyName = 15,

    /// <summary>
    /// Recovery data has been requested
    /// </summary>
    RequestRecoveryData = 16,

    /// <summary>
    /// Recovery data is contained in the sent message
    /// </summary>
    SendRecoveryData = 17,

    /// <summary>
    /// The team is being joined
    /// </summary>
    JoinTeam = 18,

    /// <summary>
    /// Team has been received
    /// </summary>
    ReceiveTeam = 19,

    /// <summary>
    /// The team discussion has started
    /// </summary>
    TeamTalkStart = 20,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("TeamTalkEnd has never been in use but is here for feature parity, it serves no other purpose internally")]
    TeamTalkEnd = 21,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("PauseGame has never been in use but is here for feature parity, it serves no other purpose internally")]
    PauseGame = 30,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("IFrameEvent has never been in use but is here for feature parity, it serves no other purpose internally")]
    IFrameEvent = 31,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("ServerIFrameEvent has never been in use but is here for feature parity, it serves no other purpose internally")]
    ServerIFrameEvent = 32,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("StoryBlockGetReady has never been in use but is here for feature parity, it serves no other purpose internally")]
    StoryBlockGetReady = 40,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("ReactionSelect has never been in use but is here for feature parity, it serves no other purpose internally")]
    ReactionSelect = 41,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("ReactionResponse has never been in use but is here for feature parity, it serves no other purpose internally")]
    ReactionResponse = 42,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("GameBlockStart has never been in use but is here for feature parity, it serves no other purpose internally")]
    GameBlockStart = 43,

    /// <summary>
    /// Obsolete
    /// </summary>
    [Obsolete("OldGameBlockEnd has never been in use but is here for feature parity, it serves no other purpose internally")]
    OldGameBlockEnd = 44,

    /// <summary>
    /// Message containing the answer to question
    /// </summary>
    AnswerQuestion = 45, // important

    /// <summary>
    /// The two factor has been submitted
    /// </summary>
    SubmitTwoFactor = 50,

    /// <summary>
    /// Invalid two factor authentication has been received
    /// </summary>
    InvalidTwoFactor = 51,

    /// <summary>
    /// Valid two factor authentication, you can play
    /// </summary>
    ValidTwoFactor = 52,

    /// <summary>
    /// The two factor code has reset
    /// </summary>
    TwoFactorReset = 53
}
