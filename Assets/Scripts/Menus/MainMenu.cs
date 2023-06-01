using Mirror;
using UnityEngine;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _landingPagePanel = null;

        public void HostLobby()
        {
            _landingPagePanel.SetActive(false);
            
            NetworkManager.singleton.StartHost();
        }
        
        
        
        
    }
}
