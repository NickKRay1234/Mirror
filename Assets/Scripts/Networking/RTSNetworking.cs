using Mirror;
using UnityEngine;

namespace Networking
{
    public class RTSNetworking : NetworkManager
    {
        [SerializeField] private GameObject _unitSpawnerPrefab = null;
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            GameObject unitSpawnerInstance = Instantiate(_unitSpawnerPrefab, conn.identity.transform.position, conn.identity.transform.rotation);
            NetworkServer.Spawn(unitSpawnerInstance, conn);
        }
    }
}
