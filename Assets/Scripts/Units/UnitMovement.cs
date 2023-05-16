using UnityEngine;
using UnityEngine.AI;

namespace Mirror.Examples.Benchmark.Scripts
{
    public class UnitMovement : NetworkBehaviour
    {
        [SerializeField] private NavMeshAgent _agent = null;

        #region Server


        [ServerCallback]
        private void Update()
        {
            if (!_agent.hasPath) return;
            if (_agent.remainingDistance > _agent.stoppingDistance) return;
            _agent.ResetPath();
        }
        
        [Command]
        public void Move(Vector3 position)
        {
            if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) return;
            _agent.SetDestination(hit.position);
        }

        #endregion
    }
    
    
    
}
