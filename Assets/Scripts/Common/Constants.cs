using UnityEngine;

public static class Constants
{
    public enum GameType
    {
        Single,
        Dual,
        Multi
    } 
    
    public enum PlayerType { None, PlayerA, PlayerB }
    
    public const int BlockColumnCount = 3;
}
