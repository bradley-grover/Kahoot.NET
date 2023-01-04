namespace Kahoot.NET.API;

/// <summary>
/// The identifier for the message data, used for <see cref="ContentData"/> purposes usually
/// </summary>
public enum LiveEventId
{
    GetReady = 1,
    QuestionStart = 2,
    GameBlockEnd = 3,
    TimeUp = 4,
    GameRestart = 5,
    SelectAnswer = 6,
    AnswerResponse = 7,
    QuestionEnd = 8,
    QuizStart = 9,
    QuizEnd = 10,
    SendFeedback = 11,
    RequestFeedback = 12,
    RevealRank = 13,
    AcceptName = 14,
    DenyName = 15,
    RequestRecoveryData = 16,
    SendRecoveryData = 17,
    JoinTeam = 18,
    ReceiveTeam = 19,
    TeamTalkStart = 20,
    TeamTalkEnd = 21,
    PauseGame = 30,
    IFrameEvent = 31,
    ServerIFrameEvent = 32,
    StoryBlockGetReady = 40,
    ReactionSelect = 41,
    ReactionResponse = 42,
    GameBlockStart = 43, 
    //GameBlockEnd = 44, error?
    AnswerQuestion = 45, // important
    SubmitTwoFactor = 50,
    InvalidTwoFactor = 51,
    ValidTwoFactor = 52,
    TwoFactorReset = 53
}
