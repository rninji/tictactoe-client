using System;
using Newtonsoft.Json;
using SocketIOClient;
using UnityEngine;

public class RoomData
{
    [JsonProperty("roomId")]
    public string roomId { get; set; }
}

public class BlockData
{
    [JsonProperty("blockIndex")]
    public int blockIndex { get; set; }
}

public class MultiplayController : MonoBehaviour
{
    private SocketIOUnity _socket;

    public MultiplayController()
    {
        var uri = new Uri(Constants.SocketServerUrl);
        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        
        _socket.On("createRoom", CreateRoom);
        _socket.On("joinRoom", JoinRoom);
        _socket.On("startGame", StartGame);
        _socket.On("exitGame", ExitGame);
        _socket.On("endGame", EndGame);
        _socket.On("doOpponent", DoOpponent);
    }

    public void CreateRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
    }
    
    public void JoinRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
    }

    
    public void StartGame(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
    }

    
    public void ExitGame(SocketIOResponse response)
    {
        
    }

    
    public void EndGame(SocketIOResponse response)
    {
        
    }

    public void DoOpponent(SocketIOResponse response)
    {
        var data = response.GetValue<BlockData>();
    }
}
