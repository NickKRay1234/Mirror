using System.Collections.Generic;
using Mirror;
using Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Menus
{
    public class LobbyMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _lobbyUI = null;
        [SerializeField] private Button startGameButton = null;
        [SerializeField] private TMP_Text[] _playerNameTexts = new TMP_Text[4];

        private void Start()
        {
            RTSNetworking.ClientOnConnected += HandleClientConnected;
            RTSPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
            RTSPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
        }

        private void OnDestroy()
        {
            RTSNetworking.ClientOnConnected -= HandleClientConnected;
            RTSPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        }

        private void ClientHandleInfoUpdated()
        {
            List<RTSPlayer> players = ((RTSNetworking)NetworkManager.singleton).Players;

            for (int i = 0; i < players.Count; i++)
            {
                _playerNameTexts[i].text = players[i].GetDisplayName();
            }

            for (int i = players.Count; i < _playerNameTexts.Length; i++)
            {
                _playerNameTexts[i].text = "Waiting For Player...";
            }

            startGameButton.interactable = players.Count >= 2;

        }

        private void AuthorityHandlePartyOwnerStateUpdated(bool state)
        {
            startGameButton.gameObject.SetActive(state);
        }
        
        public void StartGame()
        {
           NetworkClient.connection.identity.GetComponent<RTSPlayer>().CmdStartGame();
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
