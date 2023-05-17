using Mirror;
using UnityEngine;

namespace Combat
{
    public class Targeter : NetworkBehaviour
    {
        private Targetable _target;
        public Targetable GetTarget() => _target;

        [Command]
        public void CmdSetTarget(GameObject targetGameObject)
        {
            if (!targetGameObject.TryGetComponent<Targetable>(out Targetable newTarget)) return;
            _target = newTarget;
        }

        [Server]
        public void ClearTarget()
        {
            _target = null;
        }
    }
}
