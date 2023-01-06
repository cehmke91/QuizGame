using Newtonsoft.Json;

namespace QuizGame.Connector.OpenTDB;

/// <summary>
///     Structured question as received by the OpenTDBAPI
/// </summary>
class OpenTDBQuestion
{
    /// <summary>
    ///     The Category this question belongs to.
    ///     For now this is always "Science: Computers"
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    ///     The type of question this is.
    ///     <list>
    ///         <item>multiple</item>
    ///         <item>boolean</item>
    ///     </list>
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     The difficulty level of the question
    ///     <list>
    ///         <item>easy</item>
    ///         <item>medium</item>
    ///         <item>hard</item>
    ///     </list>
    /// </summary>
    [JsonProperty("difficulty")]
    public string Difficulty { get; set; }

    /// <summary>
    ///     The interogattive text stating the question.
    /// </summary>
    [JsonProperty("question")]
    public string Text { get; set; }

    /// <summary>
    ///     The correct answer for the question.
    /// </summary>
    /// <remarks>
    ///     In the case of a boolean question, this is returned as a string "True" or "False"
    /// </remarks>
    [JsonProperty("correct_answer")]
    public string CorrectAnswer { get; set; }

    /// <summary>
    ///     The incorrect answers available
    /// </summary>
    /// <remarks>
    ///     In the case of a boolean question, this is returned as a string "True" or "False"
    /// </remarks>
    [JsonProperty("incorrect_answers")]
    public List<string> IncorrectAnswers { get; set; }
}
