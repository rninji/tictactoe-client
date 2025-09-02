using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;
    
    private Constants.GameType _gameType;

    private Canvas _canvas;
    
    /// <summary>
    /// Main -> Game
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    ///
    /// Game -> Main
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenConfirmPanel(string message)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message);
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();
    }
}
