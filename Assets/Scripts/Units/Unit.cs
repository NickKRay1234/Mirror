using System;
using Combat;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Units
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] private int _resourceCost = 10;
        [SerializeField] private Health _health = null;
        [SerializeField] private UnitMovement _unitMovement = null;
        [SerializeField] private Targeter _targeter = null;
        [SerializeField] private UnityEvent onSelected = null;
        [SerializeField] private UnityEvent onDeselected = null;

        public int GetResourceCost() => _resourceCost;

        public static event Action<Unit> ServerOnUnitSpawned; 
        public static event Action<Unit> ServerOnUnitDespawned;
        
        public static event Action<Unit> AuthorityServerOnUnitSpawned; 
        public static event Action<Unit> AuthorityServerOnUnitDespawned;

        #region Server

        public override void OnStartServer()
        {
           ServerOnUnitSpawned?.Invoke(this);
           _health.ServerOnDie += ServerHandleDie;
        }

        public override void OnStopServer()
        {
            _health.ServerOnDie -= ServerHandleDie;
            ServerOnUnitDespawned?.Invoke(this);
        }

        [Server]
        private void ServerHandleDie()
        {
            NetworkServer.Destroy(gameObject);
        }

        #endregion

        public UnitMovement GetUnitMovement() => _unitMovement;

        public Targeter GetTargeter() => _targeter;

        #region Client

        public override void OnStartAuthority()
        {
            AuthorityServerOnUnitSpawned?.Invoke(this);
        }

        public override void OnStopClient()
        {
            if (!hasAuthority) return;
            AuthorityServerOnUnitDespawned?.Invoke(this);
        }
        
        
        
        
        [Client]
        public void Select()
        {
            if (!hasAuthority) return;
            onSelected?.Invoke();
        }

        [Client]
        public void Deselect()
        {
            if (!hasAuthority) return;
            onDeselected?.Invoke();
        }

        #endregion

    }
}
