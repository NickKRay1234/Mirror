using Mirror;
using UnityEngine;

namespace Combat
{
    public class Targetable : NetworkBehaviour
    {
        [SerializeField] private Transform _aimAtPoint = null;
        public Transform GetAimAtPoint() => _aimAtPoint;
        
    }
}
