namespace QuizGame.Question;

/// <summary>
///     A question to be asked by the QuizGame
/// </summary>
class MultipleChoiceQuestion : QuizQuestion
{
    /// <summary>
    ///     The possible answers which are provided.
    /// </summary>
    public string[] Options { get; }

    /// <summary>
    ///     Construct a new Multiple Choice Question.
    /// </summary>
    /// <param name="text">The interrogative sentence used to ask the question.</param>
    /// <param name="answer">Index of the answer among options (starts from 1)</param>
    /// <remarks>
    ///     Note here that the user is receiving a list of options starting with
    ///     (1) and will answer accordingly. The index should be created so it
    ///     starts counting from 1.
    /// </remarks>
    public MultipleChoiceQuestion(string text, int answer, DifficultySetting difficulty, string[] options)
        : base(text, answer, difficulty)
    {
        Options = options;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        string question = Text + "\r\n";
        for (int i = 0; i < Options.Length; i++)
            question += $"({i + 1}) {Options[i]}\r\n";

        return question;
    }

    /// <inheritdoc />
    public override bool ValidateAnswerInput(string answer)
    {
        try
        {
            // convert the answer to an integer
            int intAnswer = Convert.ToInt32(answer);

            // the answer index must be contained within the text based options.
            if (intAnswer < 1 || intAnswer > Options.Length) return false;
        }

        // the answer was not a valid integer
        catch { return false; }



        return true;
    }

    /// <inheritdoc />
    /// <remarks>
    ///     Note here that the user is receiving a list of options starting with
    ///     (1) and will answer accordingly. The index should be created so it
    ///     starts counting from 1.
    /// </remarks>
    public bool CheckAnswer(int answer)
    {
        return base.CheckAnswer(answer);
    }
}