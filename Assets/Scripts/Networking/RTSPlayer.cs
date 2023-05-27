using System;
using System.Collections.Generic;
using Buildings;
using Mirror;
using Units;
using UnityEngine;

namespace Networking
{
    public class RTSPlayer : NetworkBehaviour
    {
        [SerializeField] private Building[] _buildings = new Building[0];
        [SerializeField] private LayerMask _buildingBlockLayer = new LayerMask();
        [SerializeField] private float _buildingRangeLimit = 5f;

        [SyncVar(hook = nameof(ClientHandleResourcesUpdated))]
        private int _resources = 500;

        public event Action<int> ClientOnResourcesUpdated;
        
        [SerializeField] private List<Unit> myUnits = new();
        private List<Building> myBuildings = new();

        public List<Unit> GetMyUnits() => myUnits;

        public override void OnStartServer()
        {
            Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
            Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
            Building.ServerOnBuildingSpawned += ServerHandleBuildingSpawned;
            Building.ServerOnBuildingDespawned += ServerHandleBuildingDespawned;
        }

        public override void OnStopServer()
        {
            Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
            Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
            Building.ServerOnBuildingSpawned -= ServerHandleBuildingSpawned;
            Building.ServerOnBuildingDespawned -= ServerHandleBuildingDespawned;
        }

        public List<Building> GetMyBuildings() => myBuildings;

        private void ServerHandleUnitSpawned(Unit unit)
        {
            if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
            myUnits.Add(unit);
        }

        public bool CanPlaceBuilding(BoxCollider buildingCollider, Vector3 point)
        {
            if(Physics.CheckBox(point + buildingCollider.center, buildingCollider.size / 2, Quaternion.identity, _buildingBlockLayer)) 
                return false;
            
            foreach (Building building in myBuildings)
                if ((point - building.transform.position).sqrMagnitude <= _buildingRangeLimit * _buildingRangeLimit) 
                    return true;

            return false;
        }

        [Command]
        public void CmdTryPlaceBuilding(int buildingID, Vector3 point)
        {
            Building buildingToPlace = null;
            foreach (Building building in _buildings)
            {
                if (building.GetID() == buildingID)
                {
                    buildingToPlace = building;
                    break;
                }
            }

            if (buildingToPlace == null) return;
            if (_resources < buildingToPlace.GetPrice()) return;
            BoxCollider buildingCollider = buildingToPlace.GetComponent<BoxCollider>();
            if (!CanPlaceBuilding(buildingCollider, point)) return;
            GameObject buildingInstance = Instantiate(buildingToPlace.gameObject, point, buildingToPlace.transform.rotation);
            NetworkServer.Spawn(buildingInstance, connectionToClient);
            SetResources(_resources - buildingToPlace.GetPrice());
        }
        
        private void ServerHandleUnitDespawned(Unit unit)
        {
            if (unit.connectionToClient.connectionId != connectionToClient.connectionId) return;
            myUnits.Remove(unit);
        }
        
        private void ServerHandleBuildingSpawned(Building building)
        {
            if (building.connectionToClient.connectionId != connectionToClient.connectionId) return;
            myBuildings.Add(building);
        }
        
        private void ServerHandleBuildingDespawned(Building building)
        {
            if (building.connectionToClient.connectionId != connectionToClient.connectionId) return;
            myBuildings.Remove(building);
        }

        #region Client

        public override void OnStartAuthority()
        {
            if (NetworkServer.active) return;
            Unit.AuthorityServerOnUnitSpawned += AuthorityHandleUnitSpawned;
            Unit.AuthorityServerOnUnitDespawned += AuthorityHandleUnitDespawned;
            Building.AuthorityOnBuildingSpawned += AuthorityHandleBuildingSpawned;
            Building.AuthorityOnBuildingDespawned += AuthorityHandleBuildingDespawned;
        }

        public override void OnStopClient()
        {
            if (!isClientOnly || !hasAuthority) return;
            Unit.AuthorityServerOnUnitSpawned -= AuthorityHandleUnitSpawned;
            Unit.AuthorityServerOnUnitDespawned -= AuthorityHandleUnitDespawned;
            Building.AuthorityOnBuildingSpawned -= AuthorityHandleBuildingSpawned;
            Building.AuthorityOnBuildingDespawned -= AuthorityHandleBuildingDespawned;
        }

        private void ClientHandleResourcesUpdated(int oldResources, int newResources)
        {
            ClientOnResourcesUpdated?.Invoke(newResources);
        }
        
        private void AuthorityHandleUnitSpawned(Unit unit) => myUnits.Add(unit);

        private void AuthorityHandleUnitDespawned(Unit unit) => myUnits.Remove(unit);
        
        private void AuthorityHandleBuildingSpawned(Building building) => myBuildings.Add(building);

        private void AuthorityHandleBuildingDespawned(Building building) => myBuildings.Remove(building);

        #endregion

        public int GetResources() => _resources;

        [Server]
        public void SetResources(int newResources) => _resources = newResources;
    }
}
