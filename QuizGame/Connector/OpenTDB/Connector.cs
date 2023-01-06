using Newtonsoft.Json;
using QuizGame.Question;
using System.Collections.Specialized;
using System.Web;

namespace QuizGame.Connector.OpenTDB;

/// <summary>
///     Connects to the OpenTDB API to generate questions for the quiz.
/// </summary>
class Connector : IConnector
{
    /// <summary>
    ///     HttpClient used to connect to the external API
    /// </summary>
    private HttpClient _client;

    /// <summary>
    ///     UriBuilder used to build query strings for the external API.
    ///     We always want to default to the base URI for OpenTDB.
    /// </summary>
    private UriBuilder _uriBuilder = new UriBuilder("https://opentdb.com/api.php");

    /// <summary>
    ///     For the purpose of this demonstration we will only generate
    ///     questions with the category "Science: Computers" (18).
    /// </summary>
    private string _category = "18";

    /// <summary>
    ///     Create a new OpenTDB Connector
    /// </summary>
    /// <param name="client">Http Client to connect to the API.</param>
    public Connector(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    ///     Retrieve the questions from the OpenTDB API
    /// </summary>
    /// <inheritdoc />
    public async Task<List<QuizQuestion>> GetQuestions(int amount, DifficultySetting difficulty)
    {
        // set the query parameters.
        SetQueryParameters(amount, difficulty);

        // await the API response and convert it to a response object.
        ApiResponse response = await GetApiResponse();

        // create an list of questions to return to caller.
        List<QuizQuestion> questions = new List<QuizQuestion>();
        foreach (OpenTDBQuestion openTDBQuestion in response.Results)
            questions.Add(ConvertToQuestion(openTDBQuestion));

        return questions;
    }

    /// <summary>
    ///     Uses the query parameters set to build a query 
    ///     string and sets it on the _uriBuilder.
    /// </summary>
    /// <param name="amount">Amount of questions to generate.</param>
    /// <param name="difficulty">Difficulty for the generated questions.</param>
    /// <param name="type">The type of questions to generate.</param>
    private void SetQueryParameters(int amount, DifficultySetting difficulty)
    {
        // initialize a query collection.
        NameValueCollection query = HttpUtility.ParseQueryString("");

        // set the category to Science: Computers.
        query["category"] = _category;

        // set the number of questions to be asked
        query["amount"] = amount.ToString();

        // set the difficulty parameter if one is provided
        switch (difficulty)
        {
            case DifficultySetting.Easy:
                query["difficulty"] = "easy";
                break;

            case DifficultySetting.Medium:
                query["difficulty"] = "medium";
                break;

            case DifficultySetting.Hard:
                query["difficulty"] = "hard";
                break;

            case DifficultySetting.Mixed:
            default:
                // The API simply uses no query param in this case.
                break;
        }

        // set the final query on the UriBuilder.
        _uriBuilder.Query = query.ToString();
    }

    /// <summary>
    ///     Calls the API and returns a structured response.
    /// </summary>
    /// <returns>ApiResponse object containing the response</returns>
    /// <exception cref="Exception">Unable to deserialize the JSON response to an ApiResponse object.</exception>
    private async Task<ApiResponse> GetApiResponse()
    {
        // call the external API and deserialize the response.
        string json = await _client.GetStringAsync(_uriBuilder.Uri);
        ApiResponse? response = JsonConvert.DeserializeObject<ApiResponse>(json);

        // error out if unable to deserialize the response.
        if (response is null) throw new Exception("Unable to serialize response from OpenTDB API.");

        return response;
    }

    /// <summary>
    ///     Converts a question as is received by the OpenTDB API to a
    ///     Question as is used by the QuizGame.
    /// </summary>
    /// <param name="question">The <c>Question</c> to convert</param>
    /// <returns></returns>
    private QuizQuestion ConvertToQuestion(OpenTDBQuestion question)
    {
        // create and return a new question based on the type.
        switch (question.Type)
        {
            case "multiple": return CreateMultipleChoiceQuestion(question);
            case "boolean": return CreateTrueFalseQuestion(question);

            // An unrecognized question type was received from the API. This shouldn't happen.
            default: throw new Exception("Unrecognized question type received.");
        }
    }

    /// <summary>
    ///     Creates a Multiple choice question from an OpenTDBQuestion.
    /// </summary>
    /// <param name="question">The OpenTDBQuestion to convert.</param>
    /// <returns>a MultipleChoiceQuestion</returns>
    private MultipleChoiceQuestion CreateMultipleChoiceQuestion(OpenTDBQuestion question)
    {
        // decode the question string
        string questionText = HttpUtility.HtmlDecode(question.Text);

        // generate a list with the incorrect answers.
        List<string> options = new List<string>();
        foreach (string option in question.IncorrectAnswers)
            options.Add(HttpUtility.HtmlDecode(option));

        // generate the values for the correct answer and insert it into the list.
        int correctIndex = (new Random()).Next(options.Count);
        options.Insert(correctIndex, HttpUtility.HtmlDecode(question.CorrectAnswer));

        // since the user will receive a list of options starting at 1 the index
        // should be offset so that it matches user input.
        correctIndex += 1;

        // get the difficulty for the question
        DifficultySetting difficulty = ConvertToDifficultySetting(question.Difficulty);

        // create and return a MultipleChoiceQuestion.
        return new MultipleChoiceQuestion(questionText, correctIndex, difficulty, options.ToArray());
    }

    /// <summary>
    ///     Create a True or False question from an OpenTDBQuestion.
    /// </summary>
    /// <param name="question">The OpenTDBQuestion to convert.</param>
    /// <returns>a TrueFalseQuestion</returns>
    private TrueFalseQuestion CreateTrueFalseQuestion(OpenTDBQuestion question)
    {
        // decode the question string
        string questionText = HttpUtility.HtmlDecode(question.Text);

        // determine whether the answer was correct
        bool correctAnswer = question.CorrectAnswer == "True";

        // get the difficulty for the question
        DifficultySetting difficulty = ConvertToDifficultySetting(question.Difficulty);

        // create and return a TrueFalse Question.
        return new TrueFalseQuestion(questionText, correctAnswer, difficulty);
    }

    /// <summary>
    ///     Converts the string representation difficulty to a <c>DifficultySetting</c>.
    /// </summary>
    /// <param name="difficulty">The string difficulty returned by OpenTDB</param>
    /// <returns>A <c>DifficultySetting</c> object</returns>
    private DifficultySetting ConvertToDifficultySetting(string difficulty)
    {
        switch (difficulty)
        {
            case "easy": return DifficultySetting.Easy;
            case "medium": return DifficultySetting.Medium;
            case "hard": return DifficultySetting.Hard;
            default: return DifficultySetting.Mixed;
        }
    }
}
