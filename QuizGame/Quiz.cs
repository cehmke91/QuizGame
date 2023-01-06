using QuizGame.Connector;
using QuizGame.Question;

namespace QuizGame;

/// <summary>
///     The quiz the user will be taking.
/// </summary>
class Quiz
{
    private IConnector Connector { get; set; }

    private Queue<QuizQuestion>? _questions;

    /// <summary>
    ///     Create a Quiz instance
    /// </summary>
    /// <param name="questions">The questions to be used for this quiz</param>
    public Quiz(IConnector connector)
    {
        Connector = connector;
    }

    /// <summary>
    ///     Generate a list of questions and save them to this quiz object.
    /// </summary>
    /// <param name="amount">Number of questions to generate.</param>
    /// <param name="difficulty">Difficulty of the questions.</param>
    public async Task GenerateQuestions(int amount, DifficultySetting difficulty)
    {
        List<QuizQuestion> questionList = await Connector.GetQuestions(amount, difficulty);
        _questions = new Queue<QuizQuestion>(questionList);
    }

    /// <summary>
    ///     Removes a <c>QuizQuestions</c> from the list of questions in the quiz
    ///     and returns that <c>QuizQuestions</c>
    /// </summary>
    /// <returns>a <c>QuizQuestion</c></returns>
    public QuizQuestion? GetNextQuestion()
    {
        if (_questions == null || !_questions.Any()) return null;

        return _questions.Dequeue();
    }
}
