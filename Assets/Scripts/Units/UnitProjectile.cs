using Mirror;
using UnityEngine;

namespace Units
{
    public class UnitProjectile : NetworkBehaviour
    {
        [SerializeField] private Rigidbody rb = null;
        [SerializeField] private float _destroyAfterSeconds = 5f;
        [SerializeField] private float _launchForce = 10f;

        private void Start()
        {
            rb.velocity = transform.forward * _launchForce;
        }


        public override void OnStartServer()
        {
            Invoke(nameof(DestroySelf), _destroyAfterSeconds);
        }
        
        
        [Server]
        private void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
