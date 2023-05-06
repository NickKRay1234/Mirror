using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _displayNameText = null;
    [SerializeField] private Renderer _displayColourRenderer = null;
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))] [SerializeField] private string _displayName = "Missing Name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))] [SerializeField] private Color _displayColor;

    [Server] public void SetDisplayName(string newDisplayName) => _displayName = newDisplayName;
    [Server] public void SetColor(Color color) => _displayColor = color;

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor) =>
        _displayColourRenderer.material.color = newColor;
    private void HandleDisplayNameUpdated(string oldName, string newName) => _displayNameText.SetText(newName);
}
