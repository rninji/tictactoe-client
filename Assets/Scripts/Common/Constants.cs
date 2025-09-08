using UnityEngine;

public static class Constants
{
    public const string ServerUrl = "http://localhost:3000";
    
    public enum GameType
    {
        Single,
        Dual,
        Multi
    } 
    
    public enum PlayerType { None, PlayerA, PlayerB }
    
    public const int BlockColumnCount = 3;
}
