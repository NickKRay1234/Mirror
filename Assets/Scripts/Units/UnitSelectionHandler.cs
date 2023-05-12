using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class UnitSelectionHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        public List<Unit> SelectedUnits { get; } = new();
        [SerializeField] private LayerMask _layerMask = new LayerMask();
        
        private void Start() => _mainCamera = Camera.main;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (Unit selectedUnit in SelectedUnits)
                {
                    selectedUnit.Deselect();
                }
                SelectedUnits.Clear();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ClearSelectionArea();
            }
        }

        private void ClearSelectionArea()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask));

            if (!hit.collider.TryGetComponent(out Unit unit)) return;

            if (!unit.hasAuthority) return;
            SelectedUnits.Add(unit);

            foreach (Unit selectedUnit in SelectedUnits)
                selectedUnit.Select();
        }
    }
}
