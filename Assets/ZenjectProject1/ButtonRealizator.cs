using UnityEngine;
using Zenject;

namespace ZenjectProject1
{
    public class ButtonRealizator : MonoBehaviour
    {
        private ISoundManager _soundManager;

        [Inject]
        public void Init(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _soundManager.Play();
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _soundManager.Stop();
            }
        }
        
    }
}