using System;
using Mirror;
using UnityEngine;

namespace Buildings
{
    public class Building : NetworkBehaviour
    {
        [SerializeField] private GameObject _buildingPreview = null;
        [SerializeField] private Sprite _icon = null;
        [SerializeField] private int ID = -1;
        [SerializeField] private int _price = 100;

        public static event Action<Building> ServerOnBuildingSpawned;
        public static event Action<Building> ServerOnBuildingDespawned;
        
        public static event Action<Building> AuthorityOnBuildingSpawned;
        public static event Action<Building> AuthorityOnBuildingDespawned;

        public GameObject GetBuildingPreview() => _buildingPreview;

        public Sprite GetIcon() => _icon;
        public int GetID() => ID;
        public int GetPrice() => _price;

        #region Server

        public override void OnStartServer()
        {
            ServerOnBuildingSpawned?.Invoke(this);
        }

        public override void OnStopServer()
        {
            ServerOnBuildingDespawned?.Invoke(this);
        }

        #endregion

        #region Client

        public override void OnStartAuthority()
        {
            AuthorityOnBuildingSpawned?.Invoke(this);
        }

        public override void OnStopClient()
        {
            if (!hasAuthority) return;
            AuthorityOnBuildingDespawned?.Invoke(this);
        }

        #endregion

    }
}
