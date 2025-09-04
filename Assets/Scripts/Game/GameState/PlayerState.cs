using UnityEngine;

public class PlayerState : BasePlayerState
{
    private bool _isFirstPlayer;
    private Constants.PlayerType _playerType;
    
    public PlayerState(bool isFirstPlayer)
    {
        _isFirstPlayer = isFirstPlayer;
        _playerType = _isFirstPlayer ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }
    
    # region 필수 메서드
    public override void OnEnter(GameLogic gameLogic)
    {
        // 1. First Player인지 확인해서 게임 UI에 현재 턴 표시
        if (_isFirstPlayer)
            GameManager.Instance.SetGameTurnPanel(GameUIController.GameTurnPanelType.ATurn);
        else
            GameManager.Instance.SetGameTurnPanel(GameUIController.GameTurnPanelType.BTurn);
        
        // 2. BlockController에 해야할 일 전달
        gameLogic.blockController.OnBlockClickedDelegate = (row, col) =>
        {
            // Block이 터치될 때까지 기다렸다가
            // 터치 되면 처리할 일
            HandleMove(gameLogic, row, col);
        };
    }

    public override void OnExit(GameLogic gameLogic)
    {
        gameLogic.blockController.OnBlockClickedDelegate = null;
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col)
    {
        ProcessMove(gameLogic, _playerType, row, col);
    }

    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (_isFirstPlayer)
        {
            // 게임 로직에게 Second Player의 상태를 활성화하라고 전달
            gameLogic.SetState(gameLogic.secondPlayerState);
        }
        else
        {
            // 게임 로직에게 First Player의 상태를 활성화하라고 전달
            gameLogic.SetState(gameLogic.firstPlayerState);
        }
    }
    #endregion
}
