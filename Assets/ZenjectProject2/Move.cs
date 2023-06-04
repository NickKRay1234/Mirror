using UnityEngine;
using Zenject;

namespace ZenjectProject2
{
    public class Move : MonoBehaviour
    {
        private IInputHandler _inputHandler;

        [Inject]
        public void Init(IInputHandler inputHandler) => _inputHandler = inputHandler;

        private void Update() => _inputHandler.HandleInput(transform);
    }
}
