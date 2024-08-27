using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    #region Variables
    
    [Header("Quiz UI")]
    [SerializeField] private QuestionsConfig[] _questions;
    [SerializeField] private TMP_Text _questionLabel;
    [SerializeField] private TMP_Text[] _answersLabel;
    [SerializeField] private Image _questionImage;
    [SerializeField] private Button[] _answerButtons;

    [Header("Settings")]
    [SerializeField] private int _delay = 1;

    [Header("Button Sprites")]
    [SerializeField] private Sprite _correctSprite;
    [SerializeField] private Sprite _incorrectSprite;
    [SerializeField] private Sprite _defaultSprite;

    private int _currentQuestion;
    private int _score;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            int index = i;
            _answerButtons[i].onClick.AddListener(() => SelectAnswerClickedCallback(index));
        }

        LoadQuestion();
    }

    #endregion

    #region Private methods

    private void EndQuiz()
    {
        int incorrectAnswers = _currentQuestion - _score;
        Statistics.CorrectAnswers = _score;
        Statistics.IncorrectAnswers = incorrectAnswers;
        SceneManager.LoadScene("GameOverScene");
    }

    private void LoadQuestion()
    {
        if (_currentQuestion < _questions.Length)
        {
            QuestionsConfig question = _questions[_currentQuestion];
            _questionImage.sprite = question.QuestionSprite;
            _questionLabel.text = question.Question;
            _answersLabel[0].text = question.Answer1;
            _answersLabel[1].text = question.Answer2;
            _answersLabel[2].text = question.Answer3;
            _answersLabel[3].text = question.Answer4;
            foreach (Button button in _answerButtons)
            {
                button.image.sprite = _defaultSprite;
            }
        }
    }

    private IEnumerator ProceedToNextQuestionWithDelay()
    {
        yield return new WaitForSeconds(_delay);
        _currentQuestion++;
        if (_currentQuestion < _questions.Length)
        {
            LoadQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    private void SelectAnswerClickedCallback(int answerIndex)
    {
        bool isCorrect = answerIndex + 1 == _questions[_currentQuestion].CorrectAnswer;

        if (isCorrect)
        {
            _score++;
        }
        else
        {
            ShowCorrectAnswer();
        }

        _answerButtons[answerIndex].image.sprite = isCorrect ? _correctSprite : _incorrectSprite;

        StartCoroutine(ProceedToNextQuestionWithDelay());
    }

    private void ShowCorrectAnswer()
    {
        int correctAnswerIndex = _questions[_currentQuestion].CorrectAnswer - 1;
        _answerButtons[correctAnswerIndex].image.sprite = _correctSprite;
    }

    #endregion
    
}