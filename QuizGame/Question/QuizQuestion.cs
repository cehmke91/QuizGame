namespace QuizGame.Question;

/// <summary>
///     A question to be asked by the QuizGame
/// </summary>
abstract class QuizQuestion
{
    /// <summary>
    ///     The text of the question which is displayed
    ///     to the user.
    /// </summary>
    public string Text { get; }

    /// <summary>
    ///     The answer to the question.
    /// </summary>
    public dynamic Answer { get; }

    /// <summary>
    ///     How difficult this question is to answer.
    /// </summary>
    public DifficultySetting Difficulty { get; }

    /// <summary>
    ///     Construct a new Question
    /// </summary>
    /// <param name="text">The interrogative sentence used to ask the question.</param>
    /// <param name="answer">The answer to the question.</param>
    /// <param name="difficulty">How difficult this question is.</param>
    public QuizQuestion(string text, dynamic answer, DifficultySetting difficulty)
    {
        // questions cannot 

        Text = text;
        Answer = answer;
        Difficulty = difficulty;
    }

    /// <summary>
    ///     Returns a string representation of this question.
    /// </summary>
    /// <returns>string representation of this question</returns>
    public override string ToString()
    {
        return Text;
    }

    /// <summary>
    ///     Checks whether the provided answer input is a
    ///     valid format.
    /// </summary>
    /// <param name="answer">The answer as provided by the user</param>
    /// <returns></returns>
    public abstract bool ValidateAnswerInput(string answer);

    /// <summary>
    ///     Evaluates whether the provided answer is correct.
    /// </summary>
    /// <param name="answer">The given answer.</param>
    /// <returns>Whether the answer is correct.</returns>
    public bool CheckAnswer(dynamic answer)
    {
        return Answer.Equals(answer);
    }
}