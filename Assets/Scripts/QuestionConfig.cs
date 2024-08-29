using UnityEngine;

[CreateAssetMenu(fileName = nameof(QuestionsConfig), menuName = "Config/Questions/QuestionsConfig")]
public class QuestionsConfig : ScriptableObject
{
    #region Variables

    [TextArea(3, 10)]
    [SerializeField] private string _answer1;
    [TextArea(3, 10)]
    [SerializeField] private string _answer2;
    [TextArea(3, 10)]
    [SerializeField] private string _answer3;
    [TextArea(3, 10)]
    [SerializeField] private string _answer4;
    [TextArea(5, 10)]
    [SerializeField] private string _questions;
    [SerializeField] private int _correctAnswer;
    [SerializeField] private Sprite _questionSprite;

    #endregion

    #region Properties

    public string Answer1 => _answer1;
    public string Answer2 => _answer2;
    public string Answer3 => _answer3;
    public string Answer4 => _answer4;
    public string Question => _questions;
    public Sprite QuestionSprite => _questionSprite;
    public int CorrectAnswer => _correctAnswer;

    #endregion
}