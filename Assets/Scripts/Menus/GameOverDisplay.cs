using Buildings;
using Mirror;
using TMPro;
using UnityEngine;

namespace Menus
{
    public class GameOverDisplay : MonoBehaviour
    {
        
        
        [SerializeField] private GameObject _gameOverDisplayParent = null;
        [SerializeField] private TMP_Text _winnerNameText = null;
        private void Start()
        {
            GameOverHandler.ClientOnGameOver += ClientHandlerGameOver;
        }

        private void OnDestroy()
        {
            GameOverHandler.ClientOnGameOver -= ClientHandlerGameOver;
        }

        public void LeaveGame()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopHost();
            }
            else
            {
                NetworkManager.singleton.StopClient();
            }
        }
        
        private void ClientHandlerGameOver(string winner)
        {
            _winnerNameText.text = $"{winner} Has Won!";
            _gameOverDisplayParent.SetActive(true);
        }
    }
}
