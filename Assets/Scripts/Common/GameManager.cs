using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;
    
    private Constants.GameType _gameType;

    private Canvas _canvas;

    private GameLogic _gameLogic;

    private GameUIController _gameUIController;
    
    /// <summary>
    /// Main -> Game
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game -> Main
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
        }
    }

    /// <summary>
    /// Game Scene에서 턴을 표시하는 UI를 제어하는 함수
    /// </summary>
    /// <param name="gameTurnPanelType"></param>
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block 초기화
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }
            
            // Game UI Controller 할당 및 초기화
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }
            
            // GameLogic 생성
            if (_gameLogic != null)
            {
                // TODO: 기존 게임 로직 소멸
            }

            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }
}
