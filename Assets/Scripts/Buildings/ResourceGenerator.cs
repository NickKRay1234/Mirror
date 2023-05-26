using System.Threading;
using Combat;
using Mirror;
using Networking;
using UnityEngine;

namespace Buildings
{
    public class ResourceGenerator : NetworkBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private int _resourcesPerInterval = 10;
        [SerializeField] private float _interval = 2f;

        private float _timer;
        private RTSPlayer _player;

        public override void OnStartServer()
        {
            _timer = _interval;
            _player = connectionToClient.identity.GetComponent<RTSPlayer>();
            _health.ServerOnDie += ServerHandleDie;
            GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
        }

        public override void OnStopServer()
        {
            _health.ServerOnDie -= ServerHandleDie;
            GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
        }

        [ServerCallback]
        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _player.SetResources(_player.GetResources() + _resourcesPerInterval);
                _timer += _interval;
            }
        }

        private void ServerHandleGameOver()
        {
            enabled = false;
        }

        private void ServerHandleDie()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
