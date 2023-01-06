using QuizGame.Question;

namespace QuizGame.Connector;

interface IConnector
{
    /// <summary>
    ///     Retrieve a set of questions from the external API.
    /// </summary>
    /// <remarks>This method may be asynchronous.</remarks>
    /// <param name="amount">The number of questions to fetch.</param>
    /// <param name="difficulty"> How difficult the questions should be.</param>
    /// <returns>Task which will resolve as a list of <c>QuizGame.Questions</c></returns>
    public Task<List<QuizQuestion>> GetQuestions(int amount, DifficultySetting difficulty);
}
