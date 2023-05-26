using System;
using Mirror;
using Networking;
using TMPro;
using UnityEngine;

namespace Resources
{
    public class ResourcesDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resourcesText = null;
        private RTSPlayer _player;

        private void Update()
        {
            if (_player == null)
            {
                _player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
                if (_player != null)
                {
                    ClientHandleResourcesUpdated(_player.GetResources());
                    _player.ClientOnResourcesUpdated += ClientHandleResourcesUpdated;
                }
            }
            
        }

        private void OnDestroy() => 
            _player.ClientOnResourcesUpdated -= ClientHandleResourcesUpdated;

        private void ClientHandleResourcesUpdated(int resources) =>
            _resourcesText.text = $"Resources: {resources}";
    }
}
