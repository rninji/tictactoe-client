using UnityEngine;

public static class Constants
{
    public const string ServerUrl = "http://localhost:3000";
    public const string SocketServerUrl = "ws://localhost:3000";

    public enum MultiplayControllerState
    {
        CreateRoom,
        JoinRoom,
        StartGame,
        ExitRoom,
        EndGame
    }
    
    public enum GameType
    {
        SinglePlay,
        DualPlay,
        MultiPlay
    } 
    
    public enum PlayerType { None, PlayerA, PlayerB }
    
    public const int BlockColumnCount = 3;
}
