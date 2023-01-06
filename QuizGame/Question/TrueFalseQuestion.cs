namespace QuizGame.Question;

/// <summary>
///     A question to be asked by the QuizGame
/// </summary>
class TrueFalseQuestion : QuizQuestion
{
    /// <summary>
    ///     Construct a new True or False Question.
    /// </summary>
    /// <param name="text">The interrogative sentence used to ask the question.</param>
    /// <param name="answer">The correct answer</param>
    public TrueFalseQuestion(string text, bool answer, DifficultySetting difficulty) : base(text, answer, difficulty)
    { /* No body */ }

    /// <inheritdoc />
    public override string ToString()
    {
        return Text + "\r\n" + "(1) True     (2) False\r\n";
    }

    /// <inheritdoc />
    /// <remarks>
    ///     Here the string representation is checked which matches
    ///     what is output in the method <c>ToString</c>
    /// </remarks>
    public override bool ValidateAnswerInput(string answer)
    {
        // The answer must be either "1" True or "2" False.
        if (answer != "1" && answer != "2") return false;

        return true;
    }

    /// <inheritdoc />
    public bool CheckAnswer(bool answer)
    {
        return base.CheckAnswer(answer);
    }
}