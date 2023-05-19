using System;
using Combat;
using Mirror;
using UnityEngine;

namespace Units
{
    public class UnitProjectile : NetworkBehaviour
    {
        [SerializeField] private Rigidbody rb = null;
        [SerializeField] private int _damageToDeal = 20;
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

        [ServerCallback]
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out NetworkIdentity networkIdentity))
            {
                if (networkIdentity.connectionToClient == connectionToClient) return;
            }
            
            if(other.TryGetComponent(out Health health)) health.DealDamage(_damageToDeal);
            DestroySelf();
        }


        [Server]
        private void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
