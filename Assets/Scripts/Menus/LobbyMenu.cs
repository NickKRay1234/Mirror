using System;
using Mirror;
using Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class LobbyMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _lobbyUI = null;

        private void Start()
        {
            RTSNetworking.ClientOnConnected += HandleClientConnected;
        }

        private void OnDestroy()
        {
            RTSNetworking.ClientOnConnected -= HandleClientConnected;
        }

        private void HandleClientConnected()
        {
            _lobbyUI.SetActive(true);
        }

        public void LeaveLobby()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopHost();
            }
            else
            {
                NetworkManager.singleton.StopClient();
                SceneManager.LoadScene(0);
            }
        }
    }
}
