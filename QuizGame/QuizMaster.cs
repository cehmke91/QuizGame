using QuizGame.Scoring;
using QuizGame.Question;

namespace QuizGame;

/// <summary>
///     QuizMaster runs the quiz by posing questions to the user and collecting
///     the feedback which it uses to evaluate the correctness of the answer and
///     to keep score accordingly.
/// </summary>
class QuizMaster
{
    /// <summary>
    ///     Output the introduction paragraph.
    /// </summary>
    public void DisplayIntroduction()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Hello! Welcome to QuizGame!\r\n");
        Console.WriteLine("===========================\r\n");
        Console.ResetColor();
        Console.WriteLine("You will be quizzed on questions relating to Computer Science.\r\n");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Instructions:");
        Console.ResetColor();

        Console.WriteLine("There are both multiple choice and true or false questions in this quiz.");
        Console.WriteLine("To answer a question enter the number of the answer presented.");
        Console.WriteLine("For example if you are presented with the following:\r\n");
        Console.WriteLine("(1) True     (2) False\r\n");
        Console.WriteLine("Then pressing 1 on the keyboard will answer as True.\r\n");
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Scoring:");
        Console.ResetColor();

        Console.WriteLine("You will receive points based on the diifculty of the question:");
        Console.WriteLine("Easy:    1 point");
        Console.WriteLine("Medium:  2 points");
        Console.WriteLine("Hard:    5 points");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("If you get a question wrong, the points will be subtracted from your score.\r\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Press any key to begin the quiz!");
        Console.ResetColor();
    }

    /// <summary>
    ///     Displays the scoreboard which included the following:
    ///     <list>
    ///         <item>Whether the previous question was correct</item>
    ///         <item>The current score</item>
    ///     </list>
    /// </summary>
    /// <param name="isPreviousQuestionCorrect"></param>
    /// <param name="score"></param>
    public void DisplayScoreboard(bool? previousAnswer, int score)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("=============================================");

        // if null display nothing, otherwise display whether the previous answer was correct or incorrect.
        // this should be null if no answer has yet been provided, but should still be displayed to keep the
        // interface consistent.
        string previousCorrect = (previousAnswer == null ? "" : (previousAnswer == true ? "Correct" : "Incorrect"));

        Console.Write($"Previous Question: {previousCorrect}      Score: {score}\r\n");
        Console.WriteLine("=============================================\r\n");
        Console.ResetColor();
    }

    /// <summary>
    ///     Display the goodbye message to the user.
    /// </summary>
    /// <param name="score">The final score for the user.</param>
    public void DisplayGoodbye(int score)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("=============================================");
        Console.WriteLine("Thank you for playing QuizGame!");
        Console.WriteLine("=============================================\r\n");
        Console.ResetColor();
        
        Console.WriteLine("That was it, there are no more questions!");
        Console.Write("Your final score is: ");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(score.ToString());
        Console.ResetColor();

        Console.WriteLine("\r\n\r\n");
    }

    /// <summary>
    ///     Pose a question to the user and evaluates the answer.
    /// </summary>
    /// <returns>Whether the answer was correct.</returns>
    public bool? AskQuestion(QuizQuestion question)
    {
        // get and score the answer from the user.
        Console.WriteLine(question.ToString());

        //TODO: null means skip this question
        return question.CheckAnswer(GetAnswerFromUser(question));
    }

    /// <summary>
    ///     Gets the answer from the user and validates that the input
    ///     is correct for the question.
    /// </summary>
    /// <param name="question">The Question being asked.</param>
    /// <returns>the answer to the question</returns>
    private dynamic GetAnswerFromUser(QuizQuestion question)
    {
        // loop until we receive valid input and return
        while (true)
        {
            // retrieve a valid input from the user.
            string answer = Console.ReadKey().KeyChar.ToString();

            // if the input is invalid simply ask for new input.
            if (!question.ValidateAnswerInput(answer))
            {
                Console.WriteLine("Please provide valid input.");
                continue;
            }

            if (question is MultipleChoiceQuestion)
            {
                return Convert.ToInt32(answer);
            }

            if (question is TrueFalseQuestion)
            {
                return answer == "1";
            }

        }
    }
}
