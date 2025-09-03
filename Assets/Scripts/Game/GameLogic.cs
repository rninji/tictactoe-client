using UnityEngine;

public class GameLogic
{
    public BlockController BlockController;
    private Constants.PlayerType[,] _board;         // 보드의 상태 정보
    
    public GameLogic(BlockController blockController, Constants.GameType gameType)
    {
        BlockController = blockController;
        
        // 보드의 상태 정보 초기화
        _board = 
            new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];
        
        // Game Type 초기화
        switch (gameType)
        {
            case Constants.GameType.Single:
                break;
            case Constants.GameType.Dual:
                break;
            case Constants.GameType.Multi:
                break;
        }
    }
}