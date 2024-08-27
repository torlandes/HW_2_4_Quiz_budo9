using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    #region Variables

    [SerializeField] private TMP_Text _resultLabel;
    [SerializeField] private Button _restartGame;
    [SerializeField] private Button _exitGame;
    
    #endregion

    #region Unity lifecycle

    private void Start()
    {
        int correctAnswers = Statistics.CorrectAnswers;
        int incorrectAnswers = Statistics.IncorrectAnswers;
        _resultLabel.text =  $"Верные ответы: {correctAnswers} \nНеверные ответы: {incorrectAnswers}";
        _restartGame.onClick.AddListener(RestartGame);
        _exitGame.onClick.AddListener(ExitGame);
    }


    #endregion

    #region Private methods

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR        
        UnityEditor.EditorApplication.isPlaying = false;
#endif         
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("StartGameScene");
    }

    #endregion
}