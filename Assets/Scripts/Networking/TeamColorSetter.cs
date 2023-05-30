using Mirror;
using Unity.VisualScripting;
using UnityEngine;

namespace Networking
{
    public class TeamColorSetter : NetworkBehaviour
    {
        [SerializeField] private Renderer[] colorRenderers = new Renderer[0];

        [SyncVar(hook = nameof(HandleTeamColorUpdated))] private Color _teamColor;

        #region Server

        public override void OnStartServer()
        {
            RTSPlayer player = connectionToClient.identity.GetComponent<RTSPlayer>();
            _teamColor = player.GetTeamColor();
        }

        #endregion

        #region Client

        private void HandleTeamColorUpdated(Color oldColor, Color newColor)
        {
            foreach (Renderer renderer in colorRenderers)
                renderer.material.color = newColor;
        }

        #endregion
    }
}
