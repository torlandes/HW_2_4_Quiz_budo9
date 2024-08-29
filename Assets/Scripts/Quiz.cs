using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Button _helpButton;

    [Header("Settings")]
    [SerializeField] private GameObject[] _healthBar;
    [SerializeField] private GameObject _help;
    [SerializeField] private int _delay = 1;

    [Header("Button Sprites")]
    [SerializeField] private Sprite _correctSprite;
    [SerializeField] private Sprite _incorrectSprite;
    [SerializeField] private Sprite _defaultSprite;

    private int _currentQuestion;
    private int _lives;
    private int _score;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _lives = _healthBar.Length;

        _helpButton.onClick.AddListener(HelpButtonClickedCallBack);

        ShuffleQuestions();

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            int i1 = i;
            _answerButtons[i].onClick.AddListener(() => SelectAnswerClickedCallback(i1));
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

    private void HelpButtonClickedCallBack()
    {
        int correctIndex = _questions[_currentQuestion].CorrectAnswer;

        List<int> incorrectIndexes = new();

        while (incorrectIndexes.Count < 2)
        {
            int randomIncorrect = Random.Range(0, 4);
            
            if (randomIncorrect == correctIndex || incorrectIndexes.Contains(randomIncorrect)) 
            {
                continue;
            }
            
            incorrectIndexes.Add(randomIncorrect);
        }

        foreach (int incorrectIndex in incorrectIndexes)
        {
            _answerButtons[incorrectIndex].gameObject.SetActive(false);
            Debug.Log($"incorrectIndex {incorrectIndex}");
            Debug.Log($"correctIndex {correctIndex}");
        }

        _help.gameObject.SetActive(false);
        
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
                button.gameObject.SetActive(true);
                _help.gameObject.SetActive(true);
            }
        }
    }

    private void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            _healthBar[_lives].SetActive(false);
        }
    }

    private IEnumerator ProceedToNextQuestionWithDelay()
    {
        SetAnswerButtonInteractable(false);

        yield return new WaitForSeconds(_delay);

        _currentQuestion++;
        if (_lives > 0 && _currentQuestion < _questions.Length)
        {
            LoadQuestion();
        }
        else
        {
            EndQuiz();
        }

        SetAnswerButtonInteractable(true);
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
            LoseLife();
            ShowCorrectAnswer();
        }

        _answerButtons[answerIndex].image.sprite = isCorrect ? _correctSprite : _incorrectSprite;

        StartCoroutine(ProceedToNextQuestionWithDelay());
    }

    private void SetAnswerButtonInteractable(bool value)
    {
        foreach (Button button in _answerButtons)
        {
            button.enabled = value;
        }
    }
    
    private void ShowCorrectAnswer()
    {
        int correctAnswerIndex = _questions[_currentQuestion].CorrectAnswer - 1;
        _answerButtons[correctAnswerIndex].image.sprite = _correctSprite;
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < _questions.Length; i++)
        {
            QuestionsConfig temp = _questions[i];
            int randomIndex = Random.Range(i, _questions.Length);
            _questions[i] = _questions[randomIndex];
            _questions[randomIndex] = temp;
        }
    }

    #endregion
}