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

public class MultiplayController : IDisposable
{
    private SocketIOUnity _socket;

    private Action<Constants.MultiplayControllerState, string> _onMultiPlayStateChanged;
    private Action<int> _onBlockDataChanged;

    public MultiplayController(Action<Constants.MultiplayControllerState, string> onMultiPlayStateChanged)
    {
        // 서버에서 이벤트가 발생하면 처리할 메서드
        _onMultiPlayStateChanged = onMultiPlayStateChanged;
        
        var uri = new Uri(Constants.SocketServerUrl);
        _socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        
        _socket.On("createRoom", CreateRoom);
        _socket.On("joinRoom", JoinRoom);
        _socket.On("startGame", StartGame);
        _socket.On("exitRoom", ExitRoom);
        _socket.On("endGame", EndGame);
        _socket.On("doOpponent", DoOpponent);
        
        _socket.Connect(); // 서버에 접속
    }

    public void CreateRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiPlayStateChanged?.Invoke(Constants.MultiplayControllerState.CreateRoom, data.roomId);
    }
    
    public void JoinRoom(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiPlayStateChanged?.Invoke(Constants.MultiplayControllerState.JoinRoom, data.roomId);
    }

    
    public void StartGame(SocketIOResponse response)
    {
        var data = response.GetValue<RoomData>();
        _onMultiPlayStateChanged?.Invoke(Constants.MultiplayControllerState.StartGame, data.roomId);
    }

    
    public void ExitRoom(SocketIOResponse response)
    {
        _onMultiPlayStateChanged?.Invoke(Constants.MultiplayControllerState.ExitRoom, null);
    }

    
    public void EndGame(SocketIOResponse response)
    {
        _onMultiPlayStateChanged?.Invoke(Constants.MultiplayControllerState.EndGame, null);
    }

    public void DoOpponent(SocketIOResponse response)
    {
        var data = response.GetValue<BlockData>();
        _onBlockDataChanged?.Invoke(data.blockIndex);
    }
    
    #region Client => Server

    public void LeaveRoom(string roomId)
    {
        _socket.Emit("leaveRoom", new { roomId });
    }

    public void DoPlayer(string roomId, int position)
    {
        _socket.Emit("doPlayer", new { roomId, position });
    }
    #endregion

    public void Dispose()
    {
        if (_socket != null)
        {
            _socket.Disconnect();
            _socket.Dispose();
            _socket = null;
        }
    }
}
