using Mirror;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string _displayName = "Missing Name";
    [SyncVar] [SerializeField] private Color _displayColor;

    [Server]
    public void SetDisplayName(string newDisplayName) =>
        _displayName = newDisplayName;

    [Server]
    public void SetColor(Color color) => _displayColor = color;
}
