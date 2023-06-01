using Mirror;
using Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class JoinLobbyMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _landingPagePanel = null;
        [SerializeField] private TMP_InputField _addressInput = null;
        [SerializeField] private Button joinButton = null;

        private void OnEnable()
        {
            RTSNetworking.ClientOnConnected += HandleClientConnected;
            RTSNetworking.ClientOnDisconnected += HandleClientDisconnected;
        }

        private void OnDisable()
        {
            RTSNetworking.ClientOnConnected -= HandleClientConnected;
            RTSNetworking.ClientOnDisconnected -= HandleClientDisconnected;
        }

        public void Join()
        {
            string address = _addressInput.text;
            NetworkManager.singleton.networkAddress = address;
            NetworkManager.singleton.StartClient();

            joinButton.interactable = false;
        }

        private void HandleClientConnected()
        {
            joinButton.interactable = true;
            gameObject.SetActive(false);
            _landingPagePanel.SetActive(false);
        }

        private void HandleClientDisconnected()
        {
            joinButton.interactable = true;
        }
        
        
        
    }
}
