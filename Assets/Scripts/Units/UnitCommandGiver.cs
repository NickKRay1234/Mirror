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
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(1)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask)) return;

            TryMove(hit.point);
        }

        private void TryMove(Vector3 hitInfoPoint)
        {
            foreach (Unit unit in _unitSelectionHandler.SelectedUnits)
            {
                unit.GetUnitMovement().Move(hitInfoPoint);
            }
        }
    }
}
