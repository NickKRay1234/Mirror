using System;
using Buildings;
using Combat;
using UnityEngine;

namespace Units
{
    public class UnitCommandGiver : MonoBehaviour
    {
        [SerializeField] private UnitSelectionHandler _unitSelectionHandler = null;
        [SerializeField] private LayerMask _layerMask;

        private Camera _camera;
        
        
        private void Start()
        {
            _camera = Camera.main;
            GameOverHandler.ClientOnGameOver += ClientHandlerGameOver;
        }

        private void ClientHandlerGameOver(string winnerName)
        {
            enabled = false;
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(1)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask)) return;

            if (hit.collider.TryGetComponent(out Targetable target))
            {
                if (target.hasAuthority)
                {
                    TryMove(hit.point);
                    return;
                }
                TryTarget(target);
                return;
            }
            TryMove(hit.point);
        }

        private void OnDestroy()
        {
            GameOverHandler.ClientOnGameOver -= ClientHandlerGameOver;
        }

        private void TryTarget(Targetable target)
        {
            foreach (Unit unit in _unitSelectionHandler.SelectedUnits)
            {
                unit.GetTargeter().CmdSetTarget(target.gameObject);
            }
        }

        private void TryMove(Vector3 hitInfoPoint)
        {
            foreach (Unit unit in _unitSelectionHandler.SelectedUnits)
            {
                unit.GetUnitMovement().CmdMove(hitInfoPoint);
            }
        }
    }
}
