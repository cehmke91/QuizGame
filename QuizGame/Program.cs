using QuizGame;
using QuizGame.Connector.OpenTDB;
using QuizGame.Question;
using QuizGame.Scoring;
using System.Net.Http.Headers;

// Create a new client and set headers.
HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

// Create a connector instance and inject the client.
Connector connector = new Connector(client);

// Create objects needed to play the game
Quiz quiz = new Quiz(connector);
QuizMaster quizMaster = new QuizMaster();
ScoreKeeper scoreKeeper = new ScoreKeeper();

// Settings for the game.
// For now these are hardcoded. 
int amountQuestions = 5;
DifficultySetting difficulty = DifficultySetting.Mixed;

// get the questions and display loading text as this may be slow.
Console.WriteLine("Loading...");
await quiz.GenerateQuestions(amountQuestions, difficulty);
Console.Clear();

/*=====     GAME LOOP      =====*/

quizMaster.DisplayIntroduction();
Console.ReadKey();

// get the first question and set the answer
QuizQuestion? question = quiz.GetNextQuestion();
bool? answer = null;

// the game continues as long as there are questions.
while (question != null)
{
    Console.Clear();

    quizMaster.DisplayScoreboard(answer, scoreKeeper.Score);

    // ask the question to the user and score the output.
    answer = quizMaster.AskQuestion(question);
    scoreKeeper.Evaluate(question, answer);

    // get the next question
    question = quiz.GetNextQuestion();
}

// final output for the user before exiting program
quizMaster.DisplayGoodbye(scoreKeeper.Score);

