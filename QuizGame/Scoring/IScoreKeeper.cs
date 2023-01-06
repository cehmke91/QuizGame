using QuizGame.Question;

namespace QuizGame.Scoring;

interface IScoreKeeper
{
    public int Score { get; }

    /// <summary>
    ///     Evaluates the points to be added based on the question
    ///     then returns the total accumulated score
    /// </summary>
    /// <param name="question">The Question to evaluate.</param>
    /// <param name="correct">
    ///     Whether the question was answered correctly.
    ///     <remarks>Null is passed to indicate skipping the question.</remarks>
    /// </param>
    public void Evaluate(QuizQuestion question, bool? correct);
}

