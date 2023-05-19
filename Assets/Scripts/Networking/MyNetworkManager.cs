using Mirror;
using UnityEngine;

namespace Networking
{
    public class MyNetworkManager : NetworkManager
    {
        public override void OnClientConnect()
        {
            base.OnClientConnect();
            Debug.Log("I connected to a server");
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
            player.SetDisplayName("Player " + numPlayers);
            player.SetColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            Debug.Log("I created on a map");
            Debug.Log("Number of players on the server: " + numPlayers);
        }
    }
}
