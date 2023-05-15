using System;
using Mirror;
using Mirror.Examples.Benchmark.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Units
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] private UnitMovement _unitMovement = null;
        [SerializeField] private UnityEvent onSelected = null;
        [SerializeField] private UnityEvent onDeselected = null;

        public static event Action<Unit> ServerOnUnitSpawned; 
        public static event Action<Unit> ServerOnUnitDespawned;
        
        public static event Action<Unit> AuthorityServerOnUnitSpawned; 
        public static event Action<Unit> AuthorityServerOnUnitDespawned;

        #region Server

        public override void OnStartServer()
        {
           ServerOnUnitSpawned?.Invoke(this);
        }

        public override void OnStopServer()
        {
            ServerOnUnitDespawned?.Invoke(this);
        }

        #endregion

        public UnitMovement GetUnitMovement() => _unitMovement;

        #region Client

        public override void OnStartClient()
        {
            if (!isClientOnly || !hasAuthority) return;
            AuthorityServerOnUnitSpawned?.Invoke(this);
        }

        public override void OnStopClient()
        {
            if (!isClientOnly || !hasAuthority) return;
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
