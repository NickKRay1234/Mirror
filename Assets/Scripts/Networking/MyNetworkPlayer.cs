using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _displayNameText = null;
    [SerializeField] private Renderer _displayColourRenderer = null;
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))] [SerializeField] private string _displayName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))] [SerializeField] private Color _displayColor;

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        RpcLogNewName(newDisplayName);
        if(newDisplayName.Length < 2 || newDisplayName.Length > 20) {return;}
        SetDisplayName(newDisplayName);
    }

    [ContextMenu("Set My Name")] public void SetMyName() => CmdSetDisplayName("My New Name");
    [ClientRpc] private void RpcLogNewName(string newDisplayName) => Debug.Log(newDisplayName);


    #region Server
    [Server] public void SetDisplayName(string newDisplayName) => _displayName = newDisplayName;
    [Server] public void SetColor(Color color) => _displayColor = color;
    #endregion
    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor) =>
        _displayColourRenderer.material.color = newColor;
    private void HandleDisplayNameUpdated(string oldName, string newName) => _displayNameText.SetText(newName);
    #endregion
}
