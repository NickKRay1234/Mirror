using UnityEngine;

namespace ZenjectProject2
{
    public class MouseInputHandler : IInputHandler
    {
        public void HandleInput(Transform obj)
        {
            if(Input.GetMouseButtonDown(0))
            {
                obj.SetPositionAndRotation(
                    new Vector3(obj.position.x + 1, obj.position.y, obj.position.z), Quaternion.identity);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                obj.SetPositionAndRotation(
                    new Vector3(obj.position.x - 1, obj.position.y, obj.position.z), Quaternion.identity);
            }
        }
    }
}