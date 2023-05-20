using System;
using System.Collections.Generic;
using Buildings;
using Mirror;
using Networking;
using UnityEngine;

namespace Units
{
    public class UnitSelectionHandler : MonoBehaviour
    {
        public List<Unit> SelectedUnits { get; } = new();
        [SerializeField] private RectTransform _unitSelectionArea = null;
        [SerializeField] private LayerMask _layerMask = new LayerMask();
        
        private RTSPlayer _player;
        private Camera _mainCamera;
        private Vector2 _startPosition;
        
        private void Start()
        {
            _mainCamera = Camera.main;
            Unit.AuthorityServerOnUnitDespawned += AuthorityHandleUnitDespawned;
            GameOverHandler.ClientOnGameOver += ClientHandlerGameOver;
        }

        private void OnDestroy()
        {
            Unit.AuthorityServerOnUnitDespawned -= AuthorityHandleUnitDespawned;
            GameOverHandler.ClientOnGameOver -= ClientHandlerGameOver;
        }

        private void ClientHandlerGameOver(string winnerName)
        {
            enabled = false;
        }

        private void AuthorityHandleUnitDespawned(Unit unit)
        {
            SelectedUnits.Remove(unit);
        }

        private void Update()
        {
            if (_player == null)
                _player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();
            if (Input.GetMouseButtonDown(0))
            {
                StartSelectionArea();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ClearSelectionArea();
            }
            else if (Input.GetMouseButton(0))
            {
                UpdateSelectionArea();
            }
        }

        private void UpdateSelectionArea()
        {
            Vector2 mousePosition = Input.mousePosition;
            float areaWidth = mousePosition.x - _startPosition.x;
            float areaHeight = mousePosition.y - _startPosition.y;
            _unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
            _unitSelectionArea.anchoredPosition = _startPosition + new Vector2(areaWidth / 2, areaHeight / 2);
        }
        
        private void StartSelectionArea()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                foreach (Unit selectedUnit in SelectedUnits) 
                    selectedUnit.Deselect();
                SelectedUnits.Clear();
            }

            _unitSelectionArea.gameObject.SetActive(true);
            _startPosition = Input.mousePosition;
            UpdateSelectionArea();
        }
        
        

        private void ClearSelectionArea()
        {
            _unitSelectionArea.gameObject.SetActive(false);
            if (_unitSelectionArea.sizeDelta.magnitude == 0)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask)) {return;}

                if (!hit.collider.TryGetComponent(out Unit unit)) {return;}

                if (!unit.hasAuthority) {return;}
                SelectedUnits.Add(unit);

                foreach (Unit selectedUnit in SelectedUnits)
                    selectedUnit.Select();
                
                return;
            }

            Vector2 min = _unitSelectionArea.anchoredPosition - (_unitSelectionArea.sizeDelta / 2);
            Vector2 max = _unitSelectionArea.anchoredPosition + (_unitSelectionArea.sizeDelta / 2);

            foreach (Unit unit in _player.GetMyUnits())
            {
                if(SelectedUnits.Contains(unit)) continue;
                Vector3 screenPosition = _mainCamera.WorldToScreenPoint(unit.transform.position);
                if (screenPosition.x > min.x && screenPosition.x < max.x 
                                             && screenPosition.y > min.y 
                                             && screenPosition.y > max.y)
                {
                    SelectedUnits.Add(unit);
                    unit.Select();
                }
            }
        }
    }
}
