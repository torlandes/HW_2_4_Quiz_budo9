using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    #region Variables

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _startButton.onClick.AddListener(StartGameScene);
        _exitButton.onClick.AddListener(ExitGame);
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

    private void StartGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    #endregion
}