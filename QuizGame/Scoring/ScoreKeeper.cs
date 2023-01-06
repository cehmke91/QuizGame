using QuizGame.Question;

namespace QuizGame.Scoring;

class ScoreKeeper : IScoreKeeper
{
    public int Score { get; private set; }

    /// <inheritdoc />
    public void Evaluate(QuizQuestion question, bool? correct)
    {
        if (correct == null) return;

        // get a score value for this question based on difficulty.
        int questionScore;
        switch (question.Difficulty)
        {
            case DifficultySetting.Hard: questionScore = 5; break;
            case DifficultySetting.Medium: questionScore = 2; break;
            case DifficultySetting.Easy:
            default: questionScore = 1; break;
        }

        // use a negative modifier if answered incorrectly.
        if (correct == false) questionScore *= -1;

        // set the score.
        Score += questionScore;
    }
}
