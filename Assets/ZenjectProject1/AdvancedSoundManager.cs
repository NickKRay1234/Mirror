using UnityEngine;

namespace ZenjectProject1
{
    public class AdvancedSoundManager : ISoundManager
    {
        public void Play()
        {
            Debug.Log("Music plays 2");
        }

        public void Stop()
        {
            Debug.Log("Music stops 2");
        }
    }
}