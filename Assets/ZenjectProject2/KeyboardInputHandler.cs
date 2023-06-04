using UnityEngine;

namespace ZenjectProject2
{
    public class KeyboardInputHandler : IInputHandler
    {
        public void HandleInput(Transform obj)
        {
            if(Input.GetKeyDown(KeyCode.D))
            {
                obj.SetPositionAndRotation(
                    new Vector3(obj.position.x + 1, obj.position.y, obj.position.z), Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                obj.SetPositionAndRotation(
                    new Vector3(obj.position.x - 1, obj.position.y, obj.position.z), Quaternion.identity);
            }
        }
    }
}