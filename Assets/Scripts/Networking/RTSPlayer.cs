using System.Collections.Generic;
using Mirror;
using Units;
using UnityEngine;

namespace Networking
{
    public class RTSPlayer : NetworkBehaviour
    {
        [SerializeField] private List<Unit> myUnits = new();

        public List<Unit> GetMyUnits() => myUnits;

        public override void OnStartServer()
        {
            Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
            Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
        }

        public override void OnStopServer()
        {
            Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
            Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
        }

        private void ServerHandleUnitSpawned(Unit unit)
        {
            if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
            myUnits.Add(unit);
        }
        
        private void ServerHandleUnitDespawned(Unit unit)
        {
            if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
            myUnits.Remove(unit);
        }

        #region Client

        public override void OnStartAuthority()
        {
            if (NetworkServer.active) return;
            Unit.AuthorityServerOnUnitSpawned += AuthorityServerHandleUnitSpawned;
            Unit.AuthorityServerOnUnitDespawned += AuthorityServerHandleUnitDespawned;
        }

        public override void OnStopClient()
        {
            if (!isClientOnly || !hasAuthority) return;
            Unit.AuthorityServerOnUnitSpawned -= AuthorityServerHandleUnitSpawned;
            Unit.AuthorityServerOnUnitDespawned -= AuthorityServerHandleUnitDespawned;
        }
        
        private void AuthorityServerHandleUnitSpawned(Unit unit) => myUnits.Add(unit);

        private void AuthorityServerHandleUnitDespawned(Unit unit) => myUnits.Remove(unit);

        #endregion
    }
}
