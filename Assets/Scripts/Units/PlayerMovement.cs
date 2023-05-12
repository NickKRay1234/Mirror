using UnityEngine;
using UnityEngine.AI;

namespace Mirror.Examples.Benchmark.Scripts
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent _agent = null;
        private Camera _camera;

        #region Server

        [Command]
        private void Move(Vector3 position)
        {
            if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;
            _agent.SetDestination(hit.position);
        }

        #endregion

        #region Client

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            _camera = Camera.main;
        }

        [ClientCallback]
        private void Update()
        {
            if(!hasAuthority) return;
            if (!Input.GetMouseButtonDown(1)) return;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
            Move(hit.point);
        }

        #endregion
    }
    
    
    
}
