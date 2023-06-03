using Buildings;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Networking
{
    public class RTSNetworking : NetworkManager
    {
        [SerializeField] private GameObject _unitBasePrefab = null;
        [SerializeField] private GameOverHandler _gameOverHandlerPrefab = null;

        public static event Action ClientOnConnected;
        public static event Action ClientOnDisconnected;

        private bool isGameInProgress = false;
        public List<RTSPlayer> Players { get; } = new();

        #region Server

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();
            Players.Remove(player);
            base.OnServerDisconnect(conn);
        }

        public override void OnStopServer()
        {
            Players.Clear();
            isGameInProgress = false;
        }

        public void StartGame()
        {
            if (Players.Count < 2) return;
            isGameInProgress = true;
            ServerChangeScene("Scene_Map_01");
        }


        public override void OnServerConnect(NetworkConnectionToClient connection)
        {
            if (!isGameInProgress) return;
            connection.Disconnect();
        }
        
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();
            Players.Add(player);
            player.SetDisplayName($"Player {Players.Count}");
            player.SetTeamColor(new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f)));
            
            player.SetPartyOwner(Players.Count == 1);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
            {
                GameOverHandler gameOverHandlerInstance = Instantiate(_gameOverHandlerPrefab);
                NetworkServer.Spawn(gameOverHandlerInstance.gameObject);
                foreach (RTSPlayer player in Players)
                {
                    GameObject baseInstance = Instantiate(_unitBasePrefab, GetStartPosition().position, quaternion.identity);
                    NetworkServer.Spawn(baseInstance, player.connectionToClient);
                }
            }
        }
        
        #endregion

        #region Client
        
        public override void OnClientConnect()
        {
            base.OnClientConnect();
            
            ClientOnConnected?.Invoke();
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            ClientOnDisconnected?.Invoke();
        }

        public override void OnStopClient()
        {
            Players.Clear();
        }
        
        #endregion
    }
}
