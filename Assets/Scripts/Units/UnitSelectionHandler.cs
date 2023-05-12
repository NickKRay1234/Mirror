using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Units
{
    public class UnitSelectionHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        private List<Unit> selectedUnits = new();
        [SerializeField] private LayerMask _layerMask = new LayerMask();
        
        private void Start() => _mainCamera = Camera.main;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                foreach (Unit selectedUnit in selectedUnits)
                {
                    selectedUnit.Deselect();
                }
                selectedUnits.Clear();
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
            selectedUnits.Add(unit);

            foreach (Unit selectedUnit in selectedUnits)
                selectedUnit.Select();
        }
    }
}
